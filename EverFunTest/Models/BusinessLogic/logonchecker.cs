using System;
using System.Data;
using DataAccessLayer;

public class logonchecker
{

    public static string permission(string UserLogonKey)
    {
        #region 登入Token計時比對
        string Feedback = string.Empty;

        string SQLQuery = string.Empty;
        SQLQuery = "SELECT * FROM ADM_LoginKeys WHERE login_key = '" + UserLogonKey + "'";
        MySQLConnector SQLer = new MySQLConnector("MySQLConnection");
        DataSet ds01 = SQLer.GetSQLDataTable(SQLQuery);

        if (ds01.Tables[0].Rows.Count >= 1)
        { 

            DateTime LoginLimit = (DateTime)ds01.Tables[0].Rows[0]["access_time"];

            if (DateTime.Now > LoginLimit)
            {
                Feedback = "Overtime";
            }
            else {
                Feedback = "Pass";
            }
        }
        else
        {
            Feedback = "UnknownUser";
        }

        return Feedback;

    }
    #endregion
}