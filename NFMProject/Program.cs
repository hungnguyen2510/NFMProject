using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;

namespace NFMProject
{
    static class Program
    {
        public static string token = "";
        public static string pathWatchingConfig = "";
        public static string tokenConfig = "";
        public static string pathConfig = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Config\\";
        public static JObject ReadFileJSON(string path)
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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new LoginWebViewForm());

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
                    DialogResult res = MessageBox.Show("Bạn có muốn load config không?", "NFM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        string pathFile = pathConfig + strConfig;
                        JObject ob = ReadFileJSON(pathFile);
                        Config cf = JsonConvert.DeserializeObject<Config>(ob.ToString());
                        if (cf.token != "")
                        {
                            token = cf.token;
                            pathWatchingConfig = cf.pathWatching;
                            Application.Run(new FastProjectForm());
                        }
                    }
                    else {
                        Application.Run(new LoginWebViewForm());
                    }
                }
                else
                {
                    Application.Run(new LoginWebViewForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NFM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }
    }
}
