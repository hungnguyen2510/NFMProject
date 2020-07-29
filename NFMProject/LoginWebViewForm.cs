namespace NFMProject
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
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
                token = JObject.Parse(stringReturn)["value"]["token"].ToString();
                if (stringReturn != "")
                {
                    //this.Hide();
                    FastProjectForm fastProjectForm = new FastProjectForm();
                    fastProjectForm.ShowDialog();
                    this.Close();
                    //Application.Run(new FastProjectForm());
                    

                    //MainForm mainForm = new MainForm();
                    //mainForm.Show();
                    //this.Hide();
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
        private void btnEnterLink_Click(object sender, EventArgs e)
        {
            
            webView1.Navigate(new Uri(txtLinkWebView.Text, UriKind.Absolute));
        }

        private void LoginWebViewForm_Load(object sender, EventArgs e)
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
                    DialogResult res = MessageBox.Show("Ban co muon load config ko?", "NFM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {                       
                        string pathFile = pathConfig + strConfig;
                        JObject ob = ReadFileJSON(pathFile);
                        Config cf = JsonConvert.DeserializeObject<Config>(ob.ToString());
                        if (cf.token != "")
                        {
                            //this.Hide();
                            token = cf.token;
                            pathWatchingConfig = cf.pathWatching;
                            FastProjectForm fastProjectForm = new FastProjectForm();
                            fastProjectForm.ShowDialog();
                            //Application.Run(new FastProjectForm());
                            this.Close();
                        }                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
