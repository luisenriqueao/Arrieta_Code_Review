using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Arrieta_Code_Review
{
    public class JobLogger
    {
        private static bool _logToFile = true;
        private static bool _logToConsole = true;
        private static bool _logToDatabase = true;
        private static bool _logMessage = true;
        private static bool _logWarning = true;
        private static bool _logError = true;
        
        public enum MessageType
        {
            Message = 1,
            Error = 2,
            Warning = 3
        }
        
        public static void ConfigJobLogger(bool bLogToFile, bool bLogToConsole, bool bLogToDatabase,
            bool bLogMessage, bool bLogWarning, bool bLogError)
        {
            _logToDatabase = bLogToDatabase;
            _logToFile = bLogToFile;
            _logToConsole = bLogToConsole;
            _logMessage = bLogMessage;
            _logWarning = bLogWarning;
            _logError = bLogError;
        }
        
        public static void LogMessage(string sMessage, MessageType messageType)
        {
            if (sMessage == null || sMessage.Length == 0) { return; }
                sMessage.Trim();           
            
            if (_logToDatabase) LogToDB(sMessage, messageType);
            
            if (_logToFile) LogToText(sMessage, messageType);
            
            if (_logToConsole) LogToConsole(sMessage, messageType);
        }
        
        protected static bool MustSaveByType(MessageType messageType)
        {
            return
                (_logMessage && messageType == MessageType.Message) ||
                (_logWarning && messageType == MessageType.Warning) ||
                (_logError && messageType == MessageType.Error);
        }
        
        protected static void LogToDB(string sMessage, MessageType messageType)
        {
            if (!MustSaveByType(messageType))
                return;

            int iMessageType = (int)messageType;
            string sQuery = "Insert into Log Values('" + sMessage + "', "
                + iMessageType.ToString() + ",'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + "')";
            CNN.Crud.ExeQuery(sQuery);
        }

        protected static void LogToText(string sMessage, MessageType messageType)
        {
            if (!MustSaveByType(messageType))
                return;

            string sDirectory = ConfigurationManager.AppSettings["LogFileDirectory"];

            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            string sFileName = sDirectory + "LogFile_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            string sNewLog = string.Format("\n{0} - {1} : {2}", messageType.ToString(), DateTime.Now, sMessage);
            
            File.AppendAllText(sFileName, sNewLog);
        }
        
        protected static void LogToConsole(string sMessage, MessageType messageType)
        {
            if (!MustSaveByType(messageType))
                return;

            ConsoleColor cColor = new ConsoleColor();

            switch (messageType)
            {
                case MessageType.Message:
                    cColor = ConsoleColor.Red;
                    break;
                case MessageType.Error:
                    cColor = ConsoleColor.Yellow;
                    break;
                case MessageType.Warning:
                    cColor = ConsoleColor.White;
                    break;
            }

            Console.ForegroundColor = cColor;
            Console.WriteLine(string.Format("{0} - {1} : {2}", messageType.ToString(), DateTime.Now, sMessage));
        }
    }
}
