using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTools.DataDic.Utils
{
    /// <summary>
    /// 模版文件解析接口
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// 从文件中读取表信息
        /// </summary>
        /// <returns> List</returns>
        List<TableInfo> GetTableInfo();

        /// <summary>
        /// 从文件中读取物理图信息
        /// </summary>
        /// <returns> List</returns>
        List<PhysicalDiagramInfo> GetPDInfo();

     
        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>表信息</returns>
        List<TableInfo> GetTableColumnName(string tableName);
    }
}
