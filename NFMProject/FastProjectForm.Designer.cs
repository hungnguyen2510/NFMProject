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
            this.cboProjectList = new System.Windows.Forms.ComboBox();
            this.dgvListModules = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListModules)).BeginInit();
            this.SuspendLayout();
            // 
            // cboProjectList
            // 
            this.cboProjectList.FormattingEnabled = true;
            this.cboProjectList.Location = new System.Drawing.Point(13, 13);
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
            this.dgvListModules.Name = "dgvListModules";
            this.dgvListModules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListModules.Size = new System.Drawing.Size(1235, 558);
            this.dgvListModules.TabIndex = 1;
            this.dgvListModules.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListModules_CellClick);
            // 
            // FastProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 641);
            this.Controls.Add(this.dgvListModules);
            this.Controls.Add(this.cboProjectList);
            this.Name = "FastProjectForm";
            this.Text = "FastProjectForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FastProjectForm_FormClosed);
            this.Load += new System.EventHandler(this.FastProjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListModules)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboProjectList;
        private System.Windows.Forms.DataGridView dgvListModules;
    }
}