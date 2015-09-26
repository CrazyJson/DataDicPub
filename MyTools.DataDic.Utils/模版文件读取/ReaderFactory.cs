using System;
using System.IO;

namespace MyTools.DataDic.Utils
{
    /// <summary>
    /// 获取模版文件解析工厂
    /// </summary>
    public class ReaderFactory
    {
        /// <summary>
        /// 根据文件类型获取相对应的解析器
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>IReader</returns>
        public static IReader GetReader(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("文件路径不能为空！");
            }
            if (!File.Exists(filePath))
            {
                throw new Exception("指定文件不存在！");
            }
            //获取文件的拓展名
            string[] arr = filePath.Split(new char[] { '.' });
            string ext = arr[arr.Length - 1].ToLower();
            IReader reader = null;

            switch (ext)
            {
                    //pdm文件
                case "pdm":
                    reader= new PDMReader(filePath);
                    break;
                    //execl文件
                case "xlsx":
                case "xls":
                    reader = new ExeclReader(filePath);
                    break;
                //word文件
                case "doc":
                case "docx":
                    //2014-07-27修改，word读取替换成DocX组件
                    //reader = new WordReader(filePath);
                    reader = new WordReaderNew(filePath);
                    break;
                default:
                    throw new Exception("指定文件类型暂时还不支持！");
            }
            return reader;
        }
    }
}
