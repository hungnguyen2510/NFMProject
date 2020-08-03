namespace LoginProject
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    public partial class LoginWebViewForm : Form
    {
        public static string token = "";
        string pathKey = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Key\\";
        public LoginWebViewForm()
        {           
            InitializeComponent();
           
        }
        private void webView1_ScriptNotify(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlScriptNotifyEventArgs e)
        {
            try
            {
                string stringReturn = e.Value;
                token = JObject.Parse(stringReturn)["value"]["token"].ToString();
                if (!Directory.Exists(pathKey)) {
                    Directory.CreateDirectory(pathKey);
                }
                string filepathToken = pathKey + "\\token.json";
                JObject jOToken = new JObject();
                jOToken["token"] = token;
                string json = JsonConvert.SerializeObject(jOToken);
                using (StreamWriter sw = File.CreateText(filepathToken))
                {
                    sw.WriteLine(json);
                }
                Process process = new Process();
                process.StartInfo.FileName = @"C:\Users\Hung Mer\Source\Repos\NFMProject\NFMProject\bin\Debug\NFMProject.exe";
                process.StartInfo.Verb = "runas";
                process.StartInfo.UseShellExecute = true;
                process.Start();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NFM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }             
    }
}
