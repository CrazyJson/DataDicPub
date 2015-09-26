using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyTools.DataDic.Utils;

namespace MyTools.DataDic
{
    public partial class SerachForm : Form
    {
        public List<string> listTableName = new List<string>();

        private static bool complete = false;

        /// <summary>
        /// treeview数据源
        /// </summary>
        private List<PhysicalDiagramInfo> list = null;

        public SerachForm()
        {
            InitializeComponent();
        }

        public SerachForm(List<PhysicalDiagramInfo> list)
        {
            InitializeComponent();
            this.list = list;
        }

        private void SerachForm_Load(object sender, EventArgs e)
        {
            Data_Bind(list);
        }

        #region "TreeView数据绑定"

        /// <summary>
        /// 绑定TreeView数据源
        /// </summary>
        private void Data_Bind(List<PhysicalDiagramInfo> list)
        {
            treeView1.CheckBoxes = true;
            PhysicalDiagramInfo pd = list[0];
            TreeNode node = new TreeNode();
            node.Text = pd.Name;
            node.Name = pd.Id;
            node.Tag = pd.IfEnd;
            node.Checked = false;
            treeView1.Nodes.Add(node);
            AddReplies(list, node);
            node.Expand();
        }

        /// <summary>  
        /// 递归绑定子节点  
        /// </summary>  
        private void AddReplies(List<PhysicalDiagramInfo> list, TreeNode node)
        {
            IEnumerable<PhysicalDiagramInfo> query =
                               from c in list
                               where c.PhyParentId == node.Name
                               orderby c.Name
                               select c;
            List<PhysicalDiagramInfo> listTemp = query.ToList<PhysicalDiagramInfo>();
            foreach (PhysicalDiagramInfo pd in listTemp)
            {
                TreeNode cnode = new TreeNode();
                cnode.Text = pd.Name;
                cnode.Name = pd.Id;
                cnode.Tag = pd.IfEnd;
                cnode.Checked = false;
                node.Nodes.Add(cnode);
                node.Expand();
                AddReplies(list, cnode);
            }
        }

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


        #endregion

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            listTableName.Clear();
            GetChecked(treeView1.Nodes[0]);
            if (listTableName.Count == 0)
            {
                MessageBox.Show("请选择要生成的表！");
                return;
            }
            this.Tag = listTableName;
            this.Close();
        }

        private void GetChecked(TreeNode node)
        {
            if (node != null)
            {
                if (Convert.ToBoolean(node.Tag) && node.Checked)
                {
                    listTableName.Add(node.Text);
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

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSerach_Click(object sender, EventArgs e)
        {

            treeView1.Focus();
            //一级目录
            TreeNode node = treeView1.SelectedNode;
            complete = false;
            SetSearch(node);
            
        }

        private void SetSearch(TreeNode node)
        {
            if (complete)
            {
                return;
            }
            if (node != null)
            {
                if (Convert.ToBoolean(node.Tag))
                {
                    if (node.Text.ToLower().Contains(txtTableName.Text.Trim().ToLower()))
                    {
                        node.Checked = true;
                        node.Expand();
                        treeView1_AfterCheck(null, new TreeViewEventArgs(node, TreeViewAction.ByKeyboard));
                        complete = true;
                        return;
                    }
                }
                else
                {
                    TreeNodeCollection tcl = node.Nodes;
                    if (tcl != null && tcl.Count > 0)
                    {
                        foreach (TreeNode nodeTemp in tcl)
                        {
                            if (!complete)
                            {
                                SetSearch(nodeTemp);
                            }
                        }
                    }
                }
            }
        }

    }
}
