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
    public partial class SpecialInfoForm : Form
    {
        public SpecialInfoForm()
        {
            InitializeComponent();
        }

        private void SpecialInfoForm_Load(object sender, EventArgs e)
        {
            textBox1.AppendText(" ");
            textBox1.ScrollToCaret(); 
        }
    }
}
