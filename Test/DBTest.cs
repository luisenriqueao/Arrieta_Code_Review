using CNN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arrieta_Code_Review.JobLogger;

namespace Test
{
    [TestClass]
    public class DBTest
    {
        public SqlDataReader ExecuteSelect(string sQuery)
        {
            SqlConnection conn = Connection.getConnection();
            SqlCommand command = new SqlCommand(sQuery, conn);
            SqlDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }
        
        public string GetMessageFromRegister(string sQuery)
        {
            string sMessage = "";
            SqlDataReader sqlReader = ExecuteSelect(sQuery);
            while (sqlReader.Read())
            {
                sMessage = sqlReader.GetString(0);
                break;
            }
            sqlReader.Close();
            return sMessage;
        }
        
        public string GetSelectQuery(string sMessage, MessageType myMessageType)
        {
            return @"Select Message from Log where Message = '" + sMessage + "' and MessageType ='" + (int)myMessageType + "'";
        }
    }
}
