using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using EverFunTest.Models.BusinessLogic;
using EverFunTest.Models;

namespace EverFunTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region 登入驗證
            string sAccessToken = Convert.ToString(Session["Token"] ?? "").Trim();
            string Feedback = logonchecker.permission(sAccessToken);
            if (Feedback == "Pass")
            {
                ViewBag.Mode = "info";
            }
            else if (Feedback == "Overtime")
            {
                //已超過本次允許之登入時間，請重新登入。
                ViewBag.Mode = "Login";
            }

            else if (Feedback == "UnknownUser")
            {
                ViewBag.Mode = "Login";
            }
            else
            {
                ViewBag.Mode = "Login";

            }
            #endregion

            return View();
        }
        #region 登入功能
        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> LoginByPassword(string email, string password)
        {
            try
            {
                string e = email; string p = password;
                var res = await GetLogin.GetLoginList(e, p);
                string str = JsonConvert.SerializeObject(res.Data);
              //UsersData user = JsonConvert.DeserializeObject<UsersData>(str);
                if (res.Code == 1)
                {
                    ViewBag.Mode = "info";
                    Session["Token"] = res.Token;
                    return Json(new { success = true, responsedata = String.Format("{0}", str), Token = String.Format("{0}", res.Token) }, JsonRequestBehavior.DenyGet);
                }
                else
                    return Json(new { success = false, responseText = "登錄失敗", info = res }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ee)
            {
                return Json(new { success = false, responseText = "執行失敗" }, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion
        #region 登出功能
        [HttpPost]
        public ActionResult LoginOut()
        {
            try
            {
                Session["Token"] = "";
                return Json(new { success = true, responseText = "登出成功" }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ee)
            {
                return Json(new { success = false, responseText = "執行失敗" }, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion

        #region 註冊功能
        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> PostLogin(string email, string password,string name,string phone)
        {
            try
            {
                string e = email; string p = password;
                var res = await GetLogin.PostLoginList(e, p, name, phone);
                string str = JsonConvert.SerializeObject(res.Data);
                Users user = JsonConvert.DeserializeObject<Users>(str);
                if (res.Code == 1)
                {
                    return Json(new { success = true, responsedata = String.Format("{0}", user) }, JsonRequestBehavior.DenyGet);
                }
                else
                    return Json(new { success = false, responseText = "註冊失敗", info = res }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ee)
            {
                return Json(new { success = false, responseText = "執行失敗" }, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion
        #region 資料修改功能
       [HttpPost]
        [Obsolete]
        public async Task<ActionResult> GoPostInfo(string id, string photoData, string name, string enname, string phone,string gender,string birthday,string address,string pw)
        {
            try
            {
               string p = photoData; string n = name ; string en = enname; string ph = phone; string gd = gender; string bd = birthday; string ad = address;
                var res = await PostInfo.PostInfoList(id, p, name, en, ph, gd, bd, ad, pw);
                string str = JsonConvert.SerializeObject(res.Data);
                if (res.Code == 1)
                {
                    return Json(new { success = true, responsedata = String.Format("{0}", str) }, JsonRequestBehavior.DenyGet);
                }
                else
                    return Json(new { success = false, responseText = "修改失敗", info = res.Message }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ee)
            {
                return Json(new { success = false, responseText = "執行失敗" }, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion
    }
}