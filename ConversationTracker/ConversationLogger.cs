using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using System.Text.RegularExpressions;

namespace LyncLogger
{
    class ConversationLogger : IDisposable
    {
        private Microsoft.Lync.Model.Conversation.Conversation conversation;
        private static string userHome = System.IO.Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).FullName;
        private static string logFileDir = "\\lync\\logs\\";
        public static readonly string logFilePrefix = "log-";
        private string logFileName = "";
        private static string logFileExtension = ".txt";
        private string ConversationId;
        private Dictionary<String, String> AllNames = new Dictionary<String, String>();
        FileLogger logger = new FileLogger();

        private static Dictionary<String, ConversationLogger> Instances = new Dictionary<String, ConversationLogger>();

        public ConversationLogger(Microsoft.Lync.Model.Conversation.Conversation conversation, FileLogger logger)
        {
            this.logger = logger;

            try
            {
                ConversationId = conversation.Properties[ConversationProperty.Id].ToString();
                Instances[ConversationId] = this;

                AppLogger.GetInstance().Info("new conversation started: " + ConversationId);
                this.conversation = conversation;

                conversation.ParticipantAdded += Conversation_ParticipantAdded;
                conversation.ParticipantRemoved += Conversation_ParticipantRemoved;

                ((InstantMessageModality)conversation.Modalities[ModalityTypes.InstantMessage]).InstantMessageReceived += myInstantMessageModality_MessageReceived;
            }
            catch (Exception e)
            {
                AppLogger.GetInstance().Exception(e);
            }
        }

        public static ConversationLogger getInstance(Microsoft.Lync.Model.Conversation.Conversation conversation)
        {
            var id = conversation.Properties[ConversationProperty.Id].ToString();
            if (Instances.ContainsKey(id))
            {
                return Instances[id];
            }

            return null;
        }

        public static Array getConversationsFiles()
        {
            DirectoryInfo info = new DirectoryInfo(getLogFolder());
            FileInfo[] files = info.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();

            return files;
        }

        public void EndConversation()
        {
            AppLogger.GetInstance().Info("conversation ended: " + ConversationId);
            logger.Log("-------------------- Conversation Ended --------------------\r\n", getFileName());
            if (Instances.ContainsKey(ConversationId))
            {
                Instances.Remove(ConversationId);
            }
        }

        /// <summary>
        /// ParticipantAdded callback handles ParticpantAdded event raised by Conversation
        /// </summary>
        /// <param name="source">Conversation Source conversation.</param>
        /// <param name="data">ParticpantCollectionEventArgs Event data</param>
        void Conversation_ParticipantRemoved(Object source, ParticipantCollectionChangedEventArgs data)
        {
            try
            {
                if (data.Participant.IsSelf == false)
                {
                    AppLogger.GetInstance().Info("participant added " + data.Participant.Contact.Uri);

                    // update log file name
                    string userName = (string)data.Participant.Contact.GetContactInformation(ContactInformationType.DisplayName);


                    if (!AllNames.ContainsKey(userName))
                    {
                        AppLogger.GetInstance().Info(userName + " does not exist in conversation");
                        return;
                    }

                    if (AllNames.Count() > 1)
                    {
                        logger.Log("-------------------- Removed  " + userName + " from conversation --------------------\r\n", getFileName());
                    }

                    AllNames.Remove(userName);
                    UpdateLogFileName();

                    if (AllNames.Count() > 1)
                    {
                        logger.Log("-------------------- Started Conversation with  " + string.Join(", ", AllNames.Values.ToArray()) + " --------------------\r\n", getFileName());
                    }
                }
            }
            catch (Exception e)
            {
                AppLogger.GetInstance().Exception(e);
            }
        }

        /// <summary>
        /// ParticipantAdded callback handles ParticpantAdded event raised by Conversation
        /// </summary>
        /// <param name="source">Conversation Source conversation.</param>
        /// <param name="data">ParticpantCollectionEventArgs Event data</param>
        void Conversation_ParticipantAdded(Object source, ParticipantCollectionChangedEventArgs data)
        {
            try
            {
                if (data.Participant.IsSelf == false &&
                    ((Conversation)source).Modalities.ContainsKey(ModalityTypes.InstantMessage))
                {
                    AppLogger.GetInstance().Info("participant added "+data.Participant.Contact.Uri);

                    // update log file name
                    string userName = (string)data.Participant.Contact.GetContactInformation(ContactInformationType.DisplayName);
                    if (AllNames.ContainsKey(userName))
                    {
                        AppLogger.GetInstance().Info(userName + " already added in conversation");
                        return;
                    }

                    if (AllNames.Count() > 0)
                    {
                        logger.Log("-------------------- Added  " + userName + " to conversation --------------------\r\n", getFileName());
                    }

                    AllNames.Add(userName, userName);
                    UpdateLogFileName();

                    logger.Log("-------------------- Started Conversation with  " + string.Join(", ", AllNames.Values.ToArray()) + " --------------------\r\n", getFileName());

                    ((InstantMessageModality)data.Participant.Modalities[ModalityTypes.InstantMessage]).InstantMessageReceived += myInstantMessageModality_MessageReceived;
                }
            }
            catch(Exception e){
                AppLogger.GetInstance().Exception(e);
            }
        }

        private void UpdateLogFileName()
        {
            string tmp = string.Join("%", AllNames.Values.ToArray());
            tmp = Regex.Replace(tmp, @"[^a-zA-Z0-9%]", "");
            tmp = Regex.Replace(tmp, @"%", "-");
            logFileName = tmp;
        }

        public static string getLogFolder()
        {
            return userHome + logFileDir;
        }

        private string getFileName()
        {
            return getLogFolder() + logFilePrefix + logFileName + logFileExtension;
        }

        /// <summary>
        /// Callback is invoked when IM Modality state changes upon receipt of message
        /// </summary>
        /// <param name="source">InstantMessageModality Modality </param>
        /// <param name="data">SendMessageEventArgs The new message.</param>
        void myInstantMessageModality_MessageReceived(Object source, MessageSentEventArgs data)
        {
            try
            {
                IDictionary<InstantMessageContentType, string> messageFormatProperty = data.Contents;

                if (messageFormatProperty.ContainsKey(InstantMessageContentType.PlainText) || messageFormatProperty.ContainsKey(InstantMessageContentType.RichText))
                {
                    string outVal = data.Text;
                    string Sender = (string)((InstantMessageModality)source).Participant.Contact.GetContactInformation(ContactInformationType.DisplayName);

                    string messageToLog = Sender + ": " + outVal;
                    messageToLog = messageToLog.Trim();
                    logger.Log(messageToLog, getFileName());
                }
            }
            catch(Exception e){
                AppLogger.GetInstance().Exception(e);
            }
        }

        public void Dispose()
        {
            // TODO: implement dispoze
        }
    }
}
