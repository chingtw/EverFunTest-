using System;
using System.Data;
using System.Threading.Tasks;
using System.Web.Security;
using DataAccessLayer;

namespace EverFunTest.Models.BusinessLogic
{
    public class GetLogin
    {
        #region 登入功能
        [Obsolete]
        public static Task<Result> GetLoginList(string Email, string password)
        {
           
            string Result = string.Empty;
            string MD5_Password = string.Empty;
            Result = "0";
            MD5_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower();

            try
            {
                string ms = "此資料已經送出，請物重複送出。";
                string SQLQuery = string.Empty;

                SQLQuery = "SELECT * FROM ADM_Admin WHERE admin_id = '" + Email + "' AND admin_pw = '" + MD5_Password + "'";

                MySQLConnector SQLer = new MySQLConnector("MySQLConnection");
                DataSet ds = SQLer.GetSQLDataTable(SQLQuery);
                if (ds.Tables[0].Rows.Count >= 1)
                {
                    string UserLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string AccessToken = FormsAuthentication.HashPasswordForStoringInConfigFile(Email + UserLoginDate, "MD5").ToLower();

                    // 寫入Log
                    SQLQuery = "INSERT INTO ADM_Logs (log_status, log_description, login_id, login_pw, log_time) VALUES ('1','login ok','" + Email + "', '" + MD5_Password + "', '" + UserLoginDate + "')";
                    MySQLConnector.SQLExecute(SQLQuery);
                    // 更新登入時間
                    SQLQuery = "UPDATE ADM_Admin SET last_login = '" + UserLoginDate + "' WHERE admin_id = '" + Email + "' AND admin_pw = '" + MD5_Password + "'";
                    MySQLConnector.SQLExecute(SQLQuery);

                    // 生成Token 3小時 三小時自動登出
                    string AccessTime = DateTime.Now.AddMinutes(180).ToString("yyyy-MM-dd HH:mm:ss");
                    SQLQuery = "INSERT INTO ADM_LoginKeys (login_key, login_id, access_time, login_time) VALUES ('" + AccessToken + "','" + Email + "' , '" + AccessTime + "', '" + UserLoginDate + "')";
                    MySQLConnector.SQLExecute(SQLQuery);


                    // 取得使用者資訊
                    string AutoID = ds.Tables[0].Rows[0]["AutoID"].ToString();
                    string UserID = ds.Tables[0].Rows[0]["admin_id"].ToString();
                    string UserPW = ds.Tables[0].Rows[0]["admin_pw"].ToString();
                    string UserName = ds.Tables[0].Rows[0]["admin_name"].ToString();
                    string UserEnName = ds.Tables[0].Rows[0]["admin_EnName"].ToString();
                    string UserPhone = ds.Tables[0].Rows[0]["admin_phone"].ToString();
                    string UserPhoto = ds.Tables[0].Rows[0]["admin_Photo"].ToString();
                    string UserGender = ds.Tables[0].Rows[0]["admin_gender"].ToString();
                    string UserBirthday = ((DateTime)ds.Tables[0].Rows[0]["admin_Birthday"]).ToString("yyyy/M/d");
                    string UserAddress = ds.Tables[0].Rows[0]["admin_Address"].ToString();
                    string LoginTime = ds.Tables[0].Rows[0]["AddTime"].ToString();

                    var res = new UsersData
                    {
                        AutoID = AutoID,
                        admin_id = UserID,
                        admin_pw = UserPW,
                        admin_name = UserName,
                        admin_EnName = UserEnName,
                        admin_phone = UserPhone,
                        admin_Photo = UserPhoto,
                        admin_gender = UserGender,
                        admin_Birthday = UserBirthday,
                        admin_Address = UserAddress,
                        AddTime = LoginTime,

                    };

                    Result = "1";
                    int code = int.Parse(Result.ToString());
                    string msg = ms.ToString();

                    return Task.FromResult(new Result() { Code = code, Message = ms, Data = res,Token = AccessToken });
                }
                else {
                    return Task.FromResult(new Result() { Code = 0, Message = ms, Data =null });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result() { Code = -1, Message = $"執行失敗(801)。", Data = ex });
            }

        }
        #endregion

        #region 註冊功能
        [Obsolete]
        public static Task<Result> PostLoginList(string Email, string password,string Name, string Phone)
        {
            
            string MD5_Password = string.Empty;
            MD5_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower();

            try
            {
                string ms = "此資料已經送出，請物重複送出。";

                MD5_Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower();

                string SQLQuery = "INSERT IGNORE INTO ADM_Admin (admin_id, admin_pw, admin_name, admin_phone) VALUES ('" + Email + "','" + MD5_Password + "','" + Name + "', '" + Phone + "')";
                int Result = MySQLConnector.SQLExecute(SQLQuery);
                if (Result >= 1)
                {
                    ms = "資料送出成功";

                }
                else
                {
                    ms = "註冊資料重複";
                }


                int code = int.Parse(Result.ToString());
                string msg = ms.ToString();

                return Task.FromResult(new Result() { Code = code, Message = ms, Data = null });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result() { Code = -1, Message = $"執行失敗(801)。", Data = ex });
            }

        }
        #endregion

    }
}