using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncLogger.config
{
    class debug : config
    {

        public debug() : base()
        {
            appLogDir = "\\lync-debug\\";
            appLogFile = "LyncLogger";
            conversationLogDir = "\\lync-debug\\logs\\";
        }
    }
}
