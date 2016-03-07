using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LyncLogger
{
    public class LyncConnection : IDisposable
    {
        private bool connectionActive = false;
        private LyncClient client;
        public readonly FileLogger logger = new FileLogger();

        public LyncConnection()
        {
        }

        public void watchConnection()
        {
            if (!connectionActive)
            {
                attachListeners();
            }
            else
            {
                try
                {
                    if (ClientState.Invalid == client.State)
                    {
                        disconnect();
                    }
                }
                catch(Exception e)
                {
                    disconnect();
                    AppLogger.GetInstance().Exception(e);
                }
            }

            Timer.SetTimeout(() =>
            {
                watchConnection();
            }, 1000);
        }

        void disconnect()
        {
            if (!connectionActive)
            {
                return;
            }

            AppLogger.GetInstance().Info("lync connection loast");
            client = null;
            connectionActive = false;
        }

        void onDisconnect(Object sender, EventArgs e)
        {
            disconnect();
        }

        void onStateChanged(Object sender, ClientStateChangedEventArgs e)
        {
            AppLogger.GetInstance().Info("lync state changed: " + e.StatusCode);
            if (e.StatusCode == -1)
            {
                disconnect();
            }
        }

        private void attachListeners()
        {
            try
            {
                client = LyncClient.GetClient();
                client.ConversationManager.ConversationAdded += ConversationManager_ConversationAdded;
                client.ConversationManager.ConversationRemoved += ConversationManager_ConversationRemoved;
                client.StateChanged += onStateChanged;
                client.ClientDisconnected += onDisconnect;

                connectionActive = true;
                AppLogger.GetInstance().Info("new conversation listeners attached");
            }
            catch (ClientNotFoundException) // it needs to be decleared so that this does not enter all times
            {
                AppLogger.GetInstance().Info("Lynoc not started");
            }
            catch (Exception e)
            {
                AppLogger.GetInstance().Exception(e);
                MessageBox.Show("The application crashed!\nYou can find more details in: "+AppLogger.GetInstance().getFileName(), "You broke it :(", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                System.Windows.Forms.Application.Exit();
                System.Environment.Exit(1);
            }
        }

        void ConversationManager_ConversationAdded(object sender, Microsoft.Lync.Model.Conversation.ConversationManagerEventArgs e)
        {
            new ConversationLogger(e.Conversation, logger);
        }

        void ConversationManager_ConversationRemoved(object sender, Microsoft.Lync.Model.Conversation.ConversationManagerEventArgs e)
        {
            ConversationLogger logger = ConversationLogger.getInstance(e.Conversation);
            if(logger != null)
            {
                logger.EndConversation();
                logger.Dispose();
            }
        }

        public LyncClient getLyncClient()
        {
            return client;
        }

        public void Dispose()
        {
        }
    }
}
