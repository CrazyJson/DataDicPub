using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTools.DataDic.Utils
{
    /// <summary>
    /// 主键列信息
    /// </summary>
    public class PkKeyInfo
    {
        public PkKeyInfo()
        {
        }

        public PkKeyInfo(string ColumnId)
        {
            this.ColumnId = ColumnId;
        }

        public PkKeyInfo(string ColumnId, string Name)
        {
            this.ColumnId = ColumnId;
            this.Name = Name;
        }

        /// <summary>
        /// 主键列ID
        /// </summary>
        public string ColumnId
        {
            get;
            set;
        }

        /// <summary>
        /// 主键列名
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}
