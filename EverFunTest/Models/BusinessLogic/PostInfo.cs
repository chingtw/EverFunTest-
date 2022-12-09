using System;
using System.Data;
using System.Threading.Tasks;
using DataAccessLayer;

namespace EverFunTest.Models.BusinessLogic
{
    public class PostInfo
    {
        #region 功能
        [Obsolete]
        public static Task<Result> PostInfoList(string id, string photoData, string name, string enname, string phone, string gender, string birthday, string address,string password)
        {
           
            string Result = string.Empty;
            Result = "0";

            try
            {
                string ms = "此資料已經送出，請物重複送出。";
                string SQLQuery = string.Empty;
                string SQLQuery2 = string.Empty;
                string SQLQuery3 = string.Empty;

                SQLQuery = "SELECT * FROM ADM_Admin WHERE admin_id = '" + id + "' AND admin_pw = '" + password + "'";

                MySQLConnector SQLer = new MySQLConnector("MySQLConnection");
                DataSet ds = SQLer.GetSQLDataTable(SQLQuery);
                if (ds.Tables[0].Rows.Count >= 1)
                {
                    string dsAutoID = ds.Tables[0].Rows[0]["AutoID"].ToString();

                    //手機號碼修改重複查詢
                    if (ds.Tables[0].Rows[0]["admin_phone"].ToString() != phone)
                    {
                        SQLQuery2 = "SELECT * FROM ADM_Admin WHERE admin_phone = '" + phone + "'";
                        DataSet ds2 = SQLer.GetSQLDataTable(SQLQuery);
                        if (ds2.Tables[0].Rows.Count >= 1)
                        {
                            Result = "-3";
                            ms = "手機號碼已被註冊。";
                        }
                        else
                        {
                            Result = "1";
                        }
                    }
                    else {
                        Result = "1";
                    }
                    
                    // 更新資料
                    SQLQuery3 = string.Format("UPDATE ADM_Admin SET admin_Photo = '{0}', admin_name = '{1}', admin_EnName = '{2}', admin_phone = '{3}', admin_gender = '{4}', admin_Birthday = '{5}', admin_Address = '{6}' WHERE AutoID = {7}", photoData, name, enname, phone, gender, birthday, address, dsAutoID);
                    MySQLConnector.SQLExecute(SQLQuery3);


                    // 取得使用者資訊
                    DataSet ds3 = SQLer.GetSQLDataTable(SQLQuery);
                    string AutoID = ds3.Tables[0].Rows[0]["AutoID"].ToString();
                    string UserID = ds3.Tables[0].Rows[0]["admin_id"].ToString();
                    string UserPW = ds3.Tables[0].Rows[0]["admin_pw"].ToString();
                    string UserName = ds3.Tables[0].Rows[0]["admin_name"].ToString();
                    string UserEnName = ds3.Tables[0].Rows[0]["admin_EnName"].ToString();
                    string UserPhone = ds3.Tables[0].Rows[0]["admin_phone"].ToString();
                    string UserPhoto = ds3.Tables[0].Rows[0]["admin_Photo"].ToString();
                    string UserGender = ds3.Tables[0].Rows[0]["admin_gender"].ToString();
                    string UserBirthday = ((DateTime)ds3.Tables[0].Rows[0]["admin_Birthday"]).ToString("yyyy/M/d");
                    string UserAddress = ds3.Tables[0].Rows[0]["admin_Address"].ToString();
                    string LoginTime = ds3.Tables[0].Rows[0]["AddTime"].ToString();

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

                  
                    int code = int.Parse(Result.ToString());
                    string msg = ms.ToString();

                    return Task.FromResult(new Result() { Code = code, Message = ms, Data = res ,Token = "" });
                }
                else {
                    ms = "資料更新失敗。";
                    return Task.FromResult(new Result() { Code = 0, Message = ms, Data =null });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result() { Code = -1, Message = $"執行失敗(801)。", Data = ex });
            }

        }
        #endregion

      
    }
}