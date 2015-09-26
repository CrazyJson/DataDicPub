using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTools.DataDic.Utils
{
    /// <summary>
    /// 表结构信息
    /// </summary>
    public class TableInfo 
    {
        /// <summary>
        /// 表ID
        /// </summary>
        public string TableID
        {
            get;
            set;
        }


        /// <summary>
        /// 表ID
        /// </summary>
        public string TableObjectID
        {
            get;
            set;
        }

        /// <summary>
        /// 表所属物理图名称
        /// </summary>
        public string PDName
        {
            get;
            set;
        }

        /// <summary>
        /// 表英文名
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 表中文名
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 表描述
        /// </summary>
        public string Comment
        {
            get;
            set;
        }

        /// <summary>
        /// 是否修改表
        /// </summary>
        public bool IsUpdate
        {
            get;
            set;
        }

        /// <summary>
        /// 主键列名集合,分隔
        /// </summary>
        public string PkKeyNameList
        {
            get;
            set;
        }

        /// <summary>
        /// 表的列信息
        /// </summary>
        public List<ColumnInfo> ListColumnInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 修改表时新增的列信息
        /// </summary>
        public List<ColumnInfo> ListAddColumnInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 修改表时字段类型或者长度改变的列信息
        /// </summary>
        public List<ColumnInfo> ListUpdateColumnInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 表的主键列信息
        /// </summary>
        public List<PkKeyInfo> ListPkKeyInfo
        {
            get;
            set;
        }
    }
}
