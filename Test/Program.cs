using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using LoginProject;

namespace LoginProject
{
    public static class Program
    {
        public static string token = "";
        public static string pathWatchingConfig = "";
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
            try
            {
                if (!Directory.Exists(pathConfig))
                {
                    Directory.CreateDirectory(pathConfig);
                }
                DirectoryInfo d = new DirectoryInfo(pathConfig);
                FileInfo[] Files = d.GetFiles("*.json");
                string strConfig = "";
                foreach (FileInfo file in Files)
                {
                    strConfig = file.Name;
                }
                if (strConfig.Trim() != "")
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
                            MessageBox.Show(token);

                            Process process = new Process();
                            string pathNFMProject = Directory.GetCurrentDirectory() + "\\NFMProject.exe";
                            process.StartInfo.FileName = pathNFMProject;
                            process.StartInfo.Verb = "runas";
                            process.StartInfo.UseShellExecute = true;
                            process.Start();
                            
                        }
                    }
                    else
                    {
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
