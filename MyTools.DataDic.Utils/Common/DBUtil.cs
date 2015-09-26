using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.OleDb;
using System.Collections;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Xml;

namespace MyTools.DataDic.Utils
{
    public class DBUtil
    {
        public DBUtil(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private  String connectionString = "";

        private  DbConnection conn;
        public  DbConnection getConn()
        {
            if (conn == null)
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
            }
            return conn;
        }
        public  void BeginConn()
        {
            getConn();
        }
        public  void EndConn()
        {
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
        }

        public  ArrayList Select(string sql)
        {
            return Select(sql, null);
        }

        public  ArrayList Select(string sql, Hashtable args)
        {
            DataTable data = new DataTable();

            bool isConn = conn != null;

            DbConnection con = getConn();
            SqlCommand cmd = new SqlCommand(sql, (SqlConnection)con);
            if (args != null) SetArgs(sql, args, cmd);

            SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
            adapter.Fill(data);

            if (isConn == false)
            {
                EndConn();
            }

            return DataTable2ArrayList(data);
        }

        public  DataTable GetDataTable(string sql)
        {
            return GetDataTable(sql, null);
        }

        public  DataTable GetDataTable(string sql, Hashtable args)
        {
            DataTable data = new DataTable();

            bool isConn = conn != null;

            DbConnection con = getConn();


            SqlCommand cmd = new SqlCommand(sql, (SqlConnection)con);
            if (args != null) SetArgs(sql, args, cmd);

            SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
            adapter.Fill(data);

            if (isConn == false)
            {
                EndConn();
            }

            return data;
        }

        public  void Execute(string sql)
        {
            Execute(sql, null);
        }

        public  void Execute(string sql, Hashtable args)
        {
            bool isConn = conn != null;
            DbConnection con = getConn();

            SqlCommand cmd = new SqlCommand(sql, (SqlConnection)con);
            if (args != null) SetArgs(sql, args, cmd);
            cmd.ExecuteNonQuery();

            if (isConn == false)
            {
                EndConn();
            }
        }

        public  object ExecuteScalar(string sql, Hashtable args)
        {
            bool isConn = conn != null;
            DbConnection con = getConn();
            object obj = null;
            SqlCommand cmd = new SqlCommand(sql, (SqlConnection)con);
            if (args != null) SetArgs(sql, args, cmd);
            obj = cmd.ExecuteScalar();

            if (isConn == false)
            {
                EndConn();
            }
            return obj;
        }

        public  object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, null);
        }

        public  int GetItemInt(string sql)
        {
            return Convert.ToInt32(ExecuteScalar(sql, null));
        }

        public  int GetItemInt(string sql, Hashtable args)
        {
            return Convert.ToInt32(ExecuteScalar(sql, args));
        }

        public  string GetItemString(string sql)
        {
            return Convert.ToString(ExecuteScalar(sql, null));
        }

        public  string GetItemString(string sql, Hashtable args)
        {
            return Convert.ToString(ExecuteScalar(sql, args));
        }

        /// <summary>
        /// 获取数据库服务器的系统日期
        /// </summary>
        /// <returns></returns>
        public  string GetCurrentDate(string strDateFormat)
        {
            string strReturn = String.Empty;

            switch (strDateFormat.ToLower())
            {
                case "yyyy-mm-dd":
                    strReturn = GetItemString("SELECT Convert(Varchar(10), GetDate(), 120)");
                    break;

                case "yyyy-mm":
                    strReturn = GetItemString("SELECT Convert(Varchar(7), GetDate(), 120)");
                    break;

                case "yyyy":
                    strReturn = GetItemString("SELECT Convert(Varchar(4), GetDate(), 120)");
                    break;
                case "mm":
                    strReturn = GetItemString("SELECT Convert(Varchar(2), GetDate(), 120)");
                    break;
                case "yyyy-mm-dd hh:mm:ss":
                    strReturn = GetItemString("SELECT Convert(Varchar(20), GetDate(), 120)");
                    break;
            }
            return strReturn;
        }

        #region 私有
        private  void SetArgs(string sql, Hashtable args, IDbCommand cmd)
        {

            MatchCollection ms = Regex.Matches(sql, @"@\w+");
            foreach (Match m in ms)
            {
                string key = m.Value;

                Object value = args[key];
                if (value == null)
                {
                    value = args[key.Substring(1)];
                }
                if (value == null) value = DBNull.Value;

                cmd.Parameters.Add(new SqlParameter(key, value));
                cmd.CommandText = sql;
            }
        }

        private  ArrayList DataTable2ArrayList(DataTable data)
        {
            ArrayList array = new ArrayList();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow row = data.Rows[i];

                Hashtable record = new Hashtable();
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    object cellValue = row[j];
                    if (cellValue.GetType() == typeof(DBNull))
                    {
                        cellValue = null;
                    }
                    record[data.Columns[j].ColumnName] = cellValue;
                }
                array.Add(record);
            }
            return array;
        }
        #endregion
    }
}