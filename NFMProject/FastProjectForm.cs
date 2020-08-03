using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFM.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using LoginProject;
using Windows.UI.Xaml.Documents;
using Module = NFM.model.Module;

namespace NFM
{
    public partial class FastProjectForm : Form
    {
        static string tokenLogin= "";
        public static string pathWatching = "";
        string pathConfig = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Config\\";
        string pathKey = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Key\\";
        bool checkWatching = false;
        static string content = "";
        bool checkVSCodeRun = false;
        ManagementEventWatcher startWatch = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace");
        ManagementEventWatcher processStop = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStopTrace");
        public FastProjectForm()
        {

            InitializeComponent();
            
            try
            {
                startWatch.EventArrived += new EventArrivedEventHandler(processStart_EventArrived);
                startWatch.Start();
                processStop.EventArrived += new EventArrivedEventHandler(processStop_EventArrived);
                processStop.Start();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void processStop_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string processName = e.NewEvent.Properties["ProcessName"].Value.ToString();
            string processID = Convert.ToInt32(e.NewEvent.Properties["ProcessID"].Value).ToString();
            if (processName == "Code.exe")
            {
                Debug.Print("Process stopped. Name: " + processName + " | ID: " + processID);
            }
        }

        private void processStart_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string processName = e.NewEvent.Properties["ProcessName"].Value.ToString();
            string processID = Convert.ToInt32(e.NewEvent.Properties["ProcessID"].Value).ToString();
            if (processName == "Code.exe") {
                richTextBox1.Text = processName + "-" + processID;
                Process[] processCode = Process.GetProcessesByName("Code");
                foreach (Process process in processCode)
                {
                    if (!String.IsNullOrEmpty(process.MainWindowTitle))
                    {

                        string nameFileOpening = process.MainWindowTitle.Substring(0, process.MainWindowTitle.IndexOf(" - ")).Trim();
                        string tmp = process.MainWindowTitle.Substring(process.MainWindowTitle.IndexOf(" - ") + 3);
                        string folderFileOpening = tmp.Substring(0, tmp.IndexOf(" - "));
                        MessageBox.Show(nameFileOpening);
                    }
                }
            }

        }

        private void FastProjectForm_Load(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(pathKey);
            FileInfo[] Files = d.GetFiles("*.json");
            string strPathKey = "";
            foreach (FileInfo file in Files)
            {
                strPathKey = file.Name;
            }
            if (strPathKey.Trim() != "")
            {
                JObject jOToken = ReadFileJSON(pathKey+strPathKey);
                tokenLogin = jOToken["token"].ToString();
                
            }
            GetListProject();
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

        private void FastProjectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private async void GetListProject() {
            ListProject listProject = new ListProject();
            listProject.projects = new List<Project>();           
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://tapi.lhu.edu.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenLogin);
                //GET Method  
                HttpResponseMessage response = await client.GetAsync("fp/admin_ProjectList");

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(res.ToString());
                    JArray items = (JArray)data["data"];
                    foreach (var item in items) {
                        Project project = new Project();
                        project.id = item["id"].ToString();
                        project.ProjectID = item["ProjectID"].ToString();
                        project.ProjectName = item["ProjectName"].ToString();
                        listProject.projects.Add(project);                      
                    }
                    cboProjectList.DataSource = listProject.projects;
                    cboProjectList.ValueMember = "ProjectID";
                    cboProjectList.DisplayMember = "ProjectName";
                }
                else
                {
                    Debug.Print("GetListProject: Internal server Error");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "GetListProject", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public class ListtoDataTable
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        }

        private async void GetListModule(string projectID)
        {
            ListModule listModule = new ListModule();
            listModule.modules = new List<Module>();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://tapi.lhu.edu.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenLogin);
                //GET Method
                JObject jObject = new JObject();
                jObject["projectID"] = projectID;
                StringContent post = new StringContent(JsonConvert.SerializeObject(jObject), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("fp/admin_FileList/code", post);

                //HttpResponseMessage response = await client.GetAsync("fp/admin_FileList/code" + projectID);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(res.ToString());
                    JArray items = (JArray)data["data"];
                    foreach (var item in items)
                    {                      
                        Module module = new Module();
                        module.fileID = item["fileID"].ToString();
                        module.ProjectID = item["ProjectID"].ToString();
                        module.ModuleID = item["ModuleID"].ToString();
                        module.ProjectName = item["ProjectName"].ToString();
                        module.ModuleName = item["moduleName"].ToString();
                        module.FileName = item["FileName"].ToString();
                        module.UpdateTime = item["UpdateTime"].ToString();
                        if (module.ModuleID == "jsoneditor") {
                            break;
                        }
                        listModule.modules.Add(module);
                        
                    }                   
                    ListtoDataTable lsttodt = new ListtoDataTable();
                    DataTable dt = lsttodt.ToDataTable(listModule.modules);
                    dgvListModules.DataSource = dt;

                    int columnIndex = dgvListModules.ColumnCount;
                    DataGridViewButtonColumn btnEditColumn = new DataGridViewButtonColumn();
                    btnEditColumn.Name = "edit_column";
                    btnEditColumn.Text = "Edit";
                    btnEditColumn.UseColumnTextForButtonValue = true;

                    if (dgvListModules.Columns["edit_column"] == null)
                    {
                        dgvListModules.Columns.Insert(columnIndex, btnEditColumn);
                    }


                }
                else
                {
                    Debug.Print("GetListModule Internal server Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetListModule", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async Task<String> GetContentModuleFileJS(string fileID)
        {
            string contentJS = "";
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://file.lhu.edu.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenLogin);
                //GET Method  
                HttpResponseMessage response = await client.GetAsync("fp/import/" + fileID);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    contentJS = res.ToString();
                    //JObject data = JObject.Parse(res.ToString());
                    //contentJS = data["data"]["moduleJS"].ToString();

                    if (contentJS.Trim() != "")
                    {
                        return contentJS;
                    }
                    else
                    {
                        contentJS = "";
                    }
                }
                else
                {
                    Debug.Print("GetContentModuleFileJS Internal server Error");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetContentModuleFileJS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return contentJS;

        }
        private void cboProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string projectID = cboProjectList.SelectedValue.ToString();
            GetListModule(projectID);
        }

        private void OpenVisualCode(string pathFolder) {
            var proc1 = new ProcessStartInfo();
            proc1.UseShellExecute = true;

            proc1.WorkingDirectory = pathFolder;

            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            //proc1.Verb = "runas";
            proc1.Arguments = "/c code .";
            proc1.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(proc1);
        }

        private void Watching(string pathWatching)
        {
            try
            {
                if (!Directory.Exists(pathWatching))
                {
                    Directory.CreateDirectory(pathWatching);
                }
                if (pathWatching != "")
                {

                    string filepathConfig = pathConfig + "\\config.json";
                    Config cf = new Config();
                    cf.token = tokenLogin;
                    cf.pathWatching = pathWatching;
                    string json = JsonConvert.SerializeObject(cf);
                    using (StreamWriter sw = File.CreateText(filepathConfig))
                    {
                        sw.WriteLine(json);
                    }
                    checkWatching = true;
                    picState.Visible = true;
                    this.WindowState = FormWindowState.Minimized;
                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        OpenVisualCode(pathWatching);
                        systemTray.Visible = true;
                        WriteToFile("Service is started at " + pathWatching + " ---- " + DateTime.Now);
                        CreateFileWatcher(pathWatching);
                        systemTray.Text = "Watching..." + pathWatching;
                        systemTray.BalloonTipTitle = "NFM System";
                        systemTray.BalloonTipText = "Watching..." + pathWatching;
                        systemTray.ShowBalloonTip(500);
                    }
                }
                else
                {
                    MessageBox.Show("TEST");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Watching", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static void WriteToFile(string Message)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Logs\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = path + "\\NFMLogs_"+ DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
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
                //MessageBox.Show(ex.Message, "NFM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.Print("WriteToFile" + ex.Message);
            }
        }

        private static void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            WriteToFile($"FileSystemWatcher_Renamed:  {e.OldName } to {e.Name} -- {DateTime.Now}");
        }

        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            WriteToFile($"FileSystemWatcher_Deleted:  {e.Name} -- {DateTime.Now}");
        }

        private static async void PostContent(string FileContent, string FileID) 
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://tapi.lhu.edu.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenLogin);
                //GET Method
                JObject jObject = new JObject();
                jObject["FileID"] = FileID;
                jObject["FileContent"] = FileContent;
                StringContent post = new StringContent(JsonConvert.SerializeObject(jObject), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("fp/admin_FileUpdate/uploadcontent", post);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "NFM",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private static async void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            string path = @e.FullPath;
            await TaskDelay(1000);
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(file))
            {
                content = reader.ReadToEnd();
            }           
            if (content.Trim() == "")
            {
                WriteToFile($"FileSystemWatcher_Changed:  {e.Name} -- {""} -- {DateTime.Now}");
                PostContent("", Path.GetFileNameWithoutExtension(e.Name));
            }
            else
            {
                await TaskDelay(1000);
                WriteToFile($"FileSystemWatcher_Changed:  {e.Name} -- {content} -- {DateTime.Now}");
                PostContent(content, Path.GetFileNameWithoutExtension(e.Name));
            }

        }

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            WriteToFile($"FileSystemWatcher_Created:  {e.Name} -- {DateTime.Now}");
        }

        private async void dgvListModules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string fileID = "";
            string fileName = "";
            string moduleID = "";
            string contentJS = "";
            if (e.ColumnIndex == dgvListModules.Columns["edit_column"].Index && e.RowIndex >= 0)
            {
                fileID = dgvListModules.Rows[e.RowIndex].Cells[1].Value.ToString();
                moduleID = dgvListModules.Rows[e.RowIndex].Cells[3].Value.ToString();
                fileName = dgvListModules.Rows[e.RowIndex].Cells[6].Value.ToString();
            }

            try
            {
                string pathFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\FastProject\\" + moduleID;
                
                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);
                }
                string pathFileJS = pathFolder + "\\" + fileID + "_" + fileName + ".js";

                contentJS = await GetContentModuleFileJS(fileID);
                
                if (contentJS != "")
                {
                    if (!File.Exists(pathFileJS))
                    {
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(pathFileJS))
                        {
                            //sw.WriteLine(contentJS);
                        }
                        Watching(pathFolder);
                    }
                    else
                    {
                        string temp = "Ban lua chon: \n Yes: tiep tuc chinh sua file local. \n No: Đồng bộ nội dung mới nhất trên server. \n Cancel: Hủy thao tác.";
                        DialogResult res = MessageBox.Show(temp, "NFM", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            pathWatching = pathFolder;
                            Watching(pathFolder);
                        }
                        if (res == DialogResult.No)
                        {
                            using (StreamWriter sw = File.CreateText(pathFileJS))
                            {
                                sw.WriteLine(contentJS);
                                pathWatching = pathFolder;
                                Watching(pathFolder);
                            }
                        }
                    }
                }
                else {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(pathFileJS))
                    {
                        sw.WriteLine(contentJS);
                    }
                    OpenVisualCode(pathFolder);
                    Watching(pathFolder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "dgvListModules_CellContentClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FastProjectForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) {
                //Hide();
                systemTray.Visible = true;
                this.Hide();
                if (checkWatching == false)
                {
                    systemTray.Text = "Vui long chon file watching.....";
                    systemTray.BalloonTipTitle = "NFM System";
                    systemTray.BalloonTipText = "Vui long chon file watching.....";
                    systemTray.ShowBalloonTip(500);
                }
            }
            if (this.WindowState == FormWindowState.Normal)
            {
               //Hide();
                systemTray.Visible = false;               
            }
        }

        private void systemTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            systemTray.Visible = false;
        }

        async static Task TaskDelay(int miliSec)
        {
            await Task.Delay(miliSec);
        }
        private void FastProjectForm_Activated(object sender, EventArgs e)
        {
            //try
            //{
            //    //Computer.Computer computer = new Computer.Computer();
            //    //Debug.Print(computer.UUID());
            //    if (cboProjectList.SelectedValue != null)
            //    {
            //        GetListModule(cboProjectList.SelectedValue.ToString());
            //    }

            //    if (checkWatching)
            //    {
            //        if (checkFormActive)
            //        {
            //            //Process[] processlist = Process.GetProcesses();
            //            Process[] processCode = Process.GetProcessesByName("Code");
            //            foreach (Process process in processCode)
            //            {
            //                if (!String.IsNullOrEmpty(process.MainWindowTitle))
            //                {

            //                    string nameFileOpening = process.MainWindowTitle.Substring(0, process.MainWindowTitle.IndexOf(" - ")).Trim();
            //                    string tmp = process.MainWindowTitle.Substring(process.MainWindowTitle.IndexOf(" - ") + 3);
            //                    string folderFileOpening = tmp.Substring(0, tmp.IndexOf(" - "));
            //                    //Debug.Print(folderFileOpening);
            //                    //Debug.Print(nameFileOpening);
            //                }
            //            }
            //        }
            //        checkFormActive = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.Print(ex.Message);
            //}
        }

        private void FastProjectForm_Deactivate(object sender, EventArgs e)
        {            
            //Debug.Print("Deactive");
            //await TaskDelay(5000);
            //checkFormActive = true;
        }
    }
}
