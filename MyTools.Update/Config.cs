using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace MyTools.Update
{
    public class Config
    {
        private bool enabled = true;
        public bool Enabled { get { return enabled; } set { enabled = value; } }

        private string serverUrl = "";
        public string ServerUrl { get { return serverUrl; } set { serverUrl = value; } }

        private UpdateFileList updateFileList = new UpdateFileList();
        public UpdateFileList UpdateFileList
        {
            get { return updateFileList; }
            set { updateFileList = value; }
        }

        public static Config LoadConfig(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            StreamReader sr = new StreamReader(file);
            Config config = xs.Deserialize(sr) as Config;
            sr.Close();

            return config;
        }

        public void SaveConfig(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            StreamWriter sw = new StreamWriter(file);
            xs.Serialize(sw, this);
            sw.Close();
        }

        public void SaveEnabled(string file)
        {

            XmlDocument dom = new XmlDocument();
            dom.Load(file);
            XmlNode node=dom.SelectSingleNode("//Config/Enabled");
            if (node != null)
            {
                node.InnerText = this.Enabled.ToString().ToLower() ;
                dom.Save(file);
            }
        }
    }

    public class UpdateFileList : List<LocalFile>
    {
    }
}
