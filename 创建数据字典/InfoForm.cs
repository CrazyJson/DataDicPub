using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MyTools.DataDic
{
    public partial class InfoForm : Form
    {
        private string strFilePath = AppDomain.CurrentDomain.BaseDirectory + @"说明.txt";

        public InfoForm()
        {
            InitializeComponent();
        }

        private void InfoForm_Load(object sender, EventArgs e)
        {

            if (File.Exists(strFilePath))
            {
                using (StreamReader sr = new StreamReader(strFilePath, Encoding.UTF8))
                {
                    txtInfo.AppendText(sr.ReadToEnd());
                    txtInfo.ScrollToCaret(); 
                }
            }

        }
    }
}
