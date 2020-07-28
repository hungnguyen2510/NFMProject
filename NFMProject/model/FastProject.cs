using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMProject.model
{
    public class Project
    {
        public string id { get; set; }
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }

        public Project() { }
        public Project(string id, string projectID, string projectName) {
            this.id = id;
            this.ProjectID = projectID;
            this.ProjectName = projectName;
        }
    }

    public class Module
    {
        public string id { get; set; }
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
    }

    public class ListProject
    {
        public List<Project> projects { get; set; }       
    }

    public class ListModule
    {
        public List<Module> modules { get; set; }
    }


}
