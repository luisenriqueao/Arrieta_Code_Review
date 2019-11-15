using CNN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class CrudTest
    {
        [TestMethod]
        public void InsertToDataBase()
        {
            string sQuery = "Insert into Log Values('my test message', 1,'" + DateTime.Now + "')";
            int iExpectedValue = 1;
            
            int iAffectedRows = Crud.ExeQuery(sQuery);
            
            Assert.AreEqual(iExpectedValue, iAffectedRows);
        }
    }
}
