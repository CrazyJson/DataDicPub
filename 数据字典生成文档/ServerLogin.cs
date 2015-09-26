using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MyTools.DataDic.Utils;

namespace MyTools.DataDic2Doc
{
    public partial class ServerLogin : Form
    {
        /// <summary>
        /// 软件版本
        /// </summary>
        private static Properties.Settings setting = Properties.Settings.Default;


        public ServerLogin()
        {
            InitializeComponent();
        }

        private void ServerLogin_Load(object sender, EventArgs e)
        {
            txtServerIP.Text = setting.ServerIP;
            txtDBName.Text = setting.ServerDBName;
            txtLoginName.Text = setting.LoginName;
            txtLoginPassword.Text = setting.LoginPassword;
        }

        /// <summary>
        /// 登录
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtServerIP.Text.Trim()))
            {
                MessageBox.Show("请请输入服务器地址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrEmpty(txtDBName.Text.Trim()))
            {
                MessageBox.Show("请输入数据库！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrEmpty(txtLoginName.Text.Trim()))
            {
                MessageBox.Show("请输入登录名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string connectionString = "Data Source=" + txtServerIP.Text.Trim() + ";Initial Catalog=" + txtDBName.Text.Trim() + ";User ID=" + txtLoginName.Text.Trim() + ";Password=" + txtLoginPassword.Text.Trim() + ";";
                if (Common.IsCorrectConnection(connectionString))
                {
                    setting.ConnectionString = connectionString;
                    setting.ServerIP = txtServerIP.Text.Trim();
                    setting.ServerDBName = txtDBName.Text.Trim();
                    setting.LoginName = txtLoginName.Text.Trim();
                    setting.LoginPassword = txtLoginPassword.Text.Trim();
                    setting.Save();
                    setting.Upgrade();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
