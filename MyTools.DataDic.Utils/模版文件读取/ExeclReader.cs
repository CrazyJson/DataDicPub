using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MyTools.DataDic.Utils
{
    /// <summary>
    /// 解析Execl文件,读取信息
    /// </summary>
    public class ExeclReader : IReader
    {
        /// <summary>
        /// Execl文件路径
        /// </summary>
        private string _pdmPath;

        private List<TableInfo> mTables = null;

        private IWorkbook hssfworkbook = null;

        /// <summary>
        /// 校验文件路径是否存在
        /// </summary>
        /// <param name="pdmPath">EXECL文件路径</param>
        private void CheckPath(string pdmPath)
        {
            if (string.IsNullOrEmpty(pdmPath))
            {
                throw new Exception("文件路径不能为空！");
            }
            if (!pdmPath.EndsWith(".xls", true, null) && !pdmPath.EndsWith(".xlsx", true, null))
            {
                throw new Exception("文件格式不正确，请选择EXECL文件！");
            }
            if (!File.Exists(pdmPath))
            {
                throw new Exception("指定文件不存在！");
            }
        }

        /// <summary>
        /// 构造函数 根据路径生成所有表的SQL
        /// </summary>
        /// <param name="pdmPath">EXECL文件路径</param>
        public ExeclReader(string pdmPath)
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
        /// 初始化EXECL
        /// </summary>
        /// <returns>HSSFWorkbook</returns>
        private IWorkbook InitHSSFWorkbook()
        {
            if (hssfworkbook == null)
            {
                try
                {
                    using (FileStream fs = new FileStream(_pdmPath, FileMode.Open, FileAccess.Read))
                    {
                        hssfworkbook = WorkbookFactory.Create(fs);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return hssfworkbook;
        }

        /// <summary>
        /// 从EXECL读取表信息
        /// </summary>
        /// <returns> List</returns>
        public List<TableInfo> GetTableInfo()
        {
            try
            {
                IWorkbook hssfworkbook = InitHSSFWorkbook();
                List<TableInfo> Tables = new List<TableInfo>();
                //杜冬军2014-05-03修改 循环读取EXECL所有的非隐藏Sheet
                //sheet总数
                int iSheetNum=hssfworkbook.NumberOfSheets;
                ISheet sheet = null;
                for (int m = 0; m < iSheetNum; m++)
                {
                    if (hssfworkbook.IsSheetHidden(m))
                    {
                        continue;
                    }
                    sheet = hssfworkbook.GetSheetAt(m);    
                    IRow row = null;
                    ICell cell = null;
                    int iLastRowNum = sheet.LastRowNum;
                    for (int i = 0; i < iLastRowNum; i++)
                    {
                        row = sheet.GetRow(i);
                        if (row == null)
                        {
                            continue;
                        }
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            //列索引异常BUG修复 2014-06-07杜冬军修改 row.Cells不适合
                            cell = row.GetCell(j);
                            if (cell != null && cell.ToString().Trim() == "数据表中文名称")
                            {
                                i = GetTable(sheet, i, cell.ColumnIndex, Tables, sheet.SheetName.Trim());
                                break;
                            }
                        }
                    }
                }
                mTables = Tables;
                return Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从中读取物理图信息
        /// </summary>
        /// <returns> List</returns>
        public List<PhysicalDiagramInfo> GetPDInfo()
        {
            try
            {
                List<PhysicalDiagramInfo> PDList = new List<PhysicalDiagramInfo>();
                IWorkbook hssfworkbook = InitHSSFWorkbook();
                if (mTables == null)
                {
                    mTables = GetTableInfo();
                }
                PhysicalDiagramInfo pdInfo = new PhysicalDiagramInfo();
                string pid = System.Guid.NewGuid().ToString();
                pdInfo.Id = pid;
                pdInfo.Name = "所有数据";
                PDList.Add(pdInfo);

                int iSheetNum=hssfworkbook.NumberOfSheets;
                for (int m = 0; m < iSheetNum; m++)
                {
                    if (hssfworkbook.IsSheetHidden(m))
                    {
                        continue;
                    }
                    PhysicalDiagramInfo pd = new PhysicalDiagramInfo();
                    string pid1 = System.Guid.NewGuid().ToString();
                    pd.Id = pid1;
                    pd.Name = hssfworkbook.GetSheetAt(m).SheetName.Trim();
                    pd.PhyParentId = pid;

                    List<TableInfo> query = (from a in mTables
                                                   where a.PDName == pd.Name
                                                   select a).ToList<TableInfo>();
                    //杜冬军2014-10-11优化，标签页没有数据表则不显示
                    if (query != null && query.Count > 0)
                    {
                        PDList.Add(pd);
                        foreach (TableInfo t in query)
                        {
                            pd = new PhysicalDiagramInfo();
                            pd.Id = System.Guid.NewGuid().ToString();
                            pd.Name = t.Code;
                            pd.IfEnd = true;
                            pd.PhyParentId = pid1;
                            PDList.Add(pd);

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
        /// <param name="sheet">sheet</param>
        /// <param name="iRow">iRow</param>
        /// <param name="iCell">iCell</param>
        /// <param name="sheetName">sheetName</param>
        /// <returns>表信息</returns>
        private int GetTable(ISheet sheet, int iRow, int iCell, List<TableInfo> Tables,string sheetName)
        {
            try
            {
                TableInfo mTable = new TableInfo();
                List<ColumnInfo> list = new List<ColumnInfo>();
                List<PkKeyInfo> listPkKeyInfo = new List<PkKeyInfo>();
                mTable.ListColumnInfo = list;
                mTable.ListPkKeyInfo = listPkKeyInfo;
                mTable.PDName = sheetName;
                //表的ID
                mTable.TableObjectID = Guid.NewGuid().ToString();
                //表的中文名称
                mTable.Name = Common.GetTableCName(sheet.GetRow(iRow).GetCell(iCell + 2).ToString().Trim());
                //表的英文名称
                mTable.Code = sheet.GetRow(iRow + 1).GetCell(iCell + 2).ToString().Trim();
                //2014-07-28 添加错误详细信息提示,以便准确知道错误地方
                Common.JudgeTableInfo(mTable.Code, mTable.Name);
                //表的描述
                mTable.Comment = mTable.Name;

                //标题列 2014-05-03杜冬军修改，动态读取列，确保列顺序
                var row = sheet.GetRow(iRow+3);
                //缓存列索引和名称
                Dictionary<int, string> dic = new Dictionary<int, string>();
                for (int i = iCell; i < row.LastCellNum; i++)
                {
                    //标题列 2015-01-13杜冬军修改BUG
                    if (row.GetCell(i) == null)
                    {
                        break;
                    }
                    dic.Add(i, row.GetCell(i).ToString().Trim());
                }

                iRow = iRow + 4;
                row = sheet.GetRow(iRow);
                while (row != null)
                {
                    if (row.GetCell(iCell) != null && !String.IsNullOrEmpty(row.GetCell(iCell).ToString()))
                    {
                        InitColumns(row, iCell, mTable,dic);
                        iRow = iRow + 1;
                        row = sheet.GetRow(iRow);
                    }
                    else
                    {
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
                        mTable.PkKeyNameList = String.Format("{0}{1},", mTable.PkKeyNameList, pkInfo.Name);
                    }
                }
                if (!string.IsNullOrEmpty(mTable.PkKeyNameList))
                {
                    mTable.PkKeyNameList = mTable.PkKeyNameList.Substring(0, mTable.PkKeyNameList.Length - 1);
                }
                Tables.Add(mTable);
                return iRow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取表中的列信息
        /// </summary>
        /// <param name="row">列节点</param>
        /// <param name="iCell">列起始索引</param>
        /// <param name="pTable">表信息</param>
        /// <param name="dic">列名字典集合</param>
        private void InitColumns(IRow row, int iCell, TableInfo pTable, Dictionary<int, string> dic)
        {
            ColumnInfo mColumn = new ColumnInfo();
            //列ID
            mColumn.ColumnObjectId = Guid.NewGuid().ToString();
            string sTemp = "";
            int LastCellIndex = dic.Keys.Last<int>();
            for (int i = dic.Keys.First<int>(); i <= LastCellIndex; i++)
            {
                //2014-07-01杜冬军修改，row.LastCellNum取出来有误，确保不出现空异常
                sTemp = row.GetCell(i) == null ? "" : row.GetCell(i).ToString().Trim();
                Common.GetColumnInfo(dic, sTemp, mColumn, i, pTable);
            }
            //2014-07-28 添加错误详细信息提示,以便准确知道错误地方
            Common.JudgeColumnInfo(mColumn, pTable.Code);
            mColumn.DataTypeStr = Common.GetDataTypeStr(mColumn.DataTypeStr, mColumn.Width);
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
