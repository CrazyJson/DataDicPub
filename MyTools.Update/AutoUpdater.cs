using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyTools.Update
{
    public class AutoUpdater
    {
        const string FILENAME = "app.dic";
        private Config config = null;
        private bool bNeedRestart = false;

        public AutoUpdater()
        {
            config = Config.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME));
        }
        /// <summary>
        /// ����°汾
        /// </summary>
        /// <param name="isApplicationStart">�Ƿ��������ʱ����</param>
        /// <exception cref="System.Net.WebException">�޷��ҵ�ָ����Դ</exception>
        /// <exception cref="System.NotSupportException">������ַ���ô���</exception>
        /// <exception cref="System.Xml.XmlException">���ص������ļ��д���</exception>
        /// <exception cref="System.ArgumentException">���ص������ļ��д���</exception>
        /// <exception cref="System.Excpetion">δ֪����</exception>
        /// <returns></returns>
        public void Update(bool isApplicationStart)
        {
            //��������ʱ�����������Ƿ���Ҫ��ʾ����
            if (isApplicationStart)
            {
                if (!config.Enabled)
                {
                    return;
                }
            }
            Dictionary<string, RemoteFile> listRemotFile = ParseRemoteXml(config.ServerUrl);

            List<DownloadFileInfo> downloadList = new List<DownloadFileInfo>();

            //ĳЩ�ļ�������Ҫ�ˣ�ɾ��
            List<LocalFile> preDeleteFile = new List<LocalFile>();

            foreach (LocalFile file in config.UpdateFileList)
            {
                if (listRemotFile.ContainsKey(file.Path))
                {
                    RemoteFile rf = listRemotFile[file.Path];
                    if (rf.LastVer != file.LastVer)
                    {
                        downloadList.Add(new DownloadFileInfo(rf.Url, file.Path, rf.LastVer, rf.Size));
                        file.LastVer = rf.LastVer;
                        file.Size = rf.Size;

                        if (rf.NeedRestart)
                            bNeedRestart = true;
                    }

                    listRemotFile.Remove(file.Path);
                }
                else
                {
                    preDeleteFile.Add(file);
                }
            }

            foreach (RemoteFile file in listRemotFile.Values)
            {
                downloadList.Add(new DownloadFileInfo(file.Url, file.Path, file.LastVer, file.Size));
                config.UpdateFileList.Add(new LocalFile(file.Path, file.LastVer, file.Size));

                if (file.NeedRestart)
                    bNeedRestart = true;
            }

            if (downloadList.Count > 0)
            {
                DownloadConfirm dc = new DownloadConfirm(downloadList, isApplicationStart);

                if (this.OnShow != null)
                {
                    this.OnShow();
                }
                DialogResult result = dc.ShowDialog();

                if (DialogResult.OK == result)
                {
                    foreach (LocalFile file in preDeleteFile)
                    {
                        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.Path);
                        if (File.Exists(filePath))
                            File.Delete(filePath);

                        config.UpdateFileList.Remove(file);
                    }

                    StartDownload(downloadList);
                }
                //��������
                else if (DialogResult.Ignore == result)
                {
                    config.Enabled = false;
                    config.SaveEnabled(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME));
                }
            }
            else
            {
                if (!isApplicationStart)
                {
                    MessageBox.Show("��ϲ,���������ֵ����ɹ����Ѿ������°汾��", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void StartDownload(List<DownloadFileInfo> downloadList)
        {
            DownloadProgress dp = new DownloadProgress(downloadList);
            if (dp.ShowDialog() == DialogResult.OK)
            {
                //���³ɹ�
                config.SaveConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME));

                if (bNeedRestart)
                {
                    MessageBox.Show("������Ҫ������������Ӧ�ø��£�����ȷ��������������", "�Զ�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(Application.ExecutablePath);
                    Environment.Exit(0);
                }
            }
        }

        private Dictionary<string,RemoteFile> ParseRemoteXml(string ServerUrl)
        {
            XmlDocument document = new XmlDocument();
            document.Load(string.Format("{0}?ver={1}",ServerUrl,DateTime.Now.Millisecond));

            Dictionary<string, RemoteFile> list = new Dictionary<string, RemoteFile>();
            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                list.Add(node.Attributes["path"].Value, new RemoteFile(node));
            }

            return list;
        }
        public event ShowHandler OnShow;
    }

    public class RemoteFile
    {
        private string path = "";
        private string url = "";
        private string lastver = "";
        private int size = 0;
        private bool needRestart = false;

        public string Path { get { return path; } }
        public string Url { get { return url; } }
        public string LastVer { get { return lastver; } }
        public int Size { get { return size; } }
        public bool NeedRestart { get { return needRestart; } }

        public RemoteFile(XmlNode node)
        {
            this.path = node.Attributes["path"].Value;
            this.url = node.Attributes["url"].Value;
            this.lastver = node.Attributes["lastver"].Value;
            this.size = Convert.ToInt32(node.Attributes["size"].Value);
            this.needRestart = Convert.ToBoolean(node.Attributes["needRestart"].Value);
        }
    }

    public class LocalFile
    {
        private string path = "";
        private string lastver = "";
        private int size = 0;

        [XmlAttribute("path")]
        public string Path { get { return path; } set { path = value; } }
        [XmlAttribute("lastver")]
        public string LastVer { get { return lastver; } set { lastver = value; } }
        [XmlAttribute("size")]
        public int Size { get { return size; } set { size = value; } }

        public LocalFile(string path, string ver, int size)
        {
            this.path = path;
            this.lastver = ver;
            this.size = size;
        }

        public LocalFile()
        {
        }

    }


    public delegate void ShowHandler();

    public class DownloadFileInfo
    {
        string downloadUrl = "";
        string fileName = "";
        string lastver = "";
        int size = 0;

        /// <summary>
        /// Ҫ�����������ļ�
        /// </summary>
        public string DownloadUrl { get { return downloadUrl; } }
        /// <summary>
        /// ������ɺ�Ҫ�ŵ�����ȥ
        /// </summary>
        public string FileFullName { get { return fileName; } }
        public string FileName { get { return Path.GetFileName(FileFullName); } }
        public string LastVer { get { return lastver; } set { lastver = value; } }
        public int Size { get { return size; } }

        public DownloadFileInfo(string url, string name, string ver, int size)
        {
            this.downloadUrl = url;
            this.fileName = name;
            this.lastver = ver;
            this.size = size;
        }
    }
}
