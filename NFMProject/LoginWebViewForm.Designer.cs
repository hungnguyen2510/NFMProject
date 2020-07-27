namespace NFMProject
{
    partial class LoginWebViewForm
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
            this.txtLinkWebView = new System.Windows.Forms.TextBox();
            this.btnEnterLink = new System.Windows.Forms.Button();
            this.webView1 = new Microsoft.Toolkit.Forms.UI.Controls.WebView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.webView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLinkWebView
            // 
            this.txtLinkWebView.Location = new System.Drawing.Point(6, 12);
            this.txtLinkWebView.Name = "txtLinkWebView";
            this.txtLinkWebView.Size = new System.Drawing.Size(206, 20);
            this.txtLinkWebView.TabIndex = 5;
            // 
            // btnEnterLink
            // 
            this.btnEnterLink.Location = new System.Drawing.Point(56, 38);
            this.btnEnterLink.Name = "btnEnterLink";
            this.btnEnterLink.Size = new System.Drawing.Size(111, 23);
            this.btnEnterLink.TabIndex = 6;
            this.btnEnterLink.Text = "Enter";
            this.btnEnterLink.UseVisualStyleBackColor = true;
            this.btnEnterLink.Click += new System.EventHandler(this.btnEnterLink_Click);
            // 
            // webView1
            // 
            this.webView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView1.IsScriptNotifyAllowed = true;
            this.webView1.Location = new System.Drawing.Point(0, 0);
            this.webView1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webView1.Name = "webView1";
            this.webView1.Size = new System.Drawing.Size(1024, 634);
            this.webView1.Source = new System.Uri("https://app.lhu.edu.vn/?appmode=win", System.UriKind.Absolute);
            this.webView1.TabIndex = 3;
            this.webView1.ScriptNotify += new System.EventHandler<Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlScriptNotifyEventArgs>(this.webView1_ScriptNotify);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnEnterLink);
            this.panel1.Controls.Add(this.txtLinkWebView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1024, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 634);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.webView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 634);
            this.panel2.TabIndex = 8;
            // 
            // LoginWebViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1239, 634);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "LoginWebViewForm";
            this.Text = "WebView";
            this.Load += new System.EventHandler(this.LoginWebViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLinkWebView;
        private System.Windows.Forms.Button btnEnterLink;
        private Microsoft.Toolkit.Forms.UI.Controls.WebView webView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

