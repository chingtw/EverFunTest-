using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class MySqlContext : DbContext
    {
        public MySqlContext() : base("MySQLConnection") { }
    }
}