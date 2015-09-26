using System;
using System.Collections.Generic;
using System.Text;

namespace MyTools.DataDic2Doc.Entity
{
    /// <summary>
    /// ERP系统信息
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 表名前缀
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 是否默认勾选
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
