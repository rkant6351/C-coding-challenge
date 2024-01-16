using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSyatem.Util
{
    internal class DBUtil
    {
        static SqlConnection connection = null;
        public static SqlConnection GetConnection()
        {
            connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;
            return connection;
        }
    }
}
