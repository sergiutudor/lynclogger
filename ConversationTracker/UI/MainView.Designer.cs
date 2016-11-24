namespace LyncLogger
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.LyncLogger = new System.Windows.Forms.NotifyIcon(this.components);
            this.minTray = new System.Windows.Forms.Button();
            this.LofFol = new System.Windows.Forms.Button();
            this.LogBoxRich = new System.Windows.Forms.RichTextBox();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstContacts = new System.Windows.Forms.ListBox();
            this.lstWatched = new System.Windows.Forms.ListBox();
            this.refreshContactsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contactsRefreshIcon = new System.Windows.Forms.PictureBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.resultCount = new System.Windows.Forms.Label();
            this.logView = new LyncLogger.ScrollDetectRTF();
            this.searchPrev = new System.Windows.Forms.Button();
            this.searchNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.contactsRefreshIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // LyncLogger
            // 
            this.LyncLogger.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.LyncLogger.BalloonTipText = "Lync Logger minimized";
            this.LyncLogger.BalloonTipTitle = "Lync Logger";
            this.LyncLogger.Icon = ((System.Drawing.Icon)(resources.GetObject("LyncLogger.Icon")));
            this.LyncLogger.Text = "Lync Logger";
            this.LyncLogger.Visible = true;
            this.LyncLogger.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // minTray
            // 
            this.minTray.Location = new System.Drawing.Point(52, 508);
            this.minTray.Margin = new System.Windows.Forms.Padding(4);
            this.minTray.Name = "minTray";
            this.minTray.Size = new System.Drawing.Size(145, 28);
            this.minTray.TabIndex = 1;
            this.minTray.Text = "Minimize to tray";
            this.minTray.UseVisualStyleBackColor = true;
            // 
            // LofFol
            // 
            this.LofFol.Location = new System.Drawing.Point(392, 508);
            this.LofFol.Margin = new System.Windows.Forms.Padding(4);
            this.LofFol.Name = "LofFol";
            this.LofFol.Size = new System.Drawing.Size(144, 28);
            this.LofFol.TabIndex = 2;
            this.LofFol.Text = "Open logs folder";
            this.LofFol.UseVisualStyleBackColor = true;
            // 
            // LogBoxRich
            // 
            this.LogBoxRich.Location = new System.Drawing.Point(16, 425);
            this.LogBoxRich.Margin = new System.Windows.Forms.Padding(4);
            this.LogBoxRich.Name = "LogBoxRich";
            this.LogBoxRich.Size = new System.Drawing.Size(570, 66);
            this.LogBoxRich.TabIndex = 3;
            this.LogBoxRich.Text = "";
            // 
            // lstFiles
            // 
            this.lstFiles.ItemHeight = 16;
            this.lstFiles.Location = new System.Drawing.Point(16, 53);
            this.lstFiles.Margin = new System.Windows.Forms.Padding(4);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(255, 340);
            this.lstFiles.TabIndex = 5;
            this.lstFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstFiles_MouseClick);
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Recent Conversations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(326, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "All Contacts";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(326, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Watched Contacts";
            // 
            // lstContacts
            // 
            this.lstContacts.ItemHeight = 16;
            this.lstContacts.Location = new System.Drawing.Point(331, 53);
            this.lstContacts.Margin = new System.Windows.Forms.Padding(4);
            this.lstContacts.Name = "lstContacts";
            this.lstContacts.Size = new System.Drawing.Size(255, 180);
            this.lstContacts.TabIndex = 9;
            this.lstContacts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstContacts_MouseDoubleClick);
            // 
            // lstWatched
            // 
            this.lstWatched.ItemHeight = 16;
            this.lstWatched.Location = new System.Drawing.Point(331, 277);
            this.lstWatched.Margin = new System.Windows.Forms.Padding(4);
            this.lstWatched.Name = "lstWatched";
            this.lstWatched.Size = new System.Drawing.Size(255, 116);
            this.lstWatched.TabIndex = 10;
            this.lstWatched.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstWatched_MouseDoubleClick);
            // 
            // refreshContactsToolTip
            // 
            this.refreshContactsToolTip.AutoPopDelay = 5000;
            this.refreshContactsToolTip.InitialDelay = 500;
            this.refreshContactsToolTip.ReshowDelay = 100;
            this.refreshContactsToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.refreshContactsToolTip.ToolTipTitle = "Refresh";
            // 
            // contactsRefreshIcon
            // 
            this.contactsRefreshIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.contactsRefreshIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.contactsRefreshIcon.Image = global::LyncLogger.Properties.Resources.Refresh_icon;
            this.contactsRefreshIcon.Location = new System.Drawing.Point(529, 27);
            this.contactsRefreshIcon.Name = "contactsRefreshIcon";
            this.contactsRefreshIcon.Size = new System.Drawing.Size(20, 20);
            this.contactsRefreshIcon.TabIndex = 11;
            this.contactsRefreshIcon.TabStop = false;
            this.refreshContactsToolTip.SetToolTip(this.contactsRefreshIcon, "Refresh contacts list to see latest contacts.");
            this.contactsRefreshIcon.Click += new System.EventHandler(this.contactsRefreshIcon_Click);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(638, 53);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(214, 22);
            this.searchBox.TabIndex = 14;
            // 
            // resultCount
            // 
            this.resultCount.AutoSize = true;
            this.resultCount.Location = new System.Drawing.Point(932, 55);
            this.resultCount.Name = "resultCount";
            this.resultCount.Size = new System.Drawing.Size(114, 17);
            this.resultCount.TabIndex = 17;
            this.resultCount.Text = "X matches found";
            // 
            // logView
            // 
            this.logView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.logView.HorizontalPosition = 0;
            this.logView.Location = new System.Drawing.Point(638, 87);
            this.logView.Name = "logView";
            this.logView.ReadOnly = true;
            this.logView.Size = new System.Drawing.Size(691, 449);
            this.logView.TabIndex = 13;
            this.logView.Text = "";
            this.logView.VerticalPosition = 0;
            this.logView.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // searchPrev
            // 
            this.searchPrev.BackgroundImage = global::LyncLogger.Properties.Resources.prev_small;
            this.searchPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.searchPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchPrev.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.searchPrev.FlatAppearance.BorderSize = 0;
            this.searchPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchPrev.Location = new System.Drawing.Point(859, 49);
            this.searchPrev.Margin = new System.Windows.Forms.Padding(0);
            this.searchPrev.Name = "searchPrev";
            this.searchPrev.Size = new System.Drawing.Size(30, 30);
            this.searchPrev.TabIndex = 16;
            this.searchPrev.UseVisualStyleBackColor = true;
            this.searchPrev.Visible = false;
            // 
            // searchNext
            // 
            this.searchNext.BackgroundImage = global::LyncLogger.Properties.Resources.next_small;
            this.searchNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.searchNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchNext.FlatAppearance.BorderSize = 0;
            this.searchNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchNext.Location = new System.Drawing.Point(892, 49);
            this.searchNext.Name = "searchNext";
            this.searchNext.Size = new System.Drawing.Size(30, 30);
            this.searchNext.TabIndex = 15;
            this.searchNext.UseVisualStyleBackColor = true;
            this.searchNext.Visible = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 551);
            this.Controls.Add(this.resultCount);
            this.Controls.Add(this.searchPrev);
            this.Controls.Add(this.searchNext);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.logView);
            this.Controls.Add(this.contactsRefreshIcon);
            this.Controls.Add(this.lstWatched);
            this.Controls.Add(this.lstContacts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.LogBoxRich);
            this.Controls.Add(this.LofFol);
            this.Controls.Add(this.minTray);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainView";
            this.Text = "Lync Logger";
            this.Load += new System.EventHandler(this.MainView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.contactsRefreshIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon LyncLogger;
        private System.Windows.Forms.Button minTray;
        private System.Windows.Forms.Button LofFol;
        private System.Windows.Forms.RichTextBox LogBoxRich;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstContacts;
        private System.Windows.Forms.ListBox lstWatched;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox contactsRefreshIcon;
        private System.Windows.Forms.ToolTip refreshContactsToolTip;
        private ScrollDetectRTF logView;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchNext;
        private System.Windows.Forms.Button searchPrev;
        private System.Windows.Forms.Label resultCount;
    }
}