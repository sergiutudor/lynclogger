using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Lync.Model;
using System.Threading;
using System.Text.RegularExpressions;

namespace LyncLogger
{
    public partial class MainView : Form
    {
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);

        private const uint SW_RESTORE = 0x09;

        private ContactsManager contactsManager;

        private Dictionary<int, Contact> displayedContacts = new Dictionary<int, Contact>();
        private List<Contact> watchedContacts = new List<Contact>();
        LyncConnection Connection;

        public MainView(LyncConnection Connection)
        {
            InitializeComponent();
            Resize += MainView_Resize;
            minTray.Click += MinimizeTotray;
            LofFol.Click += openLogsFolder;
            FormClosing += onClose;
            searchBox.KeyUp += onSearch;
            searchNext.Click += onNextSearch;
            searchPrev.Click += onPrevSearch;
            logView.onScroll += onLogScroll;
            LyncLogger.DoubleClick += trayDoubleClick;

            LogBoxRich.ReadOnly = true;

            Text += " V" + Program.Version;

            contactsManager = new ContactsManager(Connection);
            this.Connection = Connection;

            //Display conversation history
            displayConversationHistory(ConversationLogger.getConversationsFiles());

            Timer.SetInterval(delegate { checkWatched(); }, 5000);
        }
        
        private void onLogScroll(object sender, EventArgs e)
        {
            if (logView.VerticalPosition < 300)
            {
                logRenderer renderer = logRenderer.getInstance(this.logView, Connection.getCurrentUserName());
                renderer.loadRowBatch();
            }
        }

        private void onNextSearch(Object sender, EventArgs args)
        {
            logRenderer renderer = logRenderer.getInstance(this.logView, Connection.getCurrentUserName());
            renderer.shiftMatch(1);
        }

        private void onPrevSearch(Object sender, EventArgs args)
        {
            logRenderer renderer = logRenderer.getInstance(this.logView, Connection.getCurrentUserName());
            renderer.shiftMatch(-1);
        }

        private void onSearch(Object sender, KeyEventArgs args)
        {
            logRenderer renderer = logRenderer.getInstance(this.logView, Connection.getCurrentUserName());
            renderer.search(searchBox.Text);
        }

        private void checkWatched()
        {
            int key;
            for (key = 0; key < watchedContacts.Count; key++)
            {
                Contact contact = watchedContacts[key];
                if (contactsManager.isContactAvailable(contact))
                {
                    removeWatchedContact(key);

                    String displayName = contact.GetContactInformation(ContactInformationType.DisplayName).ToString();
                    MessageBox.Show(displayName + " is now available for discussions.", "Contact available", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                    AppLogger.GetInstance().Info(contact.Uri + " appeared online");

                    return;
                }
            }
        }

        private void fillContactList()
        {
            var contacts = contactsManager.getAllContacts();
            SortedDictionary<String, Contact > sortedContacts = new SortedDictionary<String, Contact>();
            
            foreach (Contact contact in contacts)
            {
                sortedContacts.Add(contact.GetContactInformation(ContactInformationType.DisplayName).ToString(), contact);
            }

            lstContacts.Items.Clear();
            displayedContacts.Clear();

            foreach (var nameContact in sortedContacts)
            {
                var index = lstContacts.Items.Add(nameContact.Key);
                displayedContacts.Add(index, nameContact.Value);
            }
        }

        private void fillContactListThreadSafe()
        {
            if (lstContacts.InvokeRequired)
            {
                lstContacts.Invoke(new MethodInvoker(fillContactList));
            }
            else
            {
                fillContactList();
            }
        }

        private void tryFillCOntactList()
        {
            try
            {
                fillContactListThreadSafe();
            }
            catch (Exception e)
            {
                Timer.SetTimeout(tryFillCOntactList, 1000);
            }
        }

        private void trayDoubleClick(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Show();
                ShowWindow(this.Handle, SW_RESTORE);
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                MinimizeTotray(null, null);
            }
        }

        private void onClose(object sender, FormClosingEventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to close Lync Logger?",
                                     "Confirm Exit",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // If 'Yes', do something here.
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void MainView_Load(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void MinimizeTotray(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void openLogsFolder(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", ConversationLogger.getLogFolder());
        }

        private void MainView_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                LyncLogger.Visible = true;
                LyncLogger.ShowBalloonTip(500);
                this.Hide();
            }

            logView.Width = Width - 500;
            logView.Height = Height - 122;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void Log(Object source, LoggerEventArgs args)
        {

            if (LogBoxRich.InvokeRequired)
            {
                LogBoxRich.Invoke(new MethodInvoker(delegate { addMessge(args.message); }));
            }
            else
            {
                addMessge(args.message);
            }
        }

        public void AddConversationFiles(Object source, FileLoggerEventArgs args)
        {
            if (lstFiles.InvokeRequired)
            {
                lstFiles.Invoke(new MethodInvoker(delegate { displayConversationHistory(ConversationLogger.getConversationsFiles()); }));
            }
            else
            {
                displayConversationHistory(ConversationLogger.getConversationsFiles());
            }
        }

        private void addMessge(string mes)
        {
            var message = "\r\n" + mes;
            LogBoxRich.Text += message;

            //Scroll to end
            LogBoxRich.SelectionStart = LogBoxRich.Text.Length;
            LogBoxRich.ScrollToCaret();
        }

        private void displayConversationHistory(Array files)
        {
            String perfix = ConversationLogger.logFilePrefix;

            lstFiles.Items.Clear();

            if (files == null) // nofiles to list
            {
                return;
            }

            foreach (FileInfo file in files)
            {
                var contact = file.Name.Replace(perfix, "");
                contact = contact.Replace(ConversationLogger.logFileExtension, "");

                String name = "";
                foreach (char currentChar in contact)
                {
                    if (Char.IsUpper(currentChar))
                    {
                        name += " " + currentChar;
                        continue;
                    }
                    name += currentChar;
                }

                lstFiles.Items.Add(name);
            }
        }

        private void lstFiles_openFile(object sender, EventArgs e)
        {
            String logFolder = ConversationLogger.getLogFolder();
            String filePrefix = ConversationLogger.logFilePrefix;

            string filePath = logFolder + filePrefix + lstFiles.SelectedItem.ToString().Replace(" ", "") + ConversationLogger.logFileExtension;
            if (!File.Exists(filePath))
            {
                System.Windows.Forms.MessageBox.Show("File doesn\'t exists");
                return;
            }

            System.Diagnostics.Process.Start(filePath);
        }

        private void lstFiles_MouseClick(object sender, EventArgs e)
        {
            String logFolder = ConversationLogger.getLogFolder();
            String filePrefix = ConversationLogger.logFilePrefix;

            string filePath = logFolder + filePrefix + lstFiles.SelectedItem.ToString().Replace(" ", "") + ConversationLogger.logFileExtension;
            logRenderer renderer = logRenderer.getInstance(this.logView, Connection.getCurrentUserName());
            renderer.processFile(filePath);
        }
        
        private void lstContacts_MouseDoubleClick(object sender, EventArgs e)
        {
            if (lstContacts.SelectedIndex == -1)
            {
                return;
            }

            Contact contact = displayedContacts[lstContacts.SelectedIndex];
            if (watchedContacts.Contains(contact))
            {
                return;
            }
            watchedContacts.Add(contact);
            refreshWatched();
            AppLogger.GetInstance().Info(contact.Uri + " added to watch list");
        }

        private void refreshWatched()
        {
            lstWatched.Items.Clear();
            foreach (Contact contact in watchedContacts)
            {
                lstWatched.Items.Add(contact.GetContactInformation(ContactInformationType.DisplayName).ToString());
            }
        }

        private void refreshWatchedThreadSafe()
        {
            if (lstWatched.InvokeRequired)
            {
                lstWatched.Invoke(new MethodInvoker(refreshWatched));
            }
            else
            {
                refreshWatched();
            }
        }

        private void lstWatched_MouseDoubleClick(object sender, EventArgs e)
        {
            if (lstWatched.SelectedIndex == -1)
            {
                return;
            }
            removeWatchedContact(lstWatched.SelectedIndex);
        }

        private void removeWatchedContact(int index)
        {
            watchedContacts.RemoveAt(index);
            refreshWatchedThreadSafe();
        }

        private void contactsRefreshIcon_Click(object sender, EventArgs e)
        {
            tryFillCOntactList();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
