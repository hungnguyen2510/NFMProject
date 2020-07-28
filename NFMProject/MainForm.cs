using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace NFMProject
{
    public partial class MainForm : Form
    {
        public static string content;
        public static string filepath = "";
        string pathConfig = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Config\\";
        string pathFolderDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Logs\\";
        bool checkWatching = false;

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Debug.Print(LoginWebViewForm.token);
                if (LoginWebViewForm.pathWatchingConfig.Trim() != "") {
                    txtPathWatching.Text = LoginWebViewForm.pathWatchingConfig;
                    Watching(LoginWebViewForm.pathWatchingConfig);
                    MessageBox.Show("Watching với config: " + LoginWebViewForm.pathWatchingConfig);
                    DirectoryInfo d = new DirectoryInfo(pathFolderDocument);
                    FileInfo[] Files = d.GetFiles("*.txt");
                    string str = "";
                    foreach (FileInfo file in Files)
                    {
                        str = file.Name;
                    }
                    if (str != "")
                    {
                        string pathFile = pathFolderDocument + str;
                        ReadFileLog(pathFile);
                    }                  
                }
                else
                {
                    txtLog.Text = "Empty Log";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public static void WriteToFile(string Message)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Logs\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }               
                filepath = path + "\\NFMLogs_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                if (!File.Exists(filepath))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public JObject ReadFileJSON(string path)
        {
            JObject o1 = JObject.Parse(File.ReadAllText(path));
            JObject o2 = new JObject();
            // read JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                o2 = (JObject)JToken.ReadFrom(reader);
                //MessageBox.Show(o2.ToString());
            }
            return o2;
        }
        public void ReadFileLog(string path)
        {
            try
            {   // Open the text file using a stream reader.
                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    txtLog.AppendText(line + Environment.NewLine);
                    Debug.Print(line);
                }
            }
            catch (IOException e)
            {
                Debug.Print("The file could not be read:");
                Debug.Print(e.Message);
            }
        }

        public void CreateFileWatcher(string path)
        {
            var fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            fileSystemWatcher.Path = path;
            fileSystemWatcher.EnableRaisingEvents = true;

            WriteToFile("Watching....");
        }

        private static void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            WriteToFile($"FileSystemWatcher_Renamed:  {e.OldName } to {e.Name} -- {DateTime.Now}");
        }

        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            WriteToFile($"FileSystemWatcher_Deleted:  {e.Name} -- {DateTime.Now}");
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            string path = @e.FullPath;

            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(file))
            {
                content = reader.ReadToEnd();
            }
            WriteToFile($"FileSystemWatcher_Changed:  {e.Name} -- {DateTime.Now}");

        }

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            WriteToFile($"FileSystemWatcher_Created:  {e.Name} -- {DateTime.Now}");
        }

        private void systemTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            systemTray.Visible = false;

        }
        private void Watching(string pathWatching) {
            try
            {
                if (pathWatching != "")
                {
                    txtLog.Text = "";
                    this.WindowState = FormWindowState.Minimized;
                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        this.Hide();
                        systemTray.Visible = true;
                        WriteToFile("Service is started at " + pathWatching + " ---- " + DateTime.Now);
                        CreateFileWatcher(pathWatching);
                        systemTray.Text = "Watching..." + pathWatching;
                        systemTray.BalloonTipTitle = "NFM System";
                        systemTray.BalloonTipText = "Watching..." + pathWatching;
                        systemTray.ShowBalloonTip(1000);
                    }
                    checkWatching = true;
                    picCheck.Visible = true;
                }
            }
            catch (Exception ex) {
                Debug.Print(ex.Message);
            }
        }

        private void btnWatching_Click(object sender, EventArgs e)
        {                     
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string pathWatching = folderBrowserDialog1.SelectedPath;
                    txtPathWatching.Text = pathWatching;
                    try
                    {
                        
                        if (!Directory.Exists(pathConfig))
                        {
                            Directory.CreateDirectory(pathConfig);
                        }
                        string filepathConfig = pathConfig + "\\config.json";
                        Config cf = new Config();
                        cf.token = LoginWebViewForm.token;
                        cf.pathWatching = pathWatching;
                        string json = JsonConvert.SerializeObject(cf);

                        //if (!File.Exists(filepathConfig)) 
                        //{
                        //RootObject ro = new RootObject();
                        //Account acc = new Account();
                        //acc.items = new List<ItemConfig>();

                        //ItemConfig ic = new ItemConfig();
                        //ic.token = LoginWebViewForm.token.token.ToString();
                        //ic.pathFolderWatching = pathWatching;
                        //acc.items.Add(ic);
                        //ro.Account = acc;
                        //string json = JsonConvert.SerializeObject(ro);

                        using (StreamWriter sw = File.CreateText(filepathConfig))
                        {
                            sw.WriteLine(json);
                        }
                            
                        //}
                        //else
                        //{
                        //    DirectoryInfo d = new DirectoryInfo(pathConfig);
                        //    FileInfo[] Files = d.GetFiles("*.json");
                        //    string strConfig = "";
                        //    foreach (FileInfo file in Files)
                        //    {
                        //        strConfig = file.Name;
                        //    }
                        //    if (strConfig != "")
                        //    {
                        //        string pathFile = pathConfig + strConfig;
                        //        JObject ob = ReadFileJSON(pathFile);
                        //        JObject contentJson = JObject.Parse(ob.ToString());
                                    
                        //        RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(contentJson.ToString());
                        //        ItemConfig ic = new ItemConfig();
                        //        ic.token = LoginWebViewForm.token.token.ToString();
                        //        ic.pathFolderWatching = pathWatching;

                        //        rootobject.Account.items.Add(ic);
                        //        string json = JsonConvert.SerializeObject(rootobject);
                        //        using (StreamWriter sw = File.CreateText(filepathConfig))
                        //        {
                        //            sw.WriteLine(json);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Doc config that bai");
                        //    }                        
                        //}
                        DialogResult res = MessageBox.Show("Tao config thanh cong", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (res == DialogResult.OK) {
                            Watching(pathWatching);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefeshLog_Click(object sender, EventArgs e)
        {
            if (checkWatching == true) { 
                txtLog.Text = "";
                DirectoryInfo d = new DirectoryInfo(pathFolderDocument);
                FileInfo[] Files = d.GetFiles("*.txt");
                string str = "";
                foreach (FileInfo file in Files)
                {
                    str = file.Name;
                }
                if (str != "")
                {
                    string pathFile = pathFolderDocument + str;
                    ReadFileLog(pathFile);
                }
            }
            else
            {
                MessageBox.Show("Choose folder...empty file log");
                txtLog.Text = "Empty Log";
                btnWatching.PerformClick();
                
            }
        }

        private void btnOpenFolderFileLog_Click(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(pathFolderDocument);
            Process.Start(d.ToString());
        }

        private void txtLog_Enter(object sender, EventArgs e)
        {
            txtLog.Text = "";
            DirectoryInfo d = new DirectoryInfo(pathFolderDocument);
            FileInfo[] Files = d.GetFiles("*.txt");
            string str = "";
            foreach (FileInfo file in Files)
            {
                str = file.Name;
            }
            if (str != "")
            {
                string pathFile = pathFolderDocument + str;
                ReadFileLog(pathFile);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            systemTray.Visible = false;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                systemTray.Visible = true;
                if (txtPathWatching.Text == "")
                {
                    systemTray.Text = "Vui long chon folder watching.....";
                    systemTray.BalloonTipTitle = "NFM System";
                    systemTray.BalloonTipText = "Vui long chon folder watching.....";
                    systemTray.ShowBalloonTip(1000);
                }
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                txtLog.Text = "";
                if (filepath.Trim() != "")
                {
                    ReadFileLog(filepath);
                }
            }
        }

        private void btnOpenFolderFileWatching_Click(object sender, EventArgs e)
        {
            if (txtPathWatching.Text != "")
            {
                DirectoryInfo d = new DirectoryInfo(txtPathWatching.Text);
                Process.Start(d.ToString());
            }
            else {
                MessageBox.Show("Vui long chon folder watching");
                btnWatching.PerformClick();
            }
        }     
    }
}
