namespace NFMProject
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using Newtonsoft.Json.Linq;
    public partial class LoginWebViewForm : Form
    {
        public LoginWebViewForm()
        {
            InitializeComponent();
        }

        private async void webView1_ScriptNotify(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlScriptNotifyEventArgs e)
        {
            try
            {
                string token = await webView1.InvokeScriptAsync("sendscript");
                dynamic json = JObject.Parse(token);
                if (token != "")
                {

                    MainForm fm = new MainForm();
                    fm.Show();
                    this.Hide();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
