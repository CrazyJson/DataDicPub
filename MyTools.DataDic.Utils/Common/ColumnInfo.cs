using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTools.DataDic.Utils
{
    /// <summary>
    /// 列信息
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 列ID
        /// </summary>
        public string ColumnId
        {
            get;
            set;
        }

        /// <summary>
        /// 列ObjectId
        /// </summary>
        public string ColumnObjectId
        {
            get;
            set;
        }

        /// <summary>
        /// 列英文名
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 列中文名
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 列描述
        /// </summary>
        public string Comment
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型[DECIMAL,int,datetime]
        /// </summary>
        public string DataType
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型[DECIMAL(20,2),varchar(10)]
        /// </summary>
        public string DataTypeStr
        {
            get;
            set;
        }

        /// <summary>
        /// 列宽
        /// </summary>
        public string Length
        {
            get;
            set;
        }

        /// <summary>
        /// 列宽
        /// </summary>
        public string Width
        {
            get;
            set;
        }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool Identity
        {
            get;
            set;
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool PK
        {
            get;
            set;
        }

        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool Nullable
        {
            get;
            set;
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// 列顺序
        /// </summary>
        public int Sequence
        {
            get;
            set;
        }

        /// <summary>
        /// 获取列的默认值字符串
        /// </summary>
        /// <returns>默认值</returns>
        public string GetDefaultValue()
        {
            return Common.ImportGetDefaultValue(this.DefaultValue, this.DataType);
        }
    }
}
