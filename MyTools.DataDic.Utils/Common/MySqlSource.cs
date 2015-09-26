using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MyTools.DataDic.Utils
{
    public  class MySqlSource
    {
        public XmlDocument XmlDoc { get; set; }

        public string GetSqlByID(string nodeId, params String[] args)
        {
            XmlNode xnSQL = XmlDoc.SelectSingleNode("/xml/sqlstrings/sql[@id='" + nodeId + "']");
            if (xnSQL != null)
            {
                String SQL = xnSQL.InnerText;
                return args.Length == 0 ? SQL : String.Format(SQL, args);
            }
            else 
            {
                throw new Exception("SQL节点不存在或xml配置错误");
            }
        }

        public MySqlSource(XmlDocument xmlDoc)
        {
            XmlDoc = xmlDoc;
        }

        public MySqlSource(Type type)
        {
            XmlDoc = new XmlDocument();
            String assembleName = type.Assembly.GetName().Name;
            using (System.IO.Stream stream = type.Assembly.GetManifestResourceStream(String.Format("{0}.{1}.xml", assembleName, type.Name)))
            {
                XmlDoc.Load(stream);
            }
        }
    }
}
