
namespace EverFunTest.Models
{
    public class Result 
    {       
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
    public class Users
    {
        public string Data { get; set; }
        public string Token { get; set; }
    }
    public class UsersData
    {
        public string AutoID { get; set; }
        public string admin_id { get; set; }
        public string admin_pw { get; set; }
        public string admin_name { get; set; }
        public string admin_EnName { get; set; }
        public string admin_phone { get; set; }
        public string admin_Photo { get; set; }
        public string admin_gender { get; set; }
        public string admin_Birthday { get; set; }
        public string admin_Address { get; set; }
        public string AddTime { get; set; }

    }

}
