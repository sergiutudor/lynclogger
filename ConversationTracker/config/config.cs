using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncLogger.config
{
    abstract class config
    {
        protected static config instance;

        protected string appLogDir;
        protected string appLogFile;
        protected string conversationLogDir;

        protected config() { }

        static Boolean isDebugMode = false;

        public string getAppLogDir()
        {
            return appLogDir;
        }

        public string getAppLogFile()
        {
            return appLogFile;
        }

        public string getConversationLogDir()
        {
            return conversationLogDir;
        }

        public Boolean getIsDebugMode()
        {
            return isDebugMode;
        }

        public static config getInstance()
        {
#if DEBUG
            isDebugMode = true;
#endif

            if (instance == null)
            {
                if (isDebugMode)
                {
                    instance = new debug();
                }
                else
                {
                    instance = new prod();
                }
            }

            return instance;
        }
    }
}
