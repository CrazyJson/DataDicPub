using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyTools.DataDic.Utils;
using System.Data;

namespace MyTools.DataDic.Utils
{
    public class DataDicService
    {
        private static MySqlSource SqlSource = new MySqlSource(typeof(DataDicService));

        /// <summary>
        /// 读取表信息
        /// </summary>
        /// <param name="strTableList">过滤的表名</param>
        /// <returns>表信息</returns>
        public static DataTable GetTableInfo(string strTableList, string strcon)
        {
            string strSQL = SqlSource.GetSqlByID("GetTableInfo", strTableList.Trim().Replace(",", "','"));
            DBUtil db = new DBUtil(strcon);
            DataTable dt = db.GetDataTable(strSQL);
            return dt;
        }

        /// <summary>
        /// 读取表信息
        /// </summary>
        /// <param name="strFilter">过滤条件</param>
        /// <returns>表信息</returns>
        public static DataTable GetDCTableInfo(string strFilter, string strcon)
        {
            string strSQL = SqlSource.GetSqlByID("GetDCTableInfo", strFilter);
            DBUtil db = new DBUtil(strcon);
            DataTable dt = db.GetDataTable(strSQL);
            return dt;
        }

        /// <summary>
        /// 读取表详细信息
        /// </summary>
        /// <param name="strFilter">过滤条件</param>
        /// <returns>表信息</returns>
        public static DataTable GetDCTableDetailInfo(string strTableList, string strcon)
        {
            string strSQL = SqlSource.GetSqlByID("GetDCTableDetailInfo", strTableList.Trim());
            DBUtil db = new DBUtil(strcon);
            DataTable dt = db.GetDataTable(strSQL);
            return dt;
        }
    }
}
