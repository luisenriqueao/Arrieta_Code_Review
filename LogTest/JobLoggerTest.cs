using Arrieta_Code_Review;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTest
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
            
            string sMessage = "Test Message not allowed in data base";
            myMessageType = JobLogger.MessageType.Message;
            JobLogger.ConfigJobLogger(false, false, true, false, false, true); // cambio true - false , var 6
            string sQuery = dbHelper.GetSelectQuery(sMessage, myMessageType);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            string sSavedMessage = dbHelper.GetMessageFromRegister(sQuery);
            Assert.AreEqual(sExpectedMessage, sSavedMessage);
        }
        
        [TestMethod]
        public void NotAllowLogForWarningsDataBase()
        {
            string sMessage = "Test Warning not allowed in data base";
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
            string sMessage = "Test Error not allowed in database";
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
            string sMessage = "Test log not allowed in database";
            myMessageType = JobLogger.MessageType.Error; 
            JobLogger.ConfigJobLogger(false, false, false, true, true, true);
            string sQuery = dbHelper.GetSelectQuery(sMessage, myMessageType);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            string sSavedMessage = dbHelper.GetMessageFromRegister(sQuery);
            Assert.AreEqual(sExpectedMessage, sSavedMessage);
        }



        


        //***************************
        
        string sFileName = ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
        

          
  
        

        
        [TestMethod]
        public void RegisterOnlyMessagesTextFile()
        {
            string sMessage = "Test type Message in text file";
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
            
            string sMessage = "Test type Warning in text file";
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
            string sMessage = "Test type Error in text file";
            myMessageType = JobLogger.MessageType.Error;
            JobLogger.ConfigJobLogger(true, false, false, false, false, true);//Config to register log of only errors in a text file            
            string sExpectedMessage = string.Format("{0} - {1} : {2}", myMessageType.ToString(), DateTime.Now, sMessage);

            //Act
            JobLogger.LogMessage(sMessage, myMessageType);

            //Assert
            string sMessageRegistered = System.IO.File.ReadLines(sFileName).Last();             
            Assert.AreEqual(sExpectedMessage, sMessageRegistered); 
        }


        //**********************

        StringWriter stringWriter = new StringWriter();
        
        [TestMethod]
        public void NotAllowLogForMessagesConsole()
        {
            string sMessage = "Test Message not allowed console";
            myMessageType = JobLogger.MessageType.Message;
            JobLogger.ConfigJobLogger(false, true, false, false, false, true);
            string sExpectedMessage = "";
            Console.SetOut(stringWriter);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            Assert.AreEqual(sExpectedMessage, stringWriter.ToString());
        }
        
        [TestMethod]
        public void NotAllowLogForWarningConsole()
        {
            string sMessage = "Test Warning not allowed console";
            myMessageType = JobLogger.MessageType.Warning; //sends a type warning
            JobLogger.ConfigJobLogger(false, true, false, false, false, true);
            string sExpectedMessage = "";
            Console.SetOut(stringWriter); 
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            Assert.AreEqual(sExpectedMessage, stringWriter.ToString());
        }
        
        [TestMethod]
        public void NotAllowLogForErrorConsole()
        {
            string sMessage = "Test Error not allowed console";
            myMessageType = JobLogger.MessageType.Error;
            JobLogger.ConfigJobLogger(false, true, false, false, true, false);
            string sExpectedMessage = "";
            Console.SetOut(stringWriter);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            Assert.AreEqual(sExpectedMessage, stringWriter.ToString());
        }
        
        [TestMethod]
        public void NotAllowLogForConsoleAnyType()
        {
            string sMessage = "Test log not allowed in console";
            myMessageType = JobLogger.MessageType.Error;
            JobLogger.ConfigJobLogger(false, false, false, true, true, true);
            string sExpectedMessage = "";
            Console.SetOut(stringWriter);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            Assert.AreEqual(sExpectedMessage, stringWriter.ToString());
        }
        
        [TestMethod]
        public void RegisterOnlyMessagesConsole()
        {
            string sMessage = "Test type Message in the console";
            myMessageType = JobLogger.MessageType.Message;
            JobLogger.ConfigJobLogger(false, true, false, true, false, false);           
            string sExpectedMessage = string.Format("{0} - {1} : {2}", myMessageType.ToString(), DateTime.Now, sMessage);
            Console.SetOut(stringWriter); 
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            Assert.AreEqual(sExpectedMessage + "\r\n", stringWriter.ToString());
        }
        
        [TestMethod]
        public void RegisterOnlyWarningsConsole()
        {
            string sMessage = "Test type Warning in the console";
            myMessageType = JobLogger.MessageType.Warning;
            JobLogger.ConfigJobLogger(false, true, false, false, true, false);          
            string sExpectedMessage = string.Format("{0} - {1} : {2}", myMessageType.ToString(), DateTime.Now, sMessage);
            Console.SetOut(stringWriter); 
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            Assert.AreEqual(sExpectedMessage + "\r\n", stringWriter.ToString()); 
        }
        
        [TestMethod]
        public void RegisterOnlyErrorsConsole()
        {
            string sMessage = "Test type Error in the console";
            myMessageType = JobLogger.MessageType.Error;
            JobLogger.ConfigJobLogger(false, true, false, false, false, true);         
            string sExpectedMessage = string.Format("{0} - {1} : {2}", myMessageType.ToString(), DateTime.Now, sMessage);
            Console.SetOut(stringWriter);
            
            JobLogger.LogMessage(sMessage, myMessageType);
            
            Assert.AreEqual(sExpectedMessage + "\r\n", stringWriter.ToString());
        }

        
    }

}
