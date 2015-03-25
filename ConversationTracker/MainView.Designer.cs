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
            this.minTray.Location = new System.Drawing.Point(78, 303);
            this.minTray.Name = "minTray";
            this.minTray.Size = new System.Drawing.Size(109, 23);
            this.minTray.TabIndex = 1;
            this.minTray.Text = "Minimize to tray";
            this.minTray.UseVisualStyleBackColor = true;
            // 
            // LofFol
            // 
            this.LofFol.Location = new System.Drawing.Point(339, 303);
            this.LofFol.Name = "LofFol";
            this.LofFol.Size = new System.Drawing.Size(108, 23);
            this.LofFol.TabIndex = 2;
            this.LofFol.Text = "Open logs folder";
            this.LofFol.UseVisualStyleBackColor = true;
            // 
            // LogBoxRich
            // 
            this.LogBoxRich.Location = new System.Drawing.Point(12, 12);
            this.LogBoxRich.Name = "LogBoxRich";
            this.LogBoxRich.Size = new System.Drawing.Size(520, 278);
            this.LogBoxRich.TabIndex = 3;
            this.LogBoxRich.Text = "";
            // 
            // lstFiles
            // 
            this.lstFiles.Location = new System.Drawing.Point(538, 12);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(192, 277);
            this.lstFiles.TabIndex = 5;
            this.lstFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFiles_MouseDoubleClick);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 338);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.LogBoxRich);
            this.Controls.Add(this.LofFol);
            this.Controls.Add(this.minTray);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "Lync Logger";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon LyncLogger;
        private System.Windows.Forms.Button minTray;
        private System.Windows.Forms.Button LofFol;
        private System.Windows.Forms.RichTextBox LogBoxRich;
        private System.Windows.Forms.ListBox lstFiles;
    }
}