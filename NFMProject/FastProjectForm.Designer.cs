namespace NFMProject
{
    partial class FastProjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FastProjectForm));
            this.cboProjectList = new System.Windows.Forms.ComboBox();
            this.dgvListModules = new System.Windows.Forms.DataGridView();
            this.systemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.picState = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListModules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picState)).BeginInit();
            this.SuspendLayout();
            // 
            // cboProjectList
            // 
            this.cboProjectList.FormattingEnabled = true;
            this.cboProjectList.Location = new System.Drawing.Point(12, 33);
            this.cboProjectList.Name = "cboProjectList";
            this.cboProjectList.Size = new System.Drawing.Size(337, 21);
            this.cboProjectList.TabIndex = 0;
            this.cboProjectList.SelectedIndexChanged += new System.EventHandler(this.cboProjectList_SelectedIndexChanged);
            // 
            // dgvListModules
            // 
            this.dgvListModules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListModules.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvListModules.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvListModules.Location = new System.Drawing.Point(0, 83);
            this.dgvListModules.MultiSelect = false;
            this.dgvListModules.Name = "dgvListModules";
            this.dgvListModules.ReadOnly = true;
            this.dgvListModules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvListModules.Size = new System.Drawing.Size(1235, 558);
            this.dgvListModules.TabIndex = 1;
            this.dgvListModules.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListModules_CellContentClick);
            // 
            // systemTray
            // 
            this.systemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTray.Icon")));
            this.systemTray.Text = "notifyIcon1";
            this.systemTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.systemTray_MouseDoubleClick);
            // 
            // picState
            // 
            this.picState.Image = global::NFMProject.Properties.Resources.check_icon_png_clip_art;
            this.picState.Location = new System.Drawing.Point(1173, 13);
            this.picState.Name = "picState";
            this.picState.Size = new System.Drawing.Size(50, 41);
            this.picState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picState.TabIndex = 2;
            this.picState.TabStop = false;
            this.picState.Visible = false;
            // 
            // FastProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 641);
            this.Controls.Add(this.picState);
            this.Controls.Add(this.dgvListModules);
            this.Controls.Add(this.cboProjectList);
            this.Name = "FastProjectForm";
            this.Text = "FastProjectForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FastProjectForm_FormClosed);
            this.Load += new System.EventHandler(this.FastProjectForm_Load);
            this.Resize += new System.EventHandler(this.FastProjectForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListModules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboProjectList;
        private System.Windows.Forms.DataGridView dgvListModules;
        private System.Windows.Forms.NotifyIcon systemTray;
        private System.Windows.Forms.PictureBox picState;
    }
}