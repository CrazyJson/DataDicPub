using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Xml.Linq;
using MyTools.DataDic2Doc.Entity;
using MyTools.DataDic.Utils;
using System.IO;

namespace MyTools.DataDic2Doc
{
    public partial class DocForm : Form
    {
        /// <summary>
        /// 文件输出格式
        /// </summary>
        private string strFileType = String.Empty;

        /// <summary>
        /// 要生成的表
        /// </summary>
        public List<int> listTableId = null;

        public DocForm()
        {
            InitializeComponent();
        }

        private void DocForm_Load(object sender, EventArgs e)
        {
            try
            {
                //设置输出文件路径
                RegistryKey folders = OpenRegistryPath(Registry.CurrentUser, @"/software/microsoft/windows/currentversion/explorer/shell folders");
                // Windows用户桌面路径  
                string desktopPath = folders.GetValue("Desktop").ToString();
                txtOutPutPath.Text = desktopPath + @"\" + DateTime.Now.ToString("yyyyMMddHHmmss");

                DataTable dt = CommonHelper.GetConfigDT();
                comCheckBoxList1.DataSource = dt;
                comCheckBoxList1.DisplayMember = "SystemName";
                comCheckBoxList1.ValueMember = "TableName";
                comCheckBoxList1.SetSelectValue(CommonHelper.GetSelected(dt));

                Common.Log(Properties.Settings.Default.APPID, "数据字典文档生成工具", "启动程序", System.Net.Dns.GetHostName(), "", "", "","");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }


        private RegistryKey OpenRegistryPath(RegistryKey root, string s)
        {
            s = s.Remove(0, 1) + @"/";
            while (s.IndexOf(@"/") != -1)
            {
                root = root.OpenSubKey(s.Substring(0, s.IndexOf(@"/")));
                s = s.Remove(0, s.IndexOf(@"/") + 1);
            }
            return root;
        }

        /// <summary>
        /// 输出文件格式切换监听事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = sender as RadioButton;
            strFileType = r.Text;
            if (strFileType == "Word")
            {
                lblFileType.Text = "(.docx)";
            }
            else if (strFileType == "Html")
            {
                lblFileType.Text = "(.html)";
            }
            else
            {
                return;
            }
        }

        #region "菜单栏事件"

        private void 数据字典生成工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //文档生成工具程序集名称
            string docExe = "MyTools.DataDic";
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            //设置外部程序名         
            Info.FileName = docExe + ".exe";
            //设置外部程序工作目录为           
            Info.WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            //最小化方式启动        
            Info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            //声明一个程序类          
            System.Diagnostics.Process Proc;
            try
            {
                System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName(docExe);
                if (p != null && p.Length > 0)
                {
                    MessageBox.Show("数据字典生成工具已经运行！", "提示");
                    return;
                }
                Proc = System.Diagnostics.Process.Start(Info);
                System.Threading.Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据字典生成工具启动失败," + ex.Message, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void 系统过滤规则ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemConfig systemConfig = new SystemConfig();
            if (systemConfig.ShowDialog() == DialogResult.OK)
            {
                DataTable dt= CommonHelper.GetConfigDT();
                comCheckBoxList1.DataSource = dt;
                comCheckBoxList1.DisplayMember = "SystemName";
                comCheckBoxList1.ValueMember = "TableName";
                comCheckBoxList1.SetSelectValue(CommonHelper.GetSelected(dt));
            }
        }

        private void 数据库配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerLogin login = new ServerLogin();
            login.ShowDialog();
        }

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strCon = Properties.Settings.Default.ConnectionString;
            if (String.IsNullOrEmpty(strCon))
            {
                MessageBox.Show("请先进行数据库连接配置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Common.IsCorrectConnection(strCon);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                StringBuilder sb = new StringBuilder();
                //起始日期
                string bgnDate = dtBgnDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //截止日期
                string endDate = dtEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //过滤表名
                string strTableName = txtTableName.Text;
                //系统名称
                List<string> list = comCheckBoxList1.GetList();


                sb.AppendFormat(" modify_date BETWEEN '{0}' AND '{1}' ", bgnDate, endDate);

                if (!string.IsNullOrEmpty(strTableName))
                {
                    sb.AppendFormat(" AND name IN ('{0}')", strTableName.Replace("'", "''").Replace(",", "','"));
                }

                if (list != null && list.Count > 0)
                {
                    List<SystemInfo> listInfo = CommonHelper.GetConfig();
                    var query = (from p in listInfo
                                 join q in list
                                 on p.SystemName equals q
                                 select p).ToList<SystemInfo>();
                    sb.Append(" AND (");
                    for (int i = 0; i < query.Count; i++)
                    {
                        SystemInfo info = query[i];
                        sb.AppendFormat(" CHARINDEX('{0}',name,0)=1 ", info.TableName);
                        if (i + 1 < query.Count)
                        {
                            sb.Append(" OR ");
                        }
                        else
                        {
                            sb.Append(" ) ");
                        }
                    }
                }

                DataTable dt = DataDicService.GetDCTableInfo(sb.ToString(), strCon);
                treeView1.Nodes.Clear();
                CommonHelper.TreeData_Bind(dt, treeView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region "TreeView相关"
        /// <summary>
        /// 实现全选，反选
        /// </summary>
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                CheckAllChildNodes(e.Node, e.Node.Checked);
                //选中父节点 
                bool bol = true;
                if (e.Node.Parent != null)
                {
                    for (int i = 0; i < e.Node.Parent.Nodes.Count; i++)
                    {
                        if (!e.Node.Parent.Nodes[i].Checked)
                            bol = false;
                    }
                    e.Node.Parent.Checked = bol;
                }
            }
        }

        public void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void GetChecked(TreeNode node)
        {
            if (node != null)
            {
                if (Convert.ToBoolean(node.Tag) && node.Checked)
                {
                    listTableId.Add(Convert.ToInt32(node.Name));
                    return;
                }
                else
                {
                    TreeNodeCollection tcl = node.Nodes;
                    if (tcl != null && tcl.Count > 0)
                    {
                        foreach (TreeNode nodeTemp in tcl)
                        {
                            GetChecked(nodeTemp);
                        }
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                listTableId = new List<int>();
                if (treeView1.Nodes.Count == 0)
                {
                    MessageBox.Show("请选择要导出的表！");
                    return;
                }
                GetChecked(treeView1.Nodes[0]);
                if (listTableId.Count == 0)
                {
                    MessageBox.Show("请选择要导出的表！");
                    return;
                }

                if (string.IsNullOrEmpty(txtOutPutPath.Text.Trim()))
                {
                    MessageBox.Show("请输入导出文件目录及名称！");
                    return;
                }
                int index=txtOutPutPath.Text.Trim().LastIndexOf('\\');
                if (index < 0)
                {
                    MessageBox.Show("导出路径有误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string dir = txtOutPutPath.Text.Trim().Substring(0, index + 1);
                if (!Directory.Exists(dir))
                {
                    DialogResult dr = MessageBox.Show("导出路径不存在，是否创建！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr != DialogResult.OK)
                    {
                        return;
                    }
                    Directory.CreateDirectory(dir);
                }
                if (String.IsNullOrEmpty(txtOutPutPath.Text.Trim().Substring(index + 1)))
                {
                    MessageBox.Show("请输入导出文件名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string strCon = Properties.Settings.Default.ConnectionString;
                if (String.IsNullOrEmpty(strCon))
                {
                    MessageBox.Show("请先进行数据库连接配置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    Common.IsCorrectConnection(strCon);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < listTableId.Count; i++)
                {
                    if (i + 1 < listTableId.Count)
                    {
                        sb.Append(listTableId[i].ToString() + ",");
                    }
                    else
                    {
                        sb.Append(listTableId[i]);
                    }
                }
                lblTip.Text = "正在导出，请稍等....";
                btnExport.Enabled = false;
                Common.Log(Properties.Settings.Default.APPID, "数据字典文档生成工具", "生成文档", System.Net.Dns.GetHostName(), radioWord.Checked ? "word" : "html", "本次共操作" + listTableId.Count + "张表！", strCon,"");
                mBackgroundWorker.RunWorkerAsync(new string[] { sb.ToString(), strCon });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取导出文档类型
        /// </summary>
        /// <returns>0:Word 1:Html</returns>
        public int GetDocType()
        {
            if (radioWord.Checked)
            {
                return 0;
            }
            if (radioHtml.Checked)
            {
                return 1;
            }
            return 0;
        }

        #region "进程相关内容"

        /// <summary>
        /// 作业进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            try
            {
                string[] args = (string[])e.Argument;
                DataTable dt = DataDicService.GetDCTableDetailInfo(args[0], args[1]);
                List<TableInfo> listTableInfo = CommonHelper.DetailDT2List(dt, listTableId);

                int type = GetDocType();
                if (type == 0)
                {
                    string strExportPath = txtOutPutPath.Text.Trim() + ".docx";
                    CommonHelper.CreateWord(listTableInfo, strExportPath, bw);
                }
                else if (type == 1)
                {
                    string strExportPath = txtOutPutPath.Text.Trim() + ".html";
                    CommonHelper.CreateHtml(listTableInfo, strExportPath, Properties.Settings.Default.ServerDBName);
                }
                bw.ReportProgress(100, "Complete");
            }
            catch (Exception ex)
            {
                bw.ReportProgress(0, ex.Message);
            }
        }

        /// <summary>
        /// 进度提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string type = (string)e.UserState;
            if (type == "Process")
            {
                lblTip.Text = "完成进度:" + e.ProgressPercentage.ToString() + "%";
            }
            else if (type == "Complete")
            {
                lblTip.Text = "";
                btnExport.Enabled = true;
                MessageBox.Show("导出完成！", "提示", MessageBoxButtons.OK);
            }
            else
            {
                lblTip.Text = "";
                btnExport.Enabled = true;
                MessageBox.Show(type, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
