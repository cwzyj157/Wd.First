using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Wd.Common
{
    public class WebResponseEncoding
    {
        /// <summary>
        /// 探测Web请求内容编码格式,不保证百分百探测成功，不成功需自行处理
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public string GetEncoding(string url)
        {
            return CharsetDetection.Get(url);
        }
    }
    /// <summary>
    /// Description of MyCharsetDetectionObserver.
    /// </summary>
    internal class CharsetDetectionObserver : NChardet.ICharsetDetectionObserver
    {
        public string Charset = null;

        public void Notify(string charset)
        {
            Charset = charset;
        }
    }
    internal class CharsetDetection
    {
        public static string Get(string url)
        {
            string charset = string.Empty;
            //输入字符流
            WebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    charset = Get(stream);
                }
            }
            return charset;
        }
        private static string Get(Stream stream)
        {
            int lang = 2;
            NChardet.Detector det = new NChardet.Detector(lang);
            //初始化
            CharsetDetectionObserver cdo = new CharsetDetectionObserver();
            det.Init(cdo);

            string charset = string.Empty;
            int len;
            byte[] buf = new byte[1024];
            bool done = false;
            bool isAscii = true;
            while ((len = stream.Read(buf, 0, buf.Length)) != 0)
            {
                // 探测是否为Ascii编码
                if (isAscii) isAscii = det.isAscii(buf, len);

                // 如果不是Ascii编码，并且编码未确定，则继续探测
                if (!isAscii && !done)
                    done = det.DoIt(buf, len, false);

                if (!isAscii && done) break;
            }
            //调用DatEnd方法，
            //如果引擎认为已经探测出了正确的编码，
            //则会在此时调用ICharsetDetectionObserver的Notify方法
            det.DataEnd();
            if (isAscii)
                charset = System.Text.Encoding.ASCII.BodyName;
            else if (!string.IsNullOrEmpty(cdo.Charset))
                charset = cdo.Charset;
            return charset;
        }
    }
}
