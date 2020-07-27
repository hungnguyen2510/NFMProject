namespace NFMProject
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPathWatching = new System.Windows.Forms.TextBox();
            this.systemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnWatching = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnRefeshLog = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnOpenFolderFileLog = new System.Windows.Forms.Button();
            this.picCheck = new System.Windows.Forms.PictureBox();
            this.btnOpenFolderFileWatching = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path Watching";
            // 
            // txtPathWatching
            // 
            this.txtPathWatching.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPathWatching.Location = new System.Drawing.Point(101, 21);
            this.txtPathWatching.Name = "txtPathWatching";
            this.txtPathWatching.ReadOnly = true;
            this.txtPathWatching.Size = new System.Drawing.Size(105, 20);
            this.txtPathWatching.TabIndex = 2;
            // 
            // systemTray
            // 
            this.systemTray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.systemTray.ContextMenuStrip = this.contextMenuStrip1;
            this.systemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTray.Icon")));
            this.systemTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.systemTray_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // btnWatching
            // 
            this.btnWatching.Location = new System.Drawing.Point(19, 51);
            this.btnWatching.Name = "btnWatching";
            this.btnWatching.Size = new System.Drawing.Size(126, 23);
            this.btnWatching.TabIndex = 1;
            this.btnWatching.Text = "Watching";
            this.btnWatching.UseVisualStyleBackColor = true;
            this.btnWatching.Click += new System.EventHandler(this.btnWatching_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLog);
            this.groupBox1.Location = new System.Drawing.Point(12, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(655, 327);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logs";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.WindowText;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.SystemColors.Window;
            this.txtLog.Location = new System.Drawing.Point(7, 20);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(642, 301);
            this.txtLog.TabIndex = 2;
            this.txtLog.Enter += new System.EventHandler(this.txtLog_Enter);
            // 
            // btnRefeshLog
            // 
            this.btnRefeshLog.Location = new System.Drawing.Point(151, 51);
            this.btnRefeshLog.Name = "btnRefeshLog";
            this.btnRefeshLog.Size = new System.Drawing.Size(126, 23);
            this.btnRefeshLog.TabIndex = 4;
            this.btnRefeshLog.Text = "RefeshLog";
            this.btnRefeshLog.UseVisualStyleBackColor = true;
            this.btnRefeshLog.Click += new System.EventHandler(this.btnRefeshLog_Click);
            // 
            // btnOpenFolderFileLog
            // 
            this.btnOpenFolderFileLog.Location = new System.Drawing.Point(283, 51);
            this.btnOpenFolderFileLog.Name = "btnOpenFolderFileLog";
            this.btnOpenFolderFileLog.Size = new System.Drawing.Size(126, 23);
            this.btnOpenFolderFileLog.TabIndex = 5;
            this.btnOpenFolderFileLog.Text = "Open Folder FL";
            this.btnOpenFolderFileLog.UseVisualStyleBackColor = true;
            this.btnOpenFolderFileLog.Click += new System.EventHandler(this.btnOpenFolderFileLog_Click);
            // 
            // picCheck
            // 
            this.picCheck.Image = ((System.Drawing.Image)(resources.GetObject("picCheck.Image")));
            this.picCheck.Location = new System.Drawing.Point(631, 11);
            this.picCheck.Name = "picCheck";
            this.picCheck.Size = new System.Drawing.Size(36, 34);
            this.picCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCheck.TabIndex = 6;
            this.picCheck.TabStop = false;
            this.picCheck.Visible = false;
            // 
            // btnOpenFolderFileWatching
            // 
            this.btnOpenFolderFileWatching.Location = new System.Drawing.Point(415, 51);
            this.btnOpenFolderFileWatching.Name = "btnOpenFolderFileWatching";
            this.btnOpenFolderFileWatching.Size = new System.Drawing.Size(126, 23);
            this.btnOpenFolderFileWatching.TabIndex = 7;
            this.btnOpenFolderFileWatching.Text = "Open Folder FW";
            this.btnOpenFolderFileWatching.UseVisualStyleBackColor = true;
            this.btnOpenFolderFileWatching.Click += new System.EventHandler(this.btnOpenFolderFileWatching_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 415);
            this.Controls.Add(this.btnOpenFolderFileWatching);
            this.Controls.Add(this.picCheck);
            this.Controls.Add(this.btnOpenFolderFileLog);
            this.Controls.Add(this.btnRefeshLog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnWatching);
            this.Controls.Add(this.txtPathWatching);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPathWatching;
        private System.Windows.Forms.NotifyIcon systemTray;
        private System.Windows.Forms.Button btnWatching;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnRefeshLog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.Button btnOpenFolderFileLog;
        private System.Windows.Forms.PictureBox picCheck;
        private System.Windows.Forms.Button btnOpenFolderFileWatching;
    }
}