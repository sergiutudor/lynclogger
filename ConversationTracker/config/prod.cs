using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncLogger.config
{
    class prod : config
    {
        public prod() : base()
        {
            appLogDir = "\\lync\\";
            appLogFile = "LyncLogger";
            conversationLogDir = "\\lync\\logs\\";
        }
    }
}
