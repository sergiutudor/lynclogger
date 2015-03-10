﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncLogger
{
    public class LoggerEventArgs : EventArgs
    {
        public string message;
    }

    class AppLogger
    {
        private static string userHome = System.IO.Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).FullName;
        private static string logFileDir = "\\lync\\";
        private string logFileName = "LyncLogger";
        private static string logFileExtension = ".log";
        private static AppLogger instance;
        private string LastMessage;
        private int LastMessageTimes = 0;
        public EventHandler<LoggerEventArgs> onInfo;

        public static AppLogger GetInstance()
        {
            if (instance == null)
            {
                instance = new AppLogger();
            }

            return instance;
        }

        private AppLogger()
        {
            createPath();
        }

        private bool log(string message)
        {
            if(message == LastMessage){
                LastMessageTimes++;
                return false;
            }

            if (LastMessageTimes>0)
            {
                var msg = "   OCCURRED " + (LastMessageTimes + 1) + " TIMES";
                LastMessageTimes = 0;
                log(msg);
            }

            LastMessage = message;
            string messageToLog = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   " + message;
            string file = getFileName();
            System.IO.File.AppendAllText(@file, messageToLog+"\r\n");

            Console.WriteLine(messageToLog);
            return true;
        }

        public void Exception(Exception e)
        {
            string message = "EXCEPTION: " + e.Message;
            message += "\r\n    SOURCE: " + e.Source;
            message += "\r\n    STACK: " + e.StackTrace;
            log(message);
        }

        public void Info(string message)
        {
            message = "INFO: " + message;
            if (log(message))
            {
                triggerLogEvent(message);
            }
        }

        private void triggerLogEvent(string message)
        {
            try
            {
                LoggerEventArgs args = new LoggerEventArgs();
                args.message = message;
                EventHandler<LoggerEventArgs> handler = onInfo;
                if (handler != null)
                {
                    handler(this, args);
                }
            }
            catch(Exception e){
                Exception(e);
            }
        }

        private static void createPath()
        {
            char[] glue = new char[1];
            glue[0] = '\\';
            String[] folders = logFileDir.Split(glue, StringSplitOptions.RemoveEmptyEntries);

            string currentFolder = userHome;
            for (int i = 0; i < folders.Length; i++)
            {
                currentFolder += "\\" + folders[i];
                System.IO.Directory.CreateDirectory(currentFolder);
            }
        }

        private string getFileName()
        {
            return userHome + logFileDir + logFileName + logFileExtension;
        }
    }
}
