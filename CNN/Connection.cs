using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNN
{
    public class Connection
    {
        static SqlConnection conn;

        public static SqlConnection getConnection()
        {

            if (conn == null)
            {

                conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            }
            
            if (conn.State != ConnectionState.Open) { conn.Open(); }
            return conn;
        }
    }
}
