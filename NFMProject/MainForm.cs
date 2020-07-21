using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFMProject
{
    public partial class MainForm : Form
    {
        public static string content;
        public static string filepath = "";
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                string pathFolderDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                DirectoryInfo d = new DirectoryInfo(pathFolderDocument + "\\NFMLogs\\");
                FileInfo[] Files = d.GetFiles("*.txt");
                string str = "";
                foreach (FileInfo file in Files)
                {
                    str = file.Name;
                }
                if (str != "")
                {
                    string pathFile = pathFolderDocument + "\\NFMLogs\\" + str;
                    ReadFileLog(pathFile);
                }
                else
                {
                    txtLog.Text = "Empty Log";
                }
                //ReadFileLog(path+)

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
                //string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFMLogs";
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

        public void ReadFileLog(string path)
        {
            try
            {   // Open the text file using a stream reader.
                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    txtLog.Text += line + "\n";
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
            WriteToFile($"FileSystemWatcher_Changed:  {e.Name} -- \n{content} -- {DateTime.Now}");

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

        private void btnWatching_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string pathWatching = folderBrowserDialog1.SelectedPath;
                    txtPathWatching.Text = pathWatching;
                    if (txtPathWatching.Text != "")
                    {
                        txtLog.Text = "";
                        this.WindowState = FormWindowState.Minimized;
                        if (this.WindowState == FormWindowState.Minimized)
                        {
                            this.Hide();
                            systemTray.Visible = true;

                            WriteToFile("Service is started at " + DateTime.Now);
                            CreateFileWatcher(pathWatching);
                            systemTray.Text = "Watching..." + pathWatching;
                            systemTray.BalloonTipTitle = "NFM System";
                            systemTray.BalloonTipText = "Watching..." + pathWatching;
                            systemTray.ShowBalloonTip(1000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                systemTray.Visible = true;
                if (txtPathWatching.Text == "")
                {
                    systemTray.Text = "Please choose folder watching....";
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

        private void btnRefeshLog_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
            if (filepath.Trim() != "")
            {
                MessageBox.Show(filepath);
                ReadFileLog(filepath);
            }
            else
            {
                MessageBox.Show("Choose folder...");
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
    }
}
