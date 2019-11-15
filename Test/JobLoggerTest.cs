using Arrieta_Code_Review;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class JobLoggerTest
    {
        DBTest dbHelper = new DBTest();
        JobLogger.MessageType myMessageType = new JobLogger.MessageType();
        string sExpectedMessage = "";
        
        [TestMethod]
        public void NotAllowLogForMessagesDataBase()
        {
            
            string sMessage = "My test Message not allowed in data base";
            myMessageType = JobLogger.MessageType.Message;
            JobLogger.ConfigJobLogger(false, false, true, false, false, true);
            string sQuery = dbHelper.GetSelectQuery(sMessage, myMessageType);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            string sSavedMessage = dbHelper.GetMessageFromRegister(sQuery);
            Assert.AreEqual(sExpectedMessage, sSavedMessage);
        }
        
        [TestMethod]
        public void NotAllowLogForWarningsDataBase()
        {
            string sMessage = "My test Warning not allowed in data base";
            myMessageType = JobLogger.MessageType.Warning;
            JobLogger.ConfigJobLogger(false, false, true, false, false, true);
            string sQuery = dbHelper.GetSelectQuery(sMessage, myMessageType);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            string sSavedMessage = dbHelper.GetMessageFromRegister(sQuery);
            Assert.AreEqual(sExpectedMessage, sSavedMessage);
        }
        
        [TestMethod]
        public void NotAllowLogForErrorsDataBase()
        {
            string sMessage = "My test Error not allowed in database";
            myMessageType = JobLogger.MessageType.Error;
            JobLogger.ConfigJobLogger(false, false, true, false, true, false);
            string sQuery = dbHelper.GetSelectQuery(sMessage, myMessageType);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            string sSavedMessage = dbHelper.GetMessageFromRegister(sQuery);
            Assert.AreEqual(sExpectedMessage, sSavedMessage);
        }
        
        [TestMethod]
        public void NotAllowLogForDataBaseAnyType()
        {
            string sMessage = "My test log not allowed in database";
            myMessageType = JobLogger.MessageType.Error; 
            JobLogger.ConfigJobLogger(false, false, false, true, true, true);
            string sQuery = dbHelper.GetSelectQuery(sMessage, myMessageType);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            string sSavedMessage = dbHelper.GetMessageFromRegister(sQuery);
            Assert.AreEqual(sExpectedMessage, sSavedMessage);
        }

        [TestMethod]
        public void RegisterOnlyMessagesDataBase()
        {
            string sMessage = "My test message in data base";
            myMessageType = JobLogger.MessageType.Message;
            JobLogger.ConfigJobLogger(false, false, true, true, false, false);
            string sQuery = dbHelper.GetSelectQuery(sMessage, myMessageType);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            sExpectedMessage = dbHelper.GetMessageFromRegister(sQuery);
            
            Assert.AreEqual(sMessage, sExpectedMessage);
        }
        
        [TestMethod]
        public void RegisterOnlyWarningsDataBase()
        {
            string sMessage = "My test warning in data base";
            myMessageType = JobLogger.MessageType.Warning;
            JobLogger.ConfigJobLogger(false, false, true, false, true, false);
            string sQuery = dbHelper.GetSelectQuery(sMessage, myMessageType);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            sExpectedMessage = dbHelper.GetMessageFromRegister(sQuery);
            
            Assert.AreEqual(sMessage, sExpectedMessage);
        }
        
        [TestMethod]
        public void RegisterOnlyErrorsDataBase()
        {
            string sMessage = "My test error in data base";
            myMessageType = JobLogger.MessageType.Error;
            JobLogger.ConfigJobLogger(false, false, true, false, false, true);
            string sQuery = dbHelper.GetSelectQuery(sMessage, myMessageType);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            sExpectedMessage = dbHelper.GetMessageFromRegister(sQuery);
            
            Assert.AreEqual(sMessage, sExpectedMessage);
        }

        //***************************

        #region TextFile Test

        string sFileName = ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
        
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void NotAllowLogForMessagesTextFile()
        {

            string sMessage = "My test type Message not allowed in text file";
            myMessageType = JobLogger.MessageType.Message;
            JobLogger.ConfigJobLogger(true, false, false, false, false, true);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            System.IO.File.ReadLines(sFileName).Last();
        }
        
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void NotAllowLogForWarningsTextFile()
        {

            string sMessage = "My test type Warning not allowed in text file";
            myMessageType = JobLogger.MessageType.Warning;
            JobLogger.ConfigJobLogger(true, false, false, false, false, true);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            System.IO.File.ReadLines(sFileName).Last();
        }
        
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void NotAllowLogForErrorsTextFile()
        {
            string sMessage = "My test type error not allowed in text file";
            myMessageType = JobLogger.MessageType.Error;
            JobLogger.ConfigJobLogger(true, false, false, false, true, false);
            
            JobLogger.LogMessage(sMessage, myMessageType);

            System.IO.File.ReadLines(sFileName).Last();
        }
        
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void NotAllowLogForTextFileAnyType()
        {
            string sMessage = "My test log not allowed in text file";
            myMessageType = JobLogger.MessageType.Error;
            JobLogger.ConfigJobLogger(false, false, false, true, true, true);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            System.IO.File.ReadLines(sFileName).Last();
        }
        
        [TestMethod]
        public void RegisterOnlyMessagesTextFile()
        {
            string sMessage = "My test type Message in text file";
            myMessageType = JobLogger.MessageType.Message;
            JobLogger.ConfigJobLogger(true, false, false, true, false, false);            
            string sExpectedMessage = string.Format("{0} - {1} : {2}", myMessageType.ToString(), DateTime.Now, sMessage);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            string sMessageRegistered = System.IO.File.ReadLines(sFileName).Last();
            Assert.AreEqual(sExpectedMessage, sMessageRegistered);
        }
        
        [TestMethod]
        public void RegisterOnlyWarningsTextFile()
        {
            
            string sMessage = "My test type Warning in text file";
            myMessageType = JobLogger.MessageType.Warning;
            JobLogger.ConfigJobLogger(true, false, false, false, true, false);           
            string sExpectedMessage = string.Format("{0} - {1} : {2}", myMessageType.ToString(), DateTime.Now, sMessage);

            
            JobLogger.LogMessage(sMessage, myMessageType);

            
            string sMessageRegistered = System.IO.File.ReadLines(sFileName).Last();           
            Assert.AreEqual(sExpectedMessage, sMessageRegistered);
        }
        
        [TestMethod]
        public void RegisterOnlyErrorsTextFile()
        {
            //Arrange
            string sMessage = "My test type Error in text file";
            myMessageType = JobLogger.MessageType.Error;
            JobLogger.ConfigJobLogger(true, false, false, false, false, true);//Config to register log of only errors in a text file            
            string sExpectedMessage = string.Format("{0} - {1} : {2}", myMessageType.ToString(), DateTime.Now, sMessage);

            //Act
            JobLogger.LogMessage(sMessage, myMessageType);

            //Assert
            string sMessageRegistered = System.IO.File.ReadLines(sFileName).Last(); //Read the last line in the file            
            Assert.AreEqual(sExpectedMessage, sMessageRegistered); //Validate if the value is registered in the text file
        }
        #endregion TextFile Test
    }

}
