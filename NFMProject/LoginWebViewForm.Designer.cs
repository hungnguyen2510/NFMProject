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
            this.webView1 = new Microsoft.Toolkit.Forms.UI.Controls.WebView();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.txtLinkWebView = new System.Windows.Forms.TextBox();
            this.btnEnterLink = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webView1)).BeginInit();
            this.SuspendLayout();
            // 
            // webView1
            // 
            this.webView1.IsScriptNotifyAllowed = true;
            this.webView1.Location = new System.Drawing.Point(12, 12);
            this.webView1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webView1.Name = "webView1";
            this.webView1.Size = new System.Drawing.Size(1003, 610);
            this.webView1.Source = new System.Uri("https://google.com/", System.UriKind.Absolute);
            this.webView1.TabIndex = 3;
            this.webView1.ScriptNotify += new System.EventHandler<Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlScriptNotifyEventArgs>(this.webView1_ScriptNotify);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Location = new System.Drawing.Point(1058, 569);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(169, 53);
            this.btnLoadConfig.TabIndex = 4;
            this.btnLoadConfig.Text = "Load Config";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // txtLinkWebView
            // 
            this.txtLinkWebView.Location = new System.Drawing.Point(1021, 12);
            this.txtLinkWebView.Name = "txtLinkWebView";
            this.txtLinkWebView.Size = new System.Drawing.Size(206, 20);
            this.txtLinkWebView.TabIndex = 5;
            // 
            // btnEnterLink
            // 
            this.btnEnterLink.Location = new System.Drawing.Point(1070, 38);
            this.btnEnterLink.Name = "btnEnterLink";
            this.btnEnterLink.Size = new System.Drawing.Size(111, 23);
            this.btnEnterLink.TabIndex = 6;
            this.btnEnterLink.Text = "Enter";
            this.btnEnterLink.UseVisualStyleBackColor = true;
            this.btnEnterLink.Click += new System.EventHandler(this.btnEnterLink_Click);
            // 
            // LoginWebViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 634);
            this.Controls.Add(this.btnEnterLink);
            this.Controls.Add(this.txtLinkWebView);
            this.Controls.Add(this.btnLoadConfig);
            this.Controls.Add(this.webView1);
            this.Name = "LoginWebViewForm";
            this.Text = "WebView";
            ((System.ComponentModel.ISupportInitialize)(this.webView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Microsoft.Toolkit.Forms.UI.Controls.WebView webView1;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.TextBox txtLinkWebView;
        private System.Windows.Forms.Button btnEnterLink;
    }
}

