using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using MyTools.DataDic.Utils;
using System.IO;
using Novacode;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace MyTools.DataDic2Doc.Entity
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 系统配置文件路径
        /// </summary>
        private static string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\ErpSystem.xml";

        /// <summary>
        /// 读取ERP系统配置信息
        /// </summary>
        /// <returns>List</returns>
        public static List<SystemInfo> GetConfig()
        {
            XDocument dom = XDocument.Load(xmlPath);
            var query = from item in dom.Descendants("System")
                        select new SystemInfo
                        {
                            TableName = item.Attribute("tablename").Value,
                            SystemName = item.Attribute("name").Value,
                            Index = item.Attribute("code").Value,
                            IsDefault = item.Attribute("isDefault") == null || item.Attribute("isDefault").Value=="0"  ? false : true
                        };
            List<SystemInfo> list = query.ToList<SystemInfo>();
            return list;
        }

        /// <summary>
        /// 读取ERP系统配置信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetConfigDT()
        {
            List<SystemInfo> list = GetConfig();
            DataTable dt = new DataTable();
            dt.Columns.Add("Index");
            dt.Columns.Add("SystemName");
            dt.Columns.Add("TableName");
            dt.Columns.Add("IsDefault");
            DataRow dr = null;
            foreach (SystemInfo info in list)
            {
                dr = dt.NewRow();
                dr["Index"] = info.Index;
                dr["SystemName"] = info.SystemName;
                dr["TableName"] = info.TableName;
                dr["IsDefault"] = info.IsDefault;
                dt.Rows.Add(dr);
            }
            return dt;
        }


        /// <summary>
        /// 读取ERP系统配置信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static void SaveConfig(List<SystemInfo> list)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(xmlPath);
            XmlNode root = doc.SelectSingleNode("/Systems");
            root.InnerXml = "";
            XmlElement element = null;
            foreach (SystemInfo entity in list)
            {
                element = doc.CreateElement("System");
                element.SetAttribute("name", entity.SystemName);
                element.SetAttribute("code", entity.Index);
                element.SetAttribute("tablename", entity.TableName);
                element.SetAttribute("isDefault", entity.IsDefault?"1":"0");
                root.AppendChild(element);
            }
            doc.Save(xmlPath);
        }


        /// <summary>
        /// 读取ERP系统配置信息默认选中的值
        /// </summary>
        /// <param name="dt">dt</param>
        /// <returns>List<string></returns>
        public static List<string> GetSelected(DataTable dt)
        {
            var query = (from p in dt.AsEnumerable()
                         where Convert.ToBoolean(p.Field<string>("IsDefault")) == true
                         select p.Field<string>("SystemName")
                       ).ToList<string>();
            return query;
        }

        /// <summary>
        /// 绑定TreeView数据
        /// </summary>
        /// <param name="dt">数据源</param>
        public static void TreeData_Bind(DataTable dt, TreeView treeView)
        {
            treeView.CheckBoxes = true;
            TreeNode node = new TreeNode();
            node.Text = "所有数据";
            node.Name = "所有数据";
            node.Tag = false;
            node.Checked = true;
            treeView.Nodes.Add(node);
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode cnode = new TreeNode();
                cnode.Text = dr["name"].ToString();
                cnode.Name = dr["object_id"].ToString();
                cnode.Tag = true;
                cnode.Checked = true;
                node.Nodes.Add(cnode);
            }
            node.Expand();
        }

        /// <summary>
        /// 将详细表信息转换成List对象
        /// </summary>
        /// <param name="dt">dt</param>
        /// <returns>List<TableInfo></returns>
        public static List<TableInfo> DetailDT2List(DataTable dt, List<int> listTableId)
        {
            List<TableInfo> list = new List<TableInfo>();
            TableInfo entity = null;
            ColumnInfo colEntity = null;
            List<ColumnInfo> listCol = null;
            foreach (int tableid in listTableId)
            {
                DataRow[] drs = dt.Select("TableId=" + tableid);
                entity = new TableInfo();
                listCol = new List<ColumnInfo>();
                foreach (DataRow dr in drs)
                {
                    entity.Code = dr["table_name"].ToString();
                    entity.Name = dr["table_name_c"].ToString();
                    entity.IsUpdate = Convert.ToBoolean(dr["IsUpdate"]);
                    colEntity = new ColumnInfo();
                    colEntity.Code = dr["field_name"].ToString();
                    colEntity.Name = dr["field_name_c"].ToString();
                    colEntity.Sequence = Convert.ToInt32(dr["field_sequence"]);
                    colEntity.Nullable = Convert.ToBoolean(dr["isnullable"]);
                    colEntity.PK = Convert.ToBoolean(dr["pk"]);
                    colEntity.Identity = Convert.ToBoolean(dr["isidentity"]);
                    colEntity.DataType = dr["date_type"].ToString();
                    colEntity.DefaultValue = Common.GetDefaultValue(dr["defaultvalue"].ToString(), colEntity.DataType);
                    colEntity.Width = Common.GetColumnWidth(colEntity.DataType, dr["prec"].ToString(), dr["scale"].ToString());
                    listCol.Add(colEntity);
                }
                entity.ListColumnInfo = listCol;
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 根据读取的表信息导出WORD文档
        /// </summary>
        /// <param name="list">表信息集合</param>
        /// <param name="strExportPath">导出路径</param>
        public static void CreateWord(List<TableInfo> list, string strExportPath, BackgroundWorker bw)
        {
            int fontSize = 9;
            using (DocX doc = DocX.Create(strExportPath, DocumentTypes.Document))
            {
                int proc = 1;
                foreach (TableInfo t in list)
                {
                    Paragraph p1 = doc.InsertParagraph();
                    p1.AppendLine(string.IsNullOrEmpty(t.Name) ? t.Code : t.Name + "\n").Bold();



                    Table table = doc.AddTable(t.ListColumnInfo.Count + 4, 11);
                    table.Design = TableDesign.TableGrid;
                    table.Alignment = Alignment.center;

                    List<Row> rows = table.Rows;
                    Row row0 = rows[0];
                    row0.MergeCells(0, 1);
                    row0.Cells[0].Paragraphs[0].Append("数据表中文名称").FontSize(fontSize);
                    row0.MergeCells(1, 2);
                    row0.Cells[1].Paragraphs[0].Append(t.Name).FontSize(fontSize);
                    row0.MergeCells(2, 4);
                    row0.Cells[2].Paragraphs[0].Append("修改说明").FontSize(fontSize);
                    row0.MergeCells(3, 6);
                    row0.Cells[3].Paragraphs[0].Append(t.IsUpdate ? "调整" : "新增").FontSize(fontSize).Color(t.IsUpdate ? Color.Red : Color.Blue);

                    row0.Cells[0].Width = 143;
                    row0.Cells[1].Width = 211;
                    row0.Cells[2].Width = 127;
                    row0.Cells[3].Width = 149;
                    row0.Height = 25;

                    Row row1 = rows[1];
                    row1.MergeCells(0, 1);
                    row1.Cells[0].Paragraphs[0].Append("数据表英文名称").FontSize(fontSize);
                    row1.MergeCells(1, 9);
                    row1.Cells[1].Paragraphs[0].Append(t.Code).FontSize(fontSize);
                    row1.Cells[0].Width = 143;
                    row1.Cells[1].Width = 487;
                    row1.Height = 25;

                    Row row2 = rows[2];
                    row2.MergeCells(0, 1);
                    row2.Cells[0].Paragraphs[0].Append("功能简述").FontSize(fontSize);
                    row2.MergeCells(1, 9);
                    row2.Cells[1].Paragraphs[0].Append("").FontSize(fontSize);
                    row2.Cells[0].Width = 143;
                    row2.Cells[1].Width = 487;
                    row2.Height = 25;

                    Row row3 = rows[3];
                    row3.Cells[0].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[1].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[2].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[3].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[4].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[5].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[6].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[7].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[8].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[9].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[10].FillColor = Color.FromArgb(226, 226, 226);
                    row3.Cells[0].Width = 35;
                    row3.Cells[1].Width = 108;
                    row3.Cells[2].Width = 116;
                    row3.Cells[3].Width = 95;
                    row3.Cells[4].Width = 51;
                    row3.Cells[5].Width = 36;
                    row3.Cells[6].Width = 40;
                    row3.Cells[7].Width = 35;
                    row3.Cells[8].Width = 40;
                    row3.Cells[9].Width = 39;
                    row3.Cells[10].Width = 35;

                    row3.Cells[0].Paragraphs[0].Append("序号").Bold().FontSize(fontSize);
                    row3.Cells[1].Paragraphs[0].Append("字段中文名").Bold().FontSize(fontSize);
                    row3.Cells[2].Paragraphs[0].Append("字段英文名").Bold().FontSize(fontSize);
                    row3.Cells[3].Paragraphs[0].Append("数据类型").Bold().FontSize(fontSize);
                    row3.Cells[4].Paragraphs[0].Append("宽度").Bold().FontSize(fontSize);
                    row3.Cells[5].Paragraphs[0].Append("约束").Bold().FontSize(fontSize);
                    row3.Cells[6].Paragraphs[0].Append("默认值").Bold().FontSize(fontSize);
                    row3.Cells[7].Paragraphs[0].Append("空值").Bold().FontSize(fontSize);
                    row3.Cells[8].Paragraphs[0].Append("枚举&说明").Bold().FontSize(fontSize);
                    row3.Cells[9].Paragraphs[0].Append("自增").Bold().FontSize(fontSize);
                    row3.Cells[10].Paragraphs[0].Append("修改说明").Bold().FontSize(fontSize);

                    Row row = null;
                    ColumnInfo info = null;
                    for (int i = 0; i < t.ListColumnInfo.Count; i++)
                    {
                        row = rows[i + 4];
                        info = t.ListColumnInfo[i];
                        row.Cells[0].Paragraphs[0].Append(info.Sequence.ToString()).FontSize(fontSize);
                        row.Cells[1].Paragraphs[0].Append(info.Name).FontSize(fontSize);
                        row.Cells[2].Paragraphs[0].Append(info.Code).FontSize(fontSize);
                        row.Cells[3].Paragraphs[0].Append(info.DataType).FontSize(fontSize);
                        row.Cells[4].Paragraphs[0].Append(info.Width).FontSize(fontSize);
                        row.Cells[5].Paragraphs[0].Append(info.PK ? "PK" : "").FontSize(fontSize);
                        row.Cells[6].Paragraphs[0].Append(info.DefaultValue).FontSize(fontSize);
                        row.Cells[7].Paragraphs[0].Append(info.Nullable ? "" : "N").FontSize(fontSize);
                        row.Cells[8].Paragraphs[0].Append("").FontSize(fontSize);
                        row.Cells[9].Paragraphs[0].Append(info.Identity ? "Y" : "").FontSize(fontSize);
                        row.Cells[10].Paragraphs[0].Append("").FontSize(fontSize);


                        row.Cells[0].Width = 35;
                        row.Cells[1].Width = 108;
                        row.Cells[2].Width = 116;
                        row.Cells[3].Width = 95;
                        row.Cells[4].Width = 51;
                        row.Cells[5].Width = 36;
                        row.Cells[6].Width = 40;
                        row.Cells[7].Width = 35;
                        row.Cells[8].Width = 40;
                        row.Cells[9].Width = 39;
                        row.Cells[10].Width = 35;

                        row.Height = 35;
                    }
                    p1.InsertTableAfterSelf(table);
                    bw.ReportProgress(proc * 100 / list.Count, "Process");
                    proc++;
                }
                doc.Save();
            }
        }

        /// <summary>
        /// 根据读取的表信息导出HTML文档
        /// </summary>
        /// <param name="list">表信息集合</param>
        /// <param name="strExportPath">导出路径</param>
        public static void CreateHtml(List<TableInfo> list, string strExportPath, string strDBName)
        {
            Hashtable param = null;
            string tPath = AppDomain.CurrentDomain.BaseDirectory + @"Common\html.vm";
            param = new Hashtable();
            param.Add("TableList", list);
            param.Add("DBName", strDBName);
            FileGen.GetFile(tPath, param, strExportPath);
        }
    }
}
