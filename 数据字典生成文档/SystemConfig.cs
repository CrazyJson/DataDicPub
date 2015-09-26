using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyTools.DataDic2Doc.Entity;
using System.Xml.Linq;

namespace MyTools.DataDic2Doc
{
    public partial class SystemConfig : Form
    {
        public SystemConfig()
        {
            InitializeComponent();
        }

        private void SystemConfig_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = CommonHelper.GetConfigDT() ;
                //SetDefaultSelected(0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 设置dataGridView默认选中行
        /// </summary>
        private void SetDefaultSelected(int i)
        {
            if (dataGridView1.Rows.Count > i)
            {
                dataGridView1.Rows[i].Selected = true;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\ErpSystem.xml";
                int cellCount = dataGridView1.ColumnCount;
                int rowCount = dataGridView1.RowCount - 1;
                string temp = "";
                List<SystemInfo> list = new List<SystemInfo>();
                SystemInfo info = null;
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 1; j < cellCount; j++)
                    {
                        temp = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        if (string.IsNullOrEmpty(temp))
                        {
                            if (j == 1)
                            {
                                temp = "系统名称";
                            }
                            else if (j == 2)
                            {
                                temp = "表名前缀";
                            }
                            else
                            {
                                continue;
                            }
                            MessageBox.Show("请录入第" + (i + 1).ToString() + "行,”" + temp + "“的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    info = new SystemInfo();
                    info.Index = (i + 1).ToString();
                    info.SystemName = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    info.TableName = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    info.IsDefault = DBNull.Value==dataGridView1.Rows[i].Cells[3].Value || Convert.ToBoolean(dataGridView1.Rows[i].Cells[3].Value)==false?false:true;
                    list.Add(info);
                }
                var listGroup = (from p in list
                             group p by p.SystemName into g
                             select new
                              {
                                  SystemName=g.Key,
                                  count = g.Count()
                              } into c
                             where c.count > 1
                             select c).ToList();
                if (listGroup.Count > 0)
                {
                    MessageBox.Show(listGroup[0].SystemName+"名称重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                var listTableName = (from p in list
                                 group p by p.TableName into g
                                 select new
                                 {
                                     TableName = g.Key,
                                     count = g.Count()
                                 } into c
                                 where c.count > 1
                                 select c).ToList();
                if (listTableName.Count > 0)
                {
                    MessageBox.Show(listTableName[0].TableName + "名称重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CommonHelper.SaveConfig(list);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                //选择最后一行
                if (index == dataGridView1.RowCount - 1)
                {
                    MessageBox.Show("最后一行不允许删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataRowView drv = dataGridView1.SelectedRows[0].DataBoundItem as DataRowView;
                drv.Row.Delete();
                //SetDefaultSelected(index);
                dataGridView1.Refresh();
            }
            else
            {
                MessageBox.Show("请选择要删除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
