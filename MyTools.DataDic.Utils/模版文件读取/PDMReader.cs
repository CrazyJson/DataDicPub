using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace MyTools.DataDic.Utils
{
    /// <summary>
    /// 解析PDM文件读取信息
    /// </summary>
    public class PDMReader : IReader
    {
        /// <summary>
        /// PDM文件路径
        /// </summary>
        private string _pdmPath;

        private XmlDocument xmlDoc = null;

        /// <summary>
        /// 校验文件路径是否存在
        /// </summary>
        /// <param name="pdmPath">PDM文件路径</param>
        private void CheckPath(string pdmPath)
        {
            if (string.IsNullOrEmpty(pdmPath))
            {
                throw new Exception("文件路径不能为空！");
            }
            if (!pdmPath.EndsWith(".pdm", true, null))
            {
                throw new Exception("文件格式不正确，请选择PDM文件！");
            }
            if (!File.Exists(pdmPath))
            {
                throw new Exception("指定文件不存在！");
            }
        }

        /// <summary>
        /// 构造函数 根据路径生成所有表的SQL
        /// </summary>
        /// <param name="pdmPath">PDM文件路径</param>
        public PDMReader(string pdmPath)
        {
            try
            {
                CheckPath(pdmPath);
                _pdmPath = pdmPath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 替换文件里面的低位字符
        /// </summary>
        /// <param name="tmp"></param>
        /// <returns></returns>
        private string ReplaceLowOrderASCIICharacters(string tmp)
        {
            StringBuilder info = new StringBuilder();
            foreach (char cc in tmp)
            {
                int ss = (int)cc;
                if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && (ss <= 12)) || ((ss >= 14) && (ss <= 32)) || ss == 65535)
                    info.Append(" ");
                else info.Append(cc);
            }
            return info.ToString();
        }

        /// <summary>
        /// 读取xml文件返回XmlDocument对象
        /// </summary>
        /// <returns>XmlDocument对象</returns>
        private XmlDocument GetXmlDom()
        {
            try
            {
                if (xmlDoc == null)
                {
                    StreamReader sr = new StreamReader(_pdmPath);
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(ReplaceLowOrderASCIICharacters(sr.ReadToEnd()));                 
                }
                return xmlDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置xml文件命名空间
        /// </summary>
        /// <returns>XmlNamespaceManager</returns>
        private XmlNamespaceManager GetXmlNamespace()
        {
            XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(GetXmlDom().NameTable);
            xmlnsManager.AddNamespace("a", "attribute");
            xmlnsManager.AddNamespace("c", "collection");
            xmlnsManager.AddNamespace("o", "object");
            return xmlnsManager;
        }

        /// <summary>
        /// 从中XML读取表信息
        /// </summary>
        /// <returns> List</returns>
        public List<TableInfo> GetTableInfo()
        {
            try
            {
                XmlDocument xmlDoc = GetXmlDom();
                XmlNamespaceManager xmlnsManager = GetXmlNamespace();
                XmlNode xnTables = xmlDoc.SelectSingleNode("//" + "c:Tables", xmlnsManager);
                List<TableInfo> Tables = new List<TableInfo>();
                foreach (XmlNode xnTable in xnTables.ChildNodes)
                {
                    Tables.Add(GetTable(xnTable));
                }
                return Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从中XML读取物理图信息
        /// </summary>
        /// <returns> List</returns>
        public List<PhysicalDiagramInfo> GetPDInfo()
        {
            try
            {
                XmlDocument xmlDoc = GetXmlDom();
                XmlNamespaceManager xmlnsManager = GetXmlNamespace();
                XmlNodeList xnPDs = xmlDoc.SelectNodes("//c:PhysicalDiagrams/o:PhysicalDiagram", xmlnsManager);
                XmlNodeList xnTables = null;
                List<PhysicalDiagramInfo> PDList = new List<PhysicalDiagramInfo>();
                PhysicalDiagramInfo pdInfo = new PhysicalDiagramInfo();
                string pid = System.Guid.NewGuid().ToString();
                string pid1 = "";
                pdInfo.Id = pid;
                pdInfo.Name = "所有数据";
                PDList.Add(pdInfo);
                foreach (XmlNode xnPD in xnPDs)
                {
                    XmlNode tempNode = xnPD.Clone();
                    pdInfo = new PhysicalDiagramInfo();
                    pid1 = tempNode.SelectSingleNode("//a:ObjectID", xmlnsManager).InnerText;
                    pdInfo.Id = pid1;
                    pdInfo.PhyParentId = pid;
                    pdInfo.Name = tempNode.SelectSingleNode("//a:Name", xmlnsManager).InnerText;

                    xnTables = tempNode.SelectNodes("//c:Symbols/o:TableSymbol/c:Object/o:Table", xmlnsManager);
                    if (xnTables != null && xnTables.Count > 0)
                    {
                        PDList.Add(pdInfo);
                        foreach (XmlNode node in xnTables)
                        {
                            pdInfo = new PhysicalDiagramInfo();
                            pdInfo.PhyParentId = pid1;
                            pdInfo.IfEnd = true;
                            XmlNode tempTableNode = xnPD.SelectSingleNode("//c:Tables/o:Table[@Id='" + node.Attributes["Ref"].Value + "']", xmlnsManager);
                            if (tempTableNode != null)
                            {
                                tempTableNode = tempTableNode.Clone();
                                pdInfo.Name = tempTableNode.SelectSingleNode("//a:Code", xmlnsManager).InnerText;
                                pdInfo.Id = tempTableNode.SelectSingleNode("//a:ObjectID", xmlnsManager).InnerText;
                                PDList.Add(pdInfo);
                            }
                        }
                    }
                }
                return PDList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>表信息</returns>
        public List<TableInfo> GetTableColumnName(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception("参数空异常！");
            }
            List<TableInfo> list = GetTableInfo();
            if (list != null && list.Count > 0)
            {
                IEnumerable<TableInfo> query =
                                 from c in list
                                 where c.Code == tableName
                                 select c;
                return query.ToList<TableInfo>();
            }
            return null;
        }

        /// <summary>
        /// 获取节点中表的信息
        /// </summary>
        /// <param name="xnTable">xmlNode</param>
        /// <returns>表信息</returns>
        private TableInfo GetTable(XmlNode xnTable)
        {
            try
            {
                TableInfo mTable = new TableInfo();
                XmlElement xe = (XmlElement)xnTable;
                mTable.TableID = xe.GetAttribute("Id");
                XmlNodeList xnTProperty = xe.ChildNodes;
                foreach (XmlNode xnP in xnTProperty)
                {
                    switch (xnP.Name)
                    {
                        //表的ID
                        case "a:ObjectID":
                            mTable.TableObjectID = xnP.InnerText;
                            break;
                        //表的中文名称
                        case "a:Name":
                            mTable.Name = xnP.InnerText;
                            break;
                        //表的英文名称
                        case "a:Code":
                            mTable.Code = xnP.InnerText;
                            break;
                        //表的描述
                        case "a:Comment":
                            mTable.Comment = xnP.InnerText;
                            break;
                        //表的列信息
                        case "c:Columns":
                            InitColumns(xnP, mTable);
                            break;
                        //表的主键信息
                        case "c:Keys":
                            InitKeys(xnP, mTable);
                            break;
                        default:
                            break;
                    }
                }
                if (string.IsNullOrEmpty(mTable.Comment))
                {
                    mTable.Comment = mTable.Name;
                }
                if (mTable.ListPkKeyInfo != null && mTable.ListPkKeyInfo.Count > 0)
                {
                    foreach (PkKeyInfo pkInfo in mTable.ListPkKeyInfo)
                    {
                        ColumnInfo info = mTable.ListColumnInfo.Single(c => c.ColumnId == pkInfo.ColumnId);
                        pkInfo.Name = info.Code;
                        info.PK = true;
                        mTable.PkKeyNameList = mTable.PkKeyNameList + pkInfo.Name + ",";
                    }
                }
                //杜冬军2014-05-16 修改没有主键  生成SQL有问题的BUG  V1.4
                else
                {
                    mTable.ListPkKeyInfo=new List<PkKeyInfo>();
                }
                if (!string.IsNullOrEmpty(mTable.PkKeyNameList))
                {
                    mTable.PkKeyNameList = mTable.PkKeyNameList.Substring(0, mTable.PkKeyNameList.Length - 1);
                }
                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取表中的列信息
        /// </summary>
        /// <param name="xnColumns">列节点</param>
        /// <param name="pTable">表信息</param>
        private void InitColumns(XmlNode xnColumns, TableInfo pTable)
        {
            int i = 1;
            List<ColumnInfo> list = new List<ColumnInfo>();
            pTable.ListColumnInfo = list;
            foreach (XmlNode xnColumn in xnColumns)
            {
                ColumnInfo mColumn = GetColumn(xnColumn);
                mColumn.Sequence = i;
                pTable.ListColumnInfo.Add(mColumn);
                i++;
            }
        }

        /// <summary>
        /// 获取表的主键信息
        /// </summary>
        /// <param name="xnKeys">节点</param>
        /// <param name="pTable">表信息</param>
        private void InitKeys(XmlNode xnKeys, TableInfo pTable)
        {
            List<PkKeyInfo> list = new List<PkKeyInfo>();
            if (xnKeys != null && xnKeys.ChildNodes.Count > 0)
            {
                XmlNode xnKey = xnKeys.ChildNodes[0];
                foreach (XmlNode xnP in xnKey.ChildNodes)
                {
                    if (xnP.Name == "c:Key.Columns")
                    {
                        foreach (XmlNode xn in xnP.ChildNodes)
                        {
                            list.Add(new PkKeyInfo(((XmlElement)xn).GetAttribute("Ref")));
                        }
                    }
                }
            }
            pTable.ListPkKeyInfo = list;
        }


        /// <summary>
        /// 获取列信息
        /// </summary>
        /// <param name="xnColumn">列节点</param>
        /// <returns>列信息</returns>
        private ColumnInfo GetColumn(XmlNode xnColumn)
        {

            ColumnInfo mColumn = new ColumnInfo();
            XmlElement xe = (XmlElement)xnColumn;
            mColumn.ColumnId = xe.GetAttribute("Id");
            XmlNodeList xnCProperty = xe.ChildNodes;
            foreach (XmlNode xnP in xnCProperty)
            {
                switch (xnP.Name)
                {
                    //列ID
                    case "a:ObjectID":
                        mColumn.ColumnObjectId = xnP.InnerText;
                        break;
                    //列中文名称
                    case "a:Name":
                        mColumn.Name = xnP.InnerText;
                        break;
                    //列英文名称
                    case "a:Code":
                        mColumn.Code = xnP.InnerText;
                        break;
                    //列描述
                    case "a:Comment":
                        mColumn.Comment = xnP.InnerText;
                        break;
                    //列数据类型
                    case "a:DataType":
                        mColumn.DataTypeStr = xnP.InnerText.Replace("（", "(").Replace("）", ")");
                        mColumn.DataType = Common.GetColumnDataType(mColumn.DataTypeStr);
                        mColumn.Width = Common.GetColumnWidth(mColumn.DataTypeStr);
                        break;
                    //列宽度
                    case "a:Length":
                        mColumn.Length = xnP.InnerText;
                        break;
                    //列是否自增
                    case "a:Identity":
                        mColumn.Identity = Common.ConvertToBooleanPG(xnP.InnerText);
                        break;
                    //列默认值
                    case "a:DefaultValue":
                        mColumn.DefaultValue = xnP.InnerText;
                        break;
                    //列是否可为空
                    case "a:Mandatory":
                        mColumn.Nullable = Common.ConvertToBooleanPG(xnP.InnerText);
                        break;
                    default:
                        break;
                }
            }
            if (string.IsNullOrEmpty(mColumn.Comment))
            {
                mColumn.Comment = mColumn.Name;
            }
            if (string.IsNullOrEmpty(mColumn.DefaultValue))
            {
                mColumn.DefaultValue = "";
            }
            return mColumn;
        }       
    }
}
