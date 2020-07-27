using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            MessageBox.Show(LoginWebViewForm.token);
        }
    }
}
