using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class MySQLConnector
    {
        public MySQLConnector()
        {
            DataConnection = "MySQLConnection";
        }

        public MySQLConnector(string gamecoon)
        {
            DataConnection = gamecoon;
        }

        public static string DataConnection { get; set; }

        public DataSet GetSQLDataTable(string SQLStr)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[DataConnection].ConnectionString))
            {
                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(SQLStr, conn);
                    da.Fill(ds);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    if (conn != null)
                        ((IDisposable)conn).Dispose();
                }
            }

            return ds;
        }

        public DataSet GetSQLDataTable(string SQLStr, int StartRow, int MaxRows)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[DataConnection].ConnectionString))
            {
                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(SQLStr, conn);
                    da.Fill(ds, StartRow, MaxRows, "Tables");
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                    //System.Web.HttpContext context = System.Web.HttpContext.Current;
                    //context.Response.Write(ex);
                }
                finally
                {
                    if (conn != null)
                        ((IDisposable)conn).Dispose();
                }
            }

            return ds;
        }

        public static int SQLExecute(string SQLStr)
        {
            int result = 0;

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[DataConnection].ConnectionString))
            {
                try
                {
                    MySqlCommand CmdB = new MySqlCommand();
                    CmdB.CommandType = CommandType.Text;
                    CmdB.Connection = conn;
                    CmdB.Connection.Open();
                    CmdB.CommandText = SQLStr;
                    result = CmdB.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    if (conn != null)
                        ((IDisposable)conn).Dispose();
                }
            }
            return result;
        }

    }

}
