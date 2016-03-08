using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace LyncLogger
{
    class Program
    {
        private static LyncConnection connection;
        public static readonly String Version = "1.3.1";
        
        private static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8DE-65F04E6BDE8F}");
        //[STAThread] // makes lync connection missbehave

        static void Main(string[] args)
        {
            try
            {
                if (mutex.WaitOne(TimeSpan.Zero, true))
                {
                    connection = new LyncConnection();

                    var form = new MainView(connection);
                    AppLogger.GetInstance().onInfo += form.Log;

                    connection.logger.onLog += form.AddConversationFiles;
                    connection.watchConnection();

                    Application.EnableVisualStyles();
                    Application.Run(form);

                    Console.ReadLine();
                }
                else
                {
                    MessageBox.Show("Lync Logger is already running");
                }
            }
            catch (Exception e)
            {
                HandleFatal(e);
            }
        }

        public static void HandleFatal(Exception e)
        {
            MessageBox.Show("The application crashed!\nHere are more details: " + e.Message + "\n\n" + e.StackTrace, "You broke it :(", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            System.Windows.Forms.Application.Exit();
            System.Environment.Exit(1);
        }
    }
}
