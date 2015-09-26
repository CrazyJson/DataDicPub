namespace MyTools.DataDic
{
    partial class DataDicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataDicForm));
            this.btnGenSQL = new System.Windows.Forms.Button();
            this.textTableList = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.数据库配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.默认生成模式配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动识别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检查更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.特别说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反馈建议toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.文档生成工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MyToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.txtOutPutPath = new System.Windows.Forms.TextBox();
            this.radioAdd = new System.Windows.Forms.RadioButton();
            this.radioUpdate = new System.Windows.Forms.RadioButton();
            this.radioAuto = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnSerach = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenSQL
            // 
            this.btnGenSQL.Location = new System.Drawing.Point(101, 136);
            this.btnGenSQL.Name = "btnGenSQL";
            this.btnGenSQL.Size = new System.Drawing.Size(109, 23);
            this.btnGenSQL.TabIndex = 19;
            this.btnGenSQL.Text = "生成SQL脚本";
            this.btnGenSQL.UseVisualStyleBackColor = true;
            this.btnGenSQL.Click += new System.EventHandler(this.btnGenSQL_Click);
            // 
            // textTableList
            // 
            this.textTableList.BackColor = System.Drawing.Color.White;
            this.textTableList.Location = new System.Drawing.Point(110, 63);
            this.textTableList.Name = "textTableList";
            this.textTableList.ReadOnly = true;
            this.textTableList.Size = new System.Drawing.Size(272, 21);
            this.textTableList.TabIndex = 18;
            this.MyToolTip.SetToolTip(this.textTableList, "设置生成脚本的表名集合,分隔，如果为空则生成所有表的脚本");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "表名：";
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPath.Location = new System.Drawing.Point(110, 37);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(272, 21);
            this.txtPath.TabIndex = 15;
            this.MyToolTip.SetToolTip(this.txtPath, "模版文件路径");
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "模版文件路径：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.帮助ToolStripMenuItem,
            this.反馈建议toolStripMenuItem2,
            this.文档生成工具ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(428, 25);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据库配置ToolStripMenuItem,
            this.默认生成模式配置ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "编辑";
            // 
            // 数据库配置ToolStripMenuItem
            // 
            this.数据库配置ToolStripMenuItem.Name = "数据库配置ToolStripMenuItem";
            this.数据库配置ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.数据库配置ToolStripMenuItem.Text = "数据库配置";
            this.数据库配置ToolStripMenuItem.Click += new System.EventHandler(this.数据库配置ToolStripMenuItem_Click);
            // 
            // 默认生成模式配置ToolStripMenuItem
            // 
            this.默认生成模式配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增ToolStripMenuItem,
            this.修改ToolStripMenuItem,
            this.自动识别ToolStripMenuItem});
            this.默认生成模式配置ToolStripMenuItem.Name = "默认生成模式配置ToolStripMenuItem";
            this.默认生成模式配置ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.默认生成模式配置ToolStripMenuItem.Text = "默认生成模式配置";
            // 
            // 新增ToolStripMenuItem
            // 
            this.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem";
            this.新增ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新增ToolStripMenuItem.Text = "新增";
            this.新增ToolStripMenuItem.Click += new System.EventHandler(this.生成模式配置ToolStripMenuItem_Click);
            // 
            // 修改ToolStripMenuItem
            // 
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.修改ToolStripMenuItem.Text = "修改";
            this.修改ToolStripMenuItem.Click += new System.EventHandler(this.生成模式配置ToolStripMenuItem_Click);
            // 
            // 自动识别ToolStripMenuItem
            // 
            this.自动识别ToolStripMenuItem.Name = "自动识别ToolStripMenuItem";
            this.自动识别ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.自动识别ToolStripMenuItem.Text = "自动识别";
            this.自动识别ToolStripMenuItem.Click += new System.EventHandler(this.生成模式配置ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.说明ToolStripMenuItem,
            this.检查更新ToolStripMenuItem,
            this.关于ToolStripMenuItem2,
            this.特别说明ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 说明ToolStripMenuItem
            // 
            this.说明ToolStripMenuItem.Name = "说明ToolStripMenuItem";
            this.说明ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.说明ToolStripMenuItem.Text = "说明";
            this.说明ToolStripMenuItem.Click += new System.EventHandler(this.说明ToolStripMenuItem_Click);
            // 
            // 检查更新ToolStripMenuItem
            // 
            this.检查更新ToolStripMenuItem.Image = global::MyTools.Properties.Resources.程序更新;
            this.检查更新ToolStripMenuItem.Name = "检查更新ToolStripMenuItem";
            this.检查更新ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.检查更新ToolStripMenuItem.Text = "检查更新";
            this.检查更新ToolStripMenuItem.Click += new System.EventHandler(this.检查更新ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem2
            // 
            this.关于ToolStripMenuItem2.Name = "关于ToolStripMenuItem2";
            this.关于ToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.关于ToolStripMenuItem2.Text = "关于";
            this.关于ToolStripMenuItem2.Click += new System.EventHandler(this.关于ToolStripMenuItem2_Click);
            // 
            // 特别说明ToolStripMenuItem
            // 
            this.特别说明ToolStripMenuItem.Name = "特别说明ToolStripMenuItem";
            this.特别说明ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.特别说明ToolStripMenuItem.Text = "特别说明";
            this.特别说明ToolStripMenuItem.Click += new System.EventHandler(this.特别说明ToolStripMenuItem_Click);
            // 
            // 反馈建议toolStripMenuItem2
            // 
            this.反馈建议toolStripMenuItem2.Name = "反馈建议toolStripMenuItem2";
            this.反馈建议toolStripMenuItem2.Size = new System.Drawing.Size(68, 21);
            this.反馈建议toolStripMenuItem2.Text = "反馈建议";
            this.反馈建议toolStripMenuItem2.Click += new System.EventHandler(this.反馈建议toolStripMenuItem2_Click);
            // 
            // 文档生成工具ToolStripMenuItem
            // 
            this.文档生成工具ToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.文档生成工具ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.文档生成工具ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.文档生成工具ToolStripMenuItem.Name = "文档生成工具ToolStripMenuItem";
            this.文档生成工具ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.文档生成工具ToolStripMenuItem.Text = "文档生成工具";
            this.文档生成工具ToolStripMenuItem.ToolTipText = "启动文档生成工具";
            this.文档生成工具ToolStripMenuItem.Click += new System.EventHandler(this.文档生成工具ToolStripMenuItem_Click);
            // 
            // txtOutPutPath
            // 
            this.txtOutPutPath.Location = new System.Drawing.Point(110, 90);
            this.txtOutPutPath.Name = "txtOutPutPath";
            this.txtOutPutPath.Size = new System.Drawing.Size(272, 21);
            this.txtOutPutPath.TabIndex = 21;
            this.MyToolTip.SetToolTip(this.txtOutPutPath, "设置生成的SQL脚本输出路径");
            // 
            // radioAdd
            // 
            this.radioAdd.AutoSize = true;
            this.radioAdd.Checked = true;
            this.radioAdd.Location = new System.Drawing.Point(130, 114);
            this.radioAdd.Name = "radioAdd";
            this.radioAdd.Size = new System.Drawing.Size(47, 16);
            this.radioAdd.TabIndex = 25;
            this.radioAdd.TabStop = true;
            this.radioAdd.Text = "新增";
            this.MyToolTip.SetToolTip(this.radioAdd, "以新增表模式生成SQL");
            this.radioAdd.UseVisualStyleBackColor = true;
            // 
            // radioUpdate
            // 
            this.radioUpdate.AutoSize = true;
            this.radioUpdate.Location = new System.Drawing.Point(183, 114);
            this.radioUpdate.Name = "radioUpdate";
            this.radioUpdate.Size = new System.Drawing.Size(47, 16);
            this.radioUpdate.TabIndex = 26;
            this.radioUpdate.Text = "修改";
            this.MyToolTip.SetToolTip(this.radioUpdate, "以修改表模式生成SQL");
            this.radioUpdate.UseVisualStyleBackColor = true;
            // 
            // radioAuto
            // 
            this.radioAuto.AutoSize = true;
            this.radioAuto.Location = new System.Drawing.Point(236, 114);
            this.radioAuto.Name = "radioAuto";
            this.radioAuto.Size = new System.Drawing.Size(71, 16);
            this.radioAuto.TabIndex = 27;
            this.radioAuto.Text = "自动识别";
            this.MyToolTip.SetToolTip(this.radioAuto, "自动判断以新增或修改模式生成SQL");
            this.radioAuto.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "脚本输出路径：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.Message,
            this.Time});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 167);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(428, 155);
            this.dataGridView1.TabIndex = 23;
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "类型";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.ToolTipText = "类型";
            this.Type.Width = 80;
            // 
            // Message
            // 
            this.Message.DataPropertyName = "Message";
            this.Message.HeaderText = "消息";
            this.Message.Name = "Message";
            this.Message.ToolTipText = "消息";
            this.Message.Width = 150;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            this.Time.HeaderText = "时间";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.ToolTipText = "时间";
            this.Time.Width = 140;
            // 
            // mBackgroundWorker
            // 
            this.mBackgroundWorker.WorkerReportsProgress = true;
            this.mBackgroundWorker.WorkerSupportsCancellation = true;
            this.mBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mBackgroundWorker_DoWork);
            this.mBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.mBackgroundWorker_ProgressChanged);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(236, 136);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(109, 23);
            this.btnStop.TabIndex = 24;
            this.btnStop.Text = "停止生成脚本";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Image = global::MyTools.Properties.Resources.文件浏览;
            this.btnOpenFile.Location = new System.Drawing.Point(390, 35);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(29, 25);
            this.btnOpenFile.TabIndex = 16;
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnSerach
            // 
            this.btnSerach.BackColor = System.Drawing.SystemColors.Control;
            this.btnSerach.FlatAppearance.BorderSize = 0;
            this.btnSerach.Image = global::MyTools.Properties.Resources.放大镜;
            this.btnSerach.Location = new System.Drawing.Point(390, 61);
            this.btnSerach.Name = "btnSerach";
            this.btnSerach.Size = new System.Drawing.Size(29, 25);
            this.btnSerach.TabIndex = 14;
            this.btnSerach.UseVisualStyleBackColor = false;
            this.btnSerach.Click += new System.EventHandler(this.btnSerach_Click);
            // 
            // DataDicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 322);
            this.Controls.Add(this.radioAuto);
            this.Controls.Add(this.radioUpdate);
            this.Controls.Add(this.radioAdd);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOutPutPath);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnGenSQL);
            this.Controls.Add(this.textTableList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSerach);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DataDicForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据字典生成工具";
            this.Load += new System.EventHandler(this.DataDicForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenSQL;
        private System.Windows.Forms.TextBox textTableList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSerach;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 说明ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 检查更新ToolStripMenuItem;
        private System.Windows.Forms.ToolTip MyToolTip;
        private System.Windows.Forms.TextBox txtOutPutPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.ComponentModel.BackgroundWorker mBackgroundWorker;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 数据库配置ToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioAdd;
        private System.Windows.Forms.RadioButton radioUpdate;
        private System.Windows.Forms.RadioButton radioAuto;
        private System.Windows.Forms.ToolStripMenuItem 特别说明ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文档生成工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 默认生成模式配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动识别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反馈建议toolStripMenuItem2;
    }
}