using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyTools.Update
{
    public partial class DownloadConfirm : Form
    {
        List<DownloadFileInfo> downloadFileList = null;

        public DownloadConfirm(List<DownloadFileInfo> dfl,bool isShowNoWaring)
        {
            InitializeComponent();

            downloadFileList = dfl;
            if (!isShowNoWaring)
            {
                btnNoWaring.Visible = false;
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            foreach (DownloadFileInfo file in this.downloadFileList)
            {
                ListViewItem item = new ListViewItem(new string[] { file.FileName, file.LastVer, file.Size.ToString() });
                this.listDownloadFile.Items.Add(item);
            }

            this.Activate();
            this.Focus();
        }
    }
}