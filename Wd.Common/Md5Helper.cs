using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wd.Common
{
    public class Md5Helper
    {
        /// <summary>
        /// 通过文件全路径取得文件MD5值
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>MD5字符串</returns>
        public static string GetMd5(string fileFullPath)
        {
            System.IO.FileStream fs = null;
            try
            {

                fs = new System.IO.FileStream(fileFullPath, System.IO.FileMode.Open,
                    System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetMd5(fs);
        }

        /// <summary>
        /// 通过已打开文件流取得文件MD5值，
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <param name="closeStream">取得MD5是否立即关闭文件流,如果没有关闭建议调用方法后进行关闭</param>
        /// <returns>MD5字符串</returns>
        public static string GetMd5(Stream stream, bool closeStream)
        {
            if (stream == null) return "";

            //MD5码
            string md5 = string.Empty;
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
                //计算指定Stream 对象的哈希值
                byte[] arrbytHashValue = oMD5Hasher.ComputeHash(stream);

                if (closeStream)
                    stream.Close();

                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
                md5 = System.BitConverter.ToString(arrbytHashValue);
                md5 = md5.Replace("-", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return md5;
        }
        /// <summary>
        /// 通过已打开文件流取得文件MD5值，
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>MD5字符串</returns>
        public static string GetMd5(Stream stream)
        {
            return GetMd5(stream, true);
        }
    }
}
