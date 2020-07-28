using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFMProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Module = NFMProject.model.Module;

namespace NFMProject
{
    public partial class FastProjectForm : Form
    {
        
        public FastProjectForm()
        {
            InitializeComponent();
        }

        private void FastProjectForm_Load(object sender, EventArgs e)
        {
            Debug.Print(LoginWebViewForm.token);
            GetListProject();
                      
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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginWebViewForm.token);
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
                    Debug.Print("Internal server Error");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginWebViewForm.token);
                //GET Method  
                HttpResponseMessage response = await client.GetAsync("fp/admin_modulelist?pid=" + projectID);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(res.ToString());
                    JArray items = (JArray)data["data"];
                    foreach (var item in items)
                    {                      
                        Module module = new Module();
                        module.id = item["id"].ToString();
                        module.ModuleID = item["ModuleID"].ToString();
                        module.ModuleName = item["ModuleName"].ToString();
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
                    Debug.Print("Internal server Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async void GetContentModuleJS(string moduleID)
        {
            string contentJS = "";
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://tapi.lhu.edu.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginWebViewForm.token);
                //GET Method  
                HttpResponseMessage response = await client.GetAsync("fp/obj/admin_moduledata/module-code/fp/" + moduleID);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(res.ToString());
                    contentJS = data["data"]["moduleJS"].ToString();

                    if (contentJS.Trim() != "")
                    {
                        MessageBox.Show(contentJS);
                    }
                    else {
                        MessageBox.Show("Khong co noi dung");
                    }
                }
                else
                {
                    Debug.Print("Internal server Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cboProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string projectID = cboProjectList.SelectedValue.ToString();
            GetListModule(projectID);
        }

        private void dgvListModules_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string moduleID = "";
            if (e.ColumnIndex == dgvListModules.Columns["edit_column"].Index && e.RowIndex >= 0)
            {
                moduleID = dgvListModules.Rows[e.RowIndex].Cells[2].Value.ToString();
            }

            try {
                string pathFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NFM\\" + moduleID;
                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);
                }
                GetContentModuleJS(moduleID);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
