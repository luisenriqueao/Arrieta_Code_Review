using CNN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class CnnTest
    {
        [TestMethod]
        public void ValidateConnectionToDataBase()
        {
            SqlConnection TestConn;
            
            TestConn = Connection.getConnection();
            
            Assert.IsNotNull(TestConn);
        }
    }
}
