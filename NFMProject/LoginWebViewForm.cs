namespace NFMProject
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    public partial class LoginWebViewForm : Form
    {
        public static string token;
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
                    FastProjectForm fastProjectForm = new FastProjectForm();
                    fastProjectForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NFM", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LoginWebViewForm_Load(object sender, EventArgs e)
        {            
        }
    }
}
