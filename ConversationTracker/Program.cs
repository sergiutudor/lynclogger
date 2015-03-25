using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace LyncLogger
{
    class Program
    {
        private static LyncConnection Connection;
        public static readonly String Version = "1.1.1";
        
        private static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8DE-65F04E6BDE8F}");
        //[STAThread] // makes lync connection missbehave

        static void Main(string[] args)
        {
            if(mutex.WaitOne(TimeSpan.Zero, true)) {
                var form = new MainView();
                AppLogger.GetInstance().onInfo += form.Log;

                Connection = new LyncConnection();
                Connection.logger.onLog += form.AddConversationFiles;

                Application.EnableVisualStyles();
                Application.Run(form);

                Console.ReadLine();
            }
            else
            {
                MessageBox.Show("Lync Logger is already running");
            }
        }
       
    }
}
