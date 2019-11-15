using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNN
{
    public static class Crud
    {
        public static int ExeQuery(string sQuery)
        {
            try
            {
                SqlConnection conn = Connection.getConnection();
                
                SqlCommand command = new SqlCommand(sQuery, conn);

                return command.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
