using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTools.DataDic.Utils
{
    /// <summary>
    /// 物理图信息
    /// </summary>
    public class PhysicalDiagramInfo
    {
    
        /// <summary>
        /// 父级物理图ID
        /// </summary>
        public string PhyParentId
        {
            get;
            set;
        }

        /// <summary>
        /// 物理图名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// ID
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 是否末级
        /// </summary>
        public bool IfEnd
        {
            get;
            set;
        }
    }
}
