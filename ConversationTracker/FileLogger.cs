using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncLogger
{
    public class FileLoggerEventArgs : EventArgs
    {
        public Array files;
    }

    public class FileLogger
    {
        private static String folderSeparator = "\\";
        public EventHandler<FileLoggerEventArgs> onLog;

        public void Log(string message, string file, bool addTimestamp)
        {
            try
            {
                if(!System.IO.File.Exists(file)){
                    createPath(file);
                }

                message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   " + message + "\r\n";
                System.IO.File.AppendAllText(@file, message);
            }
            catch (Exception e)
            {
                logException(e);
            }

            triggerFileLogEvent();
        }

        private void logException(Exception e)
        {
                string exMsg = "EXCEPTION: " + e.Message;
                exMsg += "\r\n    SOURCE: " + e.Source;
                exMsg += "\r\n    STACK: " + e.StackTrace;
                Console.WriteLine(exMsg);
        }

        public void Log(string message, string file)
        {
            Log(message, file, true);
        }


        private void createPath(String logFile)
        {
            try
            {
                String[] glue = new String[1];
                glue[0] = folderSeparator;
                String[] folders = logFile.Split(glue, StringSplitOptions.RemoveEmptyEntries);

                string currentFolder;
                List<String> FolderBits= new List<String>();

                for (int i = 0; i < folders.Length - 1; i++)
                {
                    FolderBits.Add(folders[i]);
                    currentFolder = String.Join(folderSeparator, FolderBits);
                    if (System.IO.Directory.Exists(currentFolder))
                    {
                        continue;
                    }
                    System.IO.Directory.CreateDirectory(currentFolder);
                }
            }
            catch (Exception e)
            {
                logException(e);
            }
        }

        private void triggerFileLogEvent()
        {
            try
            {
                FileLoggerEventArgs args = new FileLoggerEventArgs();

                if (onLog != null)
                {
                    onLog(this, args);
                }
            }
            catch (Exception e)
            {
                logException(e);
            }
        }
    }
}
