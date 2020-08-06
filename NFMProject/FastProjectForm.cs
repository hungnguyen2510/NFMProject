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
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;

namespace NFM
{
    public delegate void EventHandler();
    public partial class FastProjectForm : Form
    {
        public static event EventHandler laterEvent;
        public static event EventHandler earlyEvent;
        static string tokenLogin= "";
        public static string pathWatching = "";
        string pathConfig = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Config\\";
        string pathKey = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\Key\\";
        bool checkWatching = false;
        static string content = "";
        string UUID = "";
        static string statusCodeUpdate = "";

        ManagementEventWatcher processStart = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace Where ProcessName='Code.exe'");
        ManagementEventWatcher processStop = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStopTrace Where ProcessName='Code.exe'");
        public FastProjectForm()
        {

            InitializeComponent();

            try
            {
                Computer.Computer computer = new Computer.Computer();
                UUID = computer.UUID();
                processStart.EventArrived += new EventArrivedEventHandler(processStart_EventArrived);
                processStart.Start();
                processStop.EventArrived += new EventArrivedEventHandler(processStop_EventArrived);
                processStop.Start();
                laterEvent += new EventHandler(laterEvent_Event);
                earlyEvent += new EventHandler(earlyEvent_Event);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void earlyEvent_Event()
        {
            MessageBox.Show("earlyEvent_Event");
        }

        private void laterEvent_Event()
        {
            MessageBox.Show("laterEvent_Event");
        }

        private void FastProjectForm_Load(object sender, EventArgs e)
        {
            
            Debug.Print(UUID);
            DirectoryInfo d = new DirectoryInfo(pathKey);
            FileInfo[] Files = d.GetFiles("*.json");
            string strPathKey = "";
            foreach (FileInfo file in Files)
            {
                strPathKey = file.Name;
            }
            if (strPathKey.Trim() != "")
            {
                JObject jOToken = ReadFileJSON(pathKey + strPathKey);
                tokenLogin = jOToken["token"].ToString();

            }
            GetListProject();
        }

        private void processStop_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string processName = e.NewEvent.Properties["ProcessName"].Value.ToString();
            string processID = Convert.ToInt32(e.NewEvent.Properties["ProcessID"].Value).ToString();
            //Debug.Print("Process stopped. Name: " + processName + " | ID: " + processID);
            //processStop.EventArrived -= new EventArrivedEventHandler(processStop_EventArrived);
            processStop.Stop();
            processStart.EventArrived += new EventArrivedEventHandler(processStart_EventArrived);

        }
        private void processStart_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string fileID = "";
            string fileName = "";
            string nameFileOpening = "";
            string folderFileOpening = "";
            string contentJS = "";
            string contentJSLocal = "";
            string pathFileJS = "";
            string processName = e.NewEvent.Properties["ProcessName"].Value.ToString();
            string processID = Convert.ToInt32(e.NewEvent.Properties["ProcessID"].Value).ToString();
            string pathFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\FastProject\\";
            //Debug.Print(processName);
            
            Process[] processCode = Process.GetProcessesByName("Code");

            try
            {
                foreach (Process process in processCode)
                {
                    if (!String.IsNullOrEmpty(process.MainWindowTitle))
                    {
                        if (process.MainWindowTitle.Contains(".js"))
                        {
                            nameFileOpening = process.MainWindowTitle.Substring(0, process.MainWindowTitle.IndexOf(" - ")).Trim();
                            string tmp = process.MainWindowTitle.Substring(process.MainWindowTitle.IndexOf(" - ") + 3);
                            folderFileOpening = tmp.Substring(0, tmp.IndexOf(" - "));

                            fileName = Path.GetFileNameWithoutExtension(nameFileOpening).Substring(0, Path.GetFileNameWithoutExtension(nameFileOpening).IndexOf('_'));
                            fileID = Path.GetFileNameWithoutExtension(nameFileOpening).Substring(Path.GetFileNameWithoutExtension(nameFileOpening).IndexOf('_') + 1);
                            //truyen id de lay content
                            contentJS = GetContentModuleFileJS(fileID).Result;
                            pathFileJS = pathFolder + "\\" + fileName + "_" + fileID + ".js";
                        }
                        else
                        {
                            Debug.Print("ex");
                        }
                    }
                }
                if (pathFileJS != "")
                {
                    contentJSLocal = File.ReadAllText(pathFileJS);
                    //Debug.Print("contentJSLocal: " + contentJSLocal);                    
                    if (contentJSLocal.Trim() == "")
                    {
                        using (StreamWriter sw = File.CreateText(pathFileJS))
                        {
                            sw.WriteLine(contentJS);
                        }
                    }                   
                    processStart.EventArrived -= new EventArrivedEventHandler(processStart_EventArrived);
                    //processStop.EventArrived += new EventArrivedEventHandler(processStop_EventArrived);                    
                    processStop.Start();
                }
                if (this.WindowState == FormWindowState.Minimized)
                {
                    if (pathFolder != "")
                    {
                        pathWatching = pathFolder;
                        Watching(pathWatching);
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

        private void OpenVisualCode(string pathFolder, string pathFile) {
            var proc1 = new ProcessStartInfo();
            proc1.UseShellExecute = true;

            proc1.WorkingDirectory = pathFolder;

            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            proc1.Arguments = "/c code " + pathFile;
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
                    //picState.Visible = true;
                    this.WindowState = FormWindowState.Minimized;
                    if (this.WindowState == FormWindowState.Minimized)
                    {                       
                        systemTray.Visible = true;
                        //WriteToFile("Service is started at " + pathWatching + " ---- " + DateTime.Now);
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
                //MessageBox.Show(ex.Message, "Watching", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private string checkUpdate(string fileID) {
            Debug.Print("CheckUpdate");
            string result = "";
            string updateTime = "";
            string uid = "";
            int rowIndex = -1;
            if (fileID != "")
            {
                foreach (DataGridViewRow row in dgvListModules.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(fileID))
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }
                //GET Method 
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://tapi.lhu.edu.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenLogin);
                JObject jObject = new JObject();
                jObject["fileID"] = fileID;
                StringContent post = new StringContent(JsonConvert.SerializeObject(jObject), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("fp/admin_FileList/code", post).Result;

                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsStringAsync().Result;
                    JObject data = JObject.Parse(res.ToString());
                    JArray items = (JArray)data["data"];
                    foreach (var item in items)
                    {
                        updateTime = item["UpdateTime"].ToString();
                        uid = item["UpdateComputer"].ToString();
                    }
                    if (uid == UUID)
                    {
                        DateTime t1 = Convert.ToDateTime(dgvListModules.Rows[rowIndex].Cells[7].Value.ToString());
                        DateTime t2 = Convert.ToDateTime(updateTime);
                        int compare = DateTime.Compare(t1, t2);

                        switch (compare)
                        {
                            case -1:
                                Debug.Print(dgvListModules.Rows[rowIndex].Cells[7].Value.ToString() + " < " + updateTime);
                                result = "-1";
                                break;
                            case 0:
                                Debug.Print(dgvListModules.Rows[rowIndex].Cells[7].Value.ToString() + " = " + updateTime);
                                result = "0";
                                break;
                            case 1:
                                Debug.Print(dgvListModules.Rows[rowIndex].Cells[7].Value.ToString() + " > " + updateTime);
                                result = "1";
                                break;
                            default:
                                Console.WriteLine("Sorry, invalid selection.");
                                break;
                        }
                    }
                    else
                    {
                        Debug.Print("Khac UID");
                        result = "-1";
                    }
                }

            }
            //Thread.Sleep(5000);
            return result;
        }

        private void PostContent(string FileContent, string FileID) 
        {          
            try
            {               
                string fileID = FileID.Substring(FileID.IndexOf("_") + 1);
                string pathFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\FastProject\\";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://tapi.lhu.edu.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenLogin);
                //GET Method
                JObject jObject = new JObject();
                jObject["FileID"] = fileID;
                jObject["FileContent"] = FileContent;
                jObject["ComputerID"] = UUID;
                string resultCheckUpdate = checkUpdate(fileID);
                StringContent post = new StringContent(JsonConvert.SerializeObject(jObject), Encoding.UTF8, "application/json");
                if (resultCheckUpdate != "")
                {
                    if (resultCheckUpdate == "-1")
                    {
                        string temp = "CÓ UPDATE MỚI TRÊN SERVER! Bạn lựa chọn?: \n Yes: Tiếp tục save file local lên server. \n No: Đồng bộ nội dung mới nhất trên server. \n Cancel: Hủy thao tác.";
                        DialogResult dialogResult = MessageBox.Show(temp, "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            HttpResponseMessage response = client.PostAsync("fp/admin_FileUpdate/uploadcontent", post).Result;
                            statusCodeUpdate = response.StatusCode.ToString();
                            if (statusCodeUpdate == "OK")
                            {
                                String searchValue = fileID;
                                int rowIndex = -1;
                                foreach (DataGridViewRow row in dgvListModules.Rows)
                                {
                                    if (row.Cells[1].Value.ToString().Equals(searchValue))
                                    {
                                        rowIndex = row.Index;
                                        break;
                                    }
                                }
                                dgvListModules.Rows[rowIndex].Cells[7].Value = DateTime.Now.ToString();
                            }
                            else
                            {
                                Debug.Print(statusCodeUpdate);
                            }
                        }
                        if (dialogResult == DialogResult.No)
                        {
                            string contentJS = GetContentModuleFileJS(fileID).Result;
                            string pathFileJS = pathFolder + "\\" + FileID + ".js";
                            using (StreamWriter sw = File.CreateText(pathFileJS))
                            {
                                sw.WriteLine(contentJS);
                                pathWatching = pathFolder;
                                String searchValue = fileID;
                                int rowIndex = -1;
                                foreach (DataGridViewRow row in dgvListModules.Rows)
                                {
                                    if (row.Cells[1].Value.ToString().Equals(searchValue))
                                    {
                                        rowIndex = row.Index;
                                        break;
                                    }
                                }
                                dgvListModules.Rows[rowIndex].Cells[7].Value = DateTime.Now.ToString();
                            }
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.PostAsync("fp/admin_FileUpdate/uploadcontent", post).Result;
                        statusCodeUpdate = response.StatusCode.ToString();
                        if (statusCodeUpdate == "OK")
                        {
                            String searchValue = fileID;
                            int rowIndex = -1;
                            foreach (DataGridViewRow row in dgvListModules.Rows)
                            {
                                if (row.Cells[1].Value.ToString().Equals(searchValue))
                                {
                                    rowIndex = row.Index;
                                    break;
                                }
                            }
                            dgvListModules.Rows[rowIndex].Cells[7].Value = DateTime.Now.ToString();
                            Debug.Print("save voi time lon hon");
                        }
                        else
                        {
                            Debug.Print(statusCodeUpdate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "NFM",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            string path = @e.FullPath;
            Thread.Sleep(1000);
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
                Thread.Sleep(1000);
                //Debug.Print(Path.GetFileNameWithoutExtension(e.Name).Substring(Path.GetFileNameWithoutExtension(e.Name).IndexOf("_") + 1));
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
            //string moduleID = "";
            string fileName = "";
            
            if (e.ColumnIndex == dgvListModules.Columns["edit_column"].Index && e.RowIndex >= 0)
            {
                fileID = dgvListModules.Rows[e.RowIndex].Cells[1].Value.ToString();
                fileName = dgvListModules.Rows[e.RowIndex].Cells[6].Value.ToString();
                //moduleID = dgvListModules.Rows[e.RowIndex].Cells[3].Value.ToString();
            }         
            try
            {
                string pathFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\FastProject\\";
                
                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);
                }
                string pathFileJS = pathFolder + "\\" + fileName + "_" + fileID + ".js";
                if (!File.Exists(pathFileJS))
                {
                    File.CreateText(pathFileJS);
                    Watching(pathFolder);
                    OpenVisualCode(pathFolder, fileName + "_" + fileID + ".js");
                    Debug.Print("tao file trong");
                }
                else
                {
                    string temp = "Ban lua chon: \n Yes: tiep tuc chinh sua file local. \n No: Đồng bộ nội dung mới nhất trên server. \n Cancel: Hủy thao tác.";
                    DialogResult res = MessageBox.Show(temp, "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        OpenVisualCode(pathFolder, fileName + "_" + fileID + ".js");
                        pathWatching = pathFolder;
                        Watching(pathFolder);
                    }
                    if (res == DialogResult.No)
                    {
                        string contentJS = await GetContentModuleFileJS(fileID);
                        using (StreamWriter sw = File.CreateText(pathFileJS))
                        {
                            sw.WriteLine(contentJS);                         
                            pathWatching = pathFolder;
                            Watching(pathFolder);
                            OpenVisualCode(pathFolder, fileName + "_" + fileID + ".js");
                        }
                    }
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
                    systemTray.Text = "Vui lòng chọn file, hoặc mở VSCode";
                    systemTray.BalloonTipTitle = "NFM System";
                    systemTray.BalloonTipText = "Vui lòng chọn file, hoặc mở VSCode";
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
            try
            {                
                //if (cboProjectList.SelectedValue != null)
                //{
                //    GetListModule(cboProjectList.SelectedValue.ToString());
                //}
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        private void FastProjectForm_Deactivate(object sender, EventArgs e)
        {            
            //Debug.Print("Deactive");
            //await TaskDelay(5000);
            //checkFormActive = true;
        }     
    }
}
