namespace NFMProject
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using static Config;
    public partial class LoginWebViewForm : Form
    {
        public static string token;
        public static string pathWatchingConfig = "";
        public static string tokenConfig = "";
        string pathConfig = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Config\\";
        public LoginWebViewForm()
        {
            InitializeComponent();
        }

        private void webView1_ScriptNotify(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlScriptNotifyEventArgs e)
        {
            try
            {
                string stringReturn = e.Value;
                token = JObject.Parse(stringReturn)["token"].ToString();
                //MessageBox.Show(token);

                if (stringReturn != "")
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
        public JObject ReadFileJSON(string path)
        {
            JObject o1 = JObject.Parse(File.ReadAllText(path));
            JObject o2 = new JObject();

            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                o2 = (JObject)JToken.ReadFrom(reader);
            }
            return o2;
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(pathConfig);
                FileInfo[] Files = d.GetFiles("*.json");
                string strConfig = "";
                foreach (FileInfo file in Files)
                {
                    strConfig = file.Name;
                }
                if (strConfig != "")
                {
                    string pathFile = pathConfig + strConfig;
                    JObject ob = ReadFileJSON(pathFile);
                    Config cf = JsonConvert.DeserializeObject<Config>(ob.ToString());
                    if (cf.token != "")
                    {
                        tokenConfig = cf.token;
                        pathWatchingConfig = cf.pathWatching;
                        MainForm fm = new MainForm();
                        fm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Khong co config");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEnterLink_Click(object sender, EventArgs e)
        {
            webView1.Navigate(new Uri(txtLinkWebView.Text, UriKind.Absolute));
        }
    }
}
