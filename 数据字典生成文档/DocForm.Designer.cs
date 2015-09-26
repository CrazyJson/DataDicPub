namespace MyTools.DataDic2Doc
{
    partial class DocForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.数据库配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统过滤规则ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据字典生成工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MyToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtBgnDate = new System.Windows.Forms.DateTimePicker();
            this.txtOutPutPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.radioHtml = new System.Windows.Forms.RadioButton();
            this.radioWord = new System.Windows.Forms.RadioButton();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblFileType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.mBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lblTip = new System.Windows.Forms.Label();
            this.comCheckBoxList1 = new UserControlDLL.ComCheckBoxList();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据库配置ToolStripMenuItem,
            this.系统过滤规则ToolStripMenuItem,
            this.数据字典生成工具ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(537, 25);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 数据库配置ToolStripMenuItem
            // 
            this.数据库配置ToolStripMenuItem.Name = "数据库配置ToolStripMenuItem";
            this.数据库配置ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.数据库配置ToolStripMenuItem.Text = "数据库配置";
            this.数据库配置ToolStripMenuItem.Click += new System.EventHandler(this.数据库配置ToolStripMenuItem_Click);
            // 
            // 系统过滤规则ToolStripMenuItem
            // 
            this.系统过滤规则ToolStripMenuItem.Name = "系统过滤规则ToolStripMenuItem";
            this.系统过滤规则ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.系统过滤规则ToolStripMenuItem.Text = "系统过滤规则";
            this.系统过滤规则ToolStripMenuItem.Click += new System.EventHandler(this.系统过滤规则ToolStripMenuItem_Click);
            // 
            // 数据字典生成工具ToolStripMenuItem
            // 
            this.数据字典生成工具ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.数据字典生成工具ToolStripMenuItem.Name = "数据字典生成工具ToolStripMenuItem";
            this.数据字典生成工具ToolStripMenuItem.Size = new System.Drawing.Size(116, 21);
            this.数据字典生成工具ToolStripMenuItem.Text = "数据字典生成工具";
            this.数据字典生成工具ToolStripMenuItem.Click += new System.EventHandler(this.数据字典生成工具ToolStripMenuItem_Click);
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(105, 57);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(152, 21);
            this.txtTableName.TabIndex = 5;
            this.MyToolTip.SetToolTip(this.txtTableName, "过滤的表名,逗号分隔");
            // 
            // dtEndDate
            // 
            this.dtEndDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndDate.Location = new System.Drawing.Point(347, 28);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(152, 21);
            this.dtEndDate.TabIndex = 3;
            this.MyToolTip.SetToolTip(this.dtEndDate, "表创建日期");
            // 
            // dtBgnDate
            // 
            this.dtBgnDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBgnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBgnDate.Location = new System.Drawing.Point(105, 28);
            this.dtBgnDate.Name = "dtBgnDate";
            this.dtBgnDate.Size = new System.Drawing.Size(152, 21);
            this.dtBgnDate.TabIndex = 0;
            this.MyToolTip.SetToolTip(this.dtBgnDate, "表创建日期");
            // 
            // txtOutPutPath
            // 
            this.txtOutPutPath.Location = new System.Drawing.Point(125, 226);
            this.txtOutPutPath.Name = "txtOutPutPath";
            this.txtOutPutPath.Size = new System.Drawing.Size(353, 21);
            this.txtOutPutPath.TabIndex = 7;
            this.MyToolTip.SetToolTip(this.txtOutPutPath, "导出文件目录及名称");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblTip);
            this.groupBox3.Controls.Add(this.treeView1);
            this.groupBox3.Controls.Add(this.radioHtml);
            this.groupBox3.Controls.Add(this.radioWord);
            this.groupBox3.Controls.Add(this.btnExport);
            this.groupBox3.Controls.Add(this.lblFileType);
            this.groupBox3.Controls.Add(this.txtOutPutPath);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 146);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(537, 290);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "2.查询结果展示及导出";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView1.Location = new System.Drawing.Point(3, 17);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(531, 176);
            this.treeView1.TabIndex = 12;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // radioHtml
            // 
            this.radioHtml.AutoSize = true;
            this.radioHtml.Location = new System.Drawing.Point(299, 199);
            this.radioHtml.Name = "radioHtml";
            this.radioHtml.Size = new System.Drawing.Size(47, 16);
            this.radioHtml.TabIndex = 11;
            this.radioHtml.Text = "Html";
            this.radioHtml.UseVisualStyleBackColor = true;
            this.radioHtml.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioWord
            // 
            this.radioWord.AutoSize = true;
            this.radioWord.Checked = true;
            this.radioWord.Location = new System.Drawing.Point(214, 199);
            this.radioWord.Name = "radioWord";
            this.radioWord.Size = new System.Drawing.Size(47, 16);
            this.radioWord.TabIndex = 10;
            this.radioWord.TabStop = true;
            this.radioWord.Text = "Word";
            this.radioWord.UseVisualStyleBackColor = true;
            this.radioWord.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(222, 255);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(109, 23);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFileType.ForeColor = System.Drawing.Color.Red;
            this.lblFileType.Location = new System.Drawing.Point(484, 229);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(54, 12);
            this.lblFileType.TabIndex = 8;
            this.lblFileType.Text = "(.docx)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "导出文件目录及名称";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comCheckBoxList1);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.txtTableName);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.dtEndDate);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.dtBgnDate);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 25);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(537, 115);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "1.条件过滤";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(286, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "系统";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(222, 84);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(109, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "表名(逗号分隔)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(286, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "到";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "表创建日期从";
            // 
            // mBackgroundWorker
            // 
            this.mBackgroundWorker.WorkerReportsProgress = true;
            this.mBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mBackgroundWorker_DoWork);
            this.mBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.mBackgroundWorker_ProgressChanged);
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Location = new System.Drawing.Point(338, 261);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(0, 12);
            this.lblTip.TabIndex = 13;
            // 
            // comCheckBoxList1
            // 
            this.comCheckBoxList1.DataSource = null;
            this.comCheckBoxList1.Location = new System.Drawing.Point(347, 57);
            this.comCheckBoxList1.Name = "comCheckBoxList1";
            this.comCheckBoxList1.Size = new System.Drawing.Size(150, 20);
            this.comCheckBoxList1.TabIndex = 9;
            this.MyToolTip.SetToolTip(this.comCheckBoxList1, "ERP系统信息");
            // 
            // DocForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 436);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DocForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据字典文档生成工具";
            this.Load += new System.EventHandler(this.DocForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolTip MyToolTip;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtOutPutPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtBgnDate;
        private System.Windows.Forms.RadioButton radioHtml;
        private System.Windows.Forms.RadioButton radioWord;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.ToolStripMenuItem 数据库配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统过滤规则ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据字典生成工具ToolStripMenuItem;
        private UserControlDLL.ComCheckBoxList comCheckBoxList1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lblTip;
        private System.ComponentModel.BackgroundWorker mBackgroundWorker;
    }
}

