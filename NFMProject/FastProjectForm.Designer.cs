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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboProjectList = new System.Windows.Forms.ComboBox();
            this.systemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.picState = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvListModules = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picState)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListModules)).BeginInit();
            this.SuspendLayout();
            // 
            // cboProjectList
            // 
            this.cboProjectList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboProjectList.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProjectList.FormattingEnabled = true;
            this.cboProjectList.Location = new System.Drawing.Point(12, 32);
            this.cboProjectList.Name = "cboProjectList";
            this.cboProjectList.Size = new System.Drawing.Size(443, 28);
            this.cboProjectList.TabIndex = 0;
            this.cboProjectList.SelectedIndexChanged += new System.EventHandler(this.cboProjectList_SelectedIndexChanged);
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
            this.picState.Location = new System.Drawing.Point(1173, 19);
            this.picState.Name = "picState";
            this.picState.Size = new System.Drawing.Size(50, 41);
            this.picState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picState.TabIndex = 2;
            this.picState.TabStop = false;
            this.picState.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(170)))), ((int)(((byte)(237)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboProjectList);
            this.panel1.Controls.Add(this.picState);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1235, 78);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chọn project";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvListModules);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 74);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1235, 567);
            this.panel2.TabIndex = 4;
            // 
            // dgvListModules
            // 
            this.dgvListModules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListModules.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvListModules.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvListModules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListModules.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvListModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListModules.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvListModules.Location = new System.Drawing.Point(0, 0);
            this.dgvListModules.MultiSelect = false;
            this.dgvListModules.Name = "dgvListModules";
            this.dgvListModules.ReadOnly = true;
            this.dgvListModules.RowHeadersVisible = false;
            this.dgvListModules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvListModules.ShowEditingIcon = false;
            this.dgvListModules.Size = new System.Drawing.Size(1235, 567);
            this.dgvListModules.TabIndex = 1;
            this.dgvListModules.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListModules_CellContentClick);
            // 
            // FastProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 641);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FastProjectForm";
            this.Text = "NFM";
            this.Activated += new System.EventHandler(this.FastProjectForm_Activated);
            this.Deactivate += new System.EventHandler(this.FastProjectForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FastProjectForm_FormClosed);
            this.Load += new System.EventHandler(this.FastProjectForm_Load);
            this.Resize += new System.EventHandler(this.FastProjectForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picState)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListModules)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboProjectList;
        private System.Windows.Forms.NotifyIcon systemTray;
        private System.Windows.Forms.PictureBox picState;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgvListModules;
    }
}