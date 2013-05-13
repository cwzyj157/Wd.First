using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace OtherTestProject.Console
{
    
    partial class Program
    {
        static void HttpWebRequestTest0()
        {
            HttpWebRequestTest.Test();
            System.Console.ReadLine();
        }
    }

    public class HttpWebRequestTest
    {
        public static void Test()
        {
            string url = "http://blog.sina.com.cn/rss/ky1982.xml";
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Referer = url;
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                using (Stream stream = webResponse.GetResponseStream())
                {
                    // Encoding.GetEncoding("gb2312")
                    using (StreamReader reader = GetStreamReader(webResponse as HttpWebResponse, stream, Encoding.UTF8))
                    {
                        string responseInfo = reader.ReadToEnd();

                        System.Console.WriteLine(responseInfo);
                    }
                }
            }
        }
        private static StreamReader GetStreamReader(HttpWebResponse webResponse, Stream stream, Encoding charset)
        {
            StreamReader reader = null;
            if (webResponse.ContentEncoding != null && webResponse.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                reader = new StreamReader(new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress), charset);
            else
                reader = new StreamReader(stream, charset);
            return reader;
        }
    }
}
