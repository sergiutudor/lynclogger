using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LyncLogger
{
    public partial class MainView : Form
    {
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);

        private const uint SW_RESTORE = 0x09;

        public MainView()
        {
            InitializeComponent();
            Resize += MainView_Resize;
            LyncLogger.DoubleClick += trayDoubleClick;
            minTray.Click += MinimizeTotray;
            LofFol.Click += openLogsFolder;
            FormClosing += onClose;

            MaximizeBox = false;

            LogBoxRich.ReadOnly = true;

            this.Text += " V"+Program.Version;
        }

        private void trayDoubleClick(object sender, EventArgs e)
        {
            this.Show();
            ShowWindow(this.Handle, SW_RESTORE);
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
            else if (FormWindowState.Normal == this.WindowState)
            {
                LyncLogger.Visible = false;
            }
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

        private void addMessge(string mes)
        {
            var message = "\r\n" + mes;
            LogBoxRich.Text += message;

            //Scroll to end
            LogBoxRich.SelectionStart = LogBoxRich.Text.Length;
            LogBoxRich.ScrollToCaret();
        }
    }
}
