using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Novacode;


namespace MyTools.DataDic.Utils
{
    /// <summary>
    /// 解析Word文件,读取信息
    /// 2014-07-08优化后重写原先NPOI读取方案
    /// </summary>
    public class WordReaderNew : IReader
    {
        /// <summary>
        /// Word文件路径
        /// </summary>
        private string _pdmPath;

        private List<TableInfo> mTables = null;

        private DocX doc = null;

        /// <summary>
        /// 校验文件路径是否存在
        /// </summary>
        /// <param name="pdmPath">Word文件路径</param>
        private void CheckPath(string pdmPath)
        {
            if (string.IsNullOrEmpty(pdmPath))
            {
                throw new Exception("文件路径不能为空！");
            }
            if (!pdmPath.EndsWith(".doc", true, null) && !pdmPath.EndsWith(".docx", true, null))
            {
                throw new Exception("文件格式不正确，请选择Word文件！");
            }
            if (!File.Exists(pdmPath))
            {
                throw new Exception("指定文件不存在！");
            }
        }

        /// <summary>
        /// 构造函数 根据路径生成所有表的SQL
        /// </summary>
        /// <param name="pdmPath">Word文件路径</param>
        public WordReaderNew(string pdmPath)
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
        /// 初始化Word
        /// </summary>
        /// <returns>DocX</returns>
        private DocX InitDocX()
        {
            if (doc == null)
            {
                try
                {
                    using (FileStream stream = new FileStream(_pdmPath, FileMode.Open, FileAccess.Read))
                    {
                        doc = DocX.Load(stream);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return doc;
        }

        /// <summary>
        /// 从Word读取表信息
        /// </summary>
        /// <returns> List</returns>
        public List<TableInfo> GetTableInfo()
        {
            try
            {
                DocX doc = InitDocX();
                List<TableInfo> iTables = new List<TableInfo>();
                var tables = doc.Tables;
                foreach (var table in tables)
                {
                    var row = table.Rows[0];
                    if (row != null)
                    {
                        if (row.Cells[0].Paragraphs[0].Text.Trim() == "数据表中文名称")
                        {
                            iTables.Add(GetTable(table));
                        }
                    }
                }
                mTables = iTables;
                return iTables;
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
                List<PhysicalDiagramInfo> PDList = new List<PhysicalDiagramInfo>();
                DocX doc = InitDocX();
                if (mTables == null)
                {
                    mTables = GetTableInfo();
                }
                PhysicalDiagramInfo pdInfo = new PhysicalDiagramInfo();
                string pid = System.Guid.NewGuid().ToString();
                pdInfo.Id = pid;
                pdInfo.Name = "所有数据";
                PDList.Add(pdInfo);

                PhysicalDiagramInfo pd = new PhysicalDiagramInfo();
                string pid1 = System.Guid.NewGuid().ToString();
                pd.Id = pid1;
                pd.Name = "数据表";
                pd.PhyParentId = pid;

                 //杜冬军2014-10-11优化，标签页没有数据表则不显示
                if (mTables != null && mTables.Count > 0)
                {
                    PDList.Add(pd);
                    foreach (TableInfo t in mTables)
                    {
                        pd = new PhysicalDiagramInfo();
                        pd.Id = System.Guid.NewGuid().ToString();
                        pd.Name = t.Code;
                        pd.IfEnd = true;
                        pd.PhyParentId = pid1;
                        PDList.Add(pd);

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
        /// <param name="table">table</param>
        private TableInfo GetTable(Table table)
        {
            try
            {
                TableInfo mTable = new TableInfo();
                List<ColumnInfo> list = new List<ColumnInfo>();
                List<PkKeyInfo> listPkKeyInfo = new List<PkKeyInfo>();
                mTable.ListColumnInfo = list;
                mTable.ListPkKeyInfo = listPkKeyInfo;
                //表的ID
                mTable.TableObjectID = Guid.NewGuid().ToString();
                //表的中文名称
                mTable.Name = Common.GetTableCName(table.Rows[0].Cells[1].Paragraphs[0].Text.Trim());
                //表的英文名称
                mTable.Code = table.Rows[1].Cells[1].Paragraphs[0].Text.Trim();
                //2014-07-28 添加错误详细信息提示,以便准确知道错误地方
                Common.JudgeTableInfo(mTable.Code, mTable.Name);
                //表的描述
                mTable.Comment = mTable.Name;

                //标题列
                var row = table.Rows[3];
                //缓存列索引和名称
                Dictionary<int, string> dic = new Dictionary<int, string>();
                int i = 0;
                foreach (var cell in row.Cells)
                {
                    dic.Add(i, cell.Paragraphs[0].Text.Trim());
                    i++;
                }

                for (i = 4; i < table.Rows.Count; i++)
                {
                    row = table.Rows[i];
                    InitColumns(row, dic, mTable);
                }

                if (string.IsNullOrEmpty(mTable.Comment))
                {
                    mTable.Comment = mTable.Name;
                }
                if (mTable.ListPkKeyInfo != null && mTable.ListPkKeyInfo.Count > 0)
                {
                    foreach (PkKeyInfo pkInfo in mTable.ListPkKeyInfo)
                    {
                        mTable.PkKeyNameList = String.Format("{0}{1},", mTable.PkKeyNameList, pkInfo.Name);
                    }
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
        /// <param name="row">行节点</param>
        /// <param name="dic">列名字典集合</param>
        /// <param name="pTable">表信息</param>
        private void InitColumns(Row row, Dictionary<int, string> dic, TableInfo pTable)
        {
            ColumnInfo mColumn = new ColumnInfo();
            int iCell = 0;
            //列ID
            mColumn.ColumnObjectId = Guid.NewGuid().ToString();
            string sTemp="";
            foreach (var cell in row.Cells)
            {
                sTemp=cell.Paragraphs[0].Text.Trim();
                Common.GetColumnInfo(dic, sTemp, mColumn, iCell, pTable);
                iCell++;
            }
            //2014-07-28 添加错误详细信息提示,以便准确知道错误地方
            Common.JudgeColumnInfo(mColumn, pTable.Code);
            mColumn.DataTypeStr=Common.GetDataTypeStr(mColumn.DataTypeStr, mColumn.Width);
            mColumn.Width = Common.GetColumnWidth(mColumn.DataTypeStr);

            //杜冬军2014-07-23修改,添加主键列的判定方式 ,如果中文名称类似 A(主键) 则认为该列是主键列
            Common.GetPrimaryKeyInfo(mColumn, pTable);

            if (string.IsNullOrEmpty(mColumn.Comment))
            {
                mColumn.Comment = mColumn.Name;
            }
            if (string.IsNullOrEmpty(mColumn.DefaultValue))
            {
                mColumn.DefaultValue = "";
            }
            mColumn.Sequence = pTable.ListColumnInfo.Count + 1;
            pTable.ListColumnInfo.Add(mColumn);
        }
    }
}
