using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Xml;
using NChardet;
using System.Globalization;
using System.Collections.ObjectModel;

using Wd.Extensions;
using Wd.Common;


namespace OtherTestProject.Console
{

    class test11111
    {

        public static DateTime ParseDateTime(string dateTime)
        {
            try
            {
                return DateTime.Parse(dateTime,
                System.Globalization.DateTimeFormatInfo.InvariantInfo,
                System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            }
            catch (FormatException fex)
            {
                string loc = fex.Message.Substring(fex.Message.LastIndexOf(" "));
                loc = loc.Substring(0, loc.LastIndexOf("."));
                int iLoc = int.Parse(loc);
                string tz = dateTime.Substring(iLoc);
                tz = TimeZoneToOffset(tz);
                dateTime = dateTime.Substring(0, iLoc);
                DateTime ret = DateTime.Parse(dateTime,
                System.Globalization.DateTimeFormatInfo.InvariantInfo,
                System.Globalization.DateTimeStyles.AllowWhiteSpaces);

                // offset for time zone
                if (tz.Length > 0)
                {
                    try
                    {
                        if (tz.Length == 4 && tz.Substring(0, 1) != "-")
                        {
                            try
                            {
                                int.Parse(tz.Substring(0, 1));
                                tz = "+" + tz;
                            }
                            catch
                            {
                            }
                        }
                        if (tz.Length == 5 && tz.Substring(0, 1) == "-" ||
                        tz.Length == 5 && tz.Substring(0, 1) == "+")
                        {
                            try
                            {
                                int h = int.Parse(tz.Substring(1, 2));
                                int m = int.Parse(tz.Substring(3, 2));
                                if (tz.Substring(0, 1) == "-")
                                {
                                    ret = ret.AddHours((h + SysDTOffset.Hours) * -1);
                                    ret = ret.AddMinutes((m + SysDTOffset.Minutes) * -1);
                                }
                                else
                                {
                                    ret = ret.AddHours(h + SysDTOffset.Hours);
                                    ret = ret.AddMinutes(m + SysDTOffset.Minutes);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch { }
                }

                return ret;
            }
        }

        private static TimeSpan SysDTOffset
        {
            get
            {
                return System.TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
            }
        }

        public static string TimeZoneToOffset(string tz)
        {
            tz = tz.ToUpper().Trim();
            //tz = "blaaaah";
            for (int i = 0; i < TimeZones.Length; i++)
            {
                if (((string)((string[])TimeZones.GetValue(i)).GetValue(0)) == tz)
                {
                    return ((string)((string[])TimeZones.GetValue(i)).GetValue(1));
                }
            }
            return System.TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).ToString()
            .Replace(":", "").Substring(0, 5);
        }

        public static string[][] TimeZones = new string[][] {
new string[] {"ACDT", "-1030", "Australian Central Daylight"},
new string[] {"ACST", "-0930", "Australian Central Standard"},
new string[] {"ADT", "+0300", "(US) Atlantic Daylight"},
new string[] {"AEDT", "-1100", "Australian East Daylight"},
new string[] {"AEST", "-1000", "Australian East Standard"},
new string[] {"AHDT", "+0900", ""},
new string[] {"AHST", "+1000", ""},
new string[] {"AST", "+0400", "(US) Atlantic Standard"},
new string[] {"AT", "+0200", "Azores"},
new string[] {"AWDT", "-0900", "Australian West Daylight"},
new string[] {"AWST", "-0800", "Australian West Standard"},
new string[] {"BAT", "-0300", "Bhagdad"},
new string[] {"BDST", "-0200", "British Double Summer"},
new string[] {"BET", "+1100", "Bering Standard"},
new string[] {"BST", "+0300", "Brazil Standard"},
new string[] {"BT", "-0300", "Baghdad"},
new string[] {"BZT2", "+0300", "Brazil Zone 2"},
new string[] {"CADT", "-1030", "Central Australian Daylight"},
new string[] {"CAST", "-0930", "Central Australian Standard"},
new string[] {"CAT", "+1000", "Central Alaska"},
new string[] {"CCT", "-0800", "China Coast"},
new string[] {"CDT", "+0500", "(US) Central Daylight"},
new string[] {"CED", "-0200", "Central European Daylight"},
new string[] {"CET", "-0100", "Central European"},
new string[] {"CST", "+0600", "(US) Central Standard"},
new string[] {"EAST", "-1000", "Eastern Australian Standard"},
new string[] {"EDT", "+0400", "(US) Eastern Daylight"},
new string[] {"EED", "-0300", "Eastern European Daylight"},
new string[] {"EET", "-0200", "Eastern Europe"},
new string[] {"EEST", "-0300", "Eastern Europe Summer"},
new string[] {"EST", "+0500", "(US) Eastern Standard"},
new string[] {"FST", "-0200", "French Summer"},
new string[] {"FWT", "-0100", "French Winter"},
new string[] {"GMT", "+0000", "Greenwich Mean"},
new string[] {"GST", "-1000", "Guam Standard"},
new string[] {"HDT", "+0900", "Hawaii Daylight"},
new string[] {"HST", "+1000", "Hawaii Standard"},
new string[] {"IDLE", "-1200", "Internation Date Line East"},
new string[] {"IDLW", "+1200", "Internation Date Line West"},
new string[] {"IST", "-0530", "Indian Standard"},
new string[] {"IT", "-0330", "Iran"},
new string[] {"JST", "-0900", "Japan Standard"},
new string[] {"JT", "-0700", "Java"},
new string[] {"MDT", "+0600", "(US) Mountain Daylight"},
new string[] {"MED", "-0200", "Middle European Daylight"},
new string[] {"MET", "-0100", "Middle European"},
new string[] {"MEST", "-0200", "Middle European Summer"},
new string[] {"MEWT", "-0100", "Middle European Winter"},
new string[] {"MST", "+0700", "(US) Mountain Standard"},
new string[] {"MT", "-0800", "Moluccas"},
new string[] {"NDT", "+0230", "Newfoundland Daylight"},
new string[] {"NFT", "+0330", "Newfoundland"},
new string[] {"NT", "+1100", "Nome"},
new string[] {"NST", "-0630", "North Sumatra"},
new string[] {"NZ", "-1100", "New Zealand "},
new string[] {"NZST", "-1200", "New Zealand Standard"},
new string[] {"NZDT", "-1300", "New Zealand Daylight"},
new string[] {"NZT", "-1200", "New Zealand"},
new string[] {"PDT", "+0700", "(US) Pacific Daylight"},
new string[] {"PST", "+0800", "(US) Pacific Standard"},
new string[] {"ROK", "-0900", "Republic of Korea"},
new string[] {"SAD", "-1000", "South Australia Daylight"},
new string[] {"SAST", "-0900", "South Australia Standard"},
new string[] {"SAT", "-0900", "South Australia Standard"},
new string[] {"SDT", "-1000", "South Australia Daylight"},
new string[] {"SST", "-0200", "Swedish Summer"},
new string[] {"SWT", "-0100", "Swedish Winter"},
new string[] {"USZ3", "-0400", "USSR Zone 3"},
new string[] {"USZ4", "-0500", "USSR Zone 4"},
new string[] {"USZ5", "-0600", "USSR Zone 5"},
new string[] {"USZ6", "-0700", "USSR Zone 6"},
new string[] {"UT", "+0000", "Universal Coordinated"},
new string[] {"UTC", "+0000", "Universal Coordinated"},
new string[] {"UZ10", "-1100", "USSR Zone 10"},
new string[] {"WAT", "+0100", "West Africa"},
new string[] {"WET", "+0000", "West European"},
new string[] {"WST", "-0800", "West Australian Standard"},
new string[] {"YDT", "+0800", "Yukon Daylight"},
new string[] {"YST", "+0900", "Yukon Standard"},
new string[] {"ZP4", "-0400", "USSR Zone 3"},
new string[] {"ZP5", "-0500", "USSR Zone 4"},
new string[] {"ZP6", "-0600", "USSR Zone 5"}
};
    }



    partial class Program
    {
        static void WebRequestEncoding()
        {
        //    DateTime dt;


        //    System.Console.WriteLine(test11111.ParseDateTime("2013-01-21 09:16 AST"));

            MyCharsetDetectionObserverTest.Test("http://blog.sina.com.cn/rss/ky1982.xml");
            //TestDecode.TestDecodeISO88591("http://2010.163.com/special/00863I1M/rss_2010.xml");
            //WebCrawl.getHtml("http://2010.163.com/special/00863I1M/rss_2010.xml");
        }
    }

    public class MyCharsetDetectionObserver :
       NChardet.ICharsetDetectionObserver
    {
        public string Charset = null;

        public void Notify(string charset)
        {
            Charset = charset;
        }
    }
    public class MyCharsetDetectionObserverTest
    {
        public static void Test(string url)
        {
            bool isFound = false;
            int lang = 2;//
            //用指定的语参数实例化Detector
            Detector det = new Detector(lang);
            //初始化
            MyCharsetDetectionObserver cdo = new MyCharsetDetectionObserver();
            det.Init(cdo);

            //输入字符流
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Referer = "";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            byte[] buf = new byte[1024];
            int len;
            bool done = false;
            bool isAscii = true;

            while ((len = stream.Read(buf, 0, buf.Length)) != 0)
            {
                // 探测是否为Ascii编码
                if (isAscii)
                    isAscii = det.isAscii(buf, len);

                // 如果不是Ascii编码，并且编码未确定，则继续探测
                if (!isAscii && !done)
                    done = det.DoIt(buf, len, false);

                //if (!isAscii && done) break;
            }
            stream.Position = 0;
            if (!string.IsNullOrEmpty(cdo.Charset))
            {
                string channelInfo = GetResponseInfo(response, GetStreamReader(response, stream, Encoding.GetEncoding(cdo.Charset)));
            }

            stream.Close();
            stream.Dispose();
            //调用DatEnd方法，
            //如果引擎认为已经探测出了正确的编码，
            //则会在此时调用ICharsetDetectionObserver的Notify方法
            det.DataEnd();

            if (isAscii)
            {
                System.Console.WriteLine("CHARSET = ASCII");
                isFound = true;
            }
            else if (cdo.Charset != null)
            {
                System.Console.WriteLine("CHARSET = {0}", cdo.Charset);
                isFound = true;
            }

            if (!isFound)
            {
                string[] prob = det.getProbableCharsets();
                for (int i = 0; i < prob.Length; i++)
                {
                    System.Console.WriteLine("Probable Charset = " + prob[i]);
                }
            }
            System.Console.ReadLine();
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
        private static string GetResponseInfo(WebResponse response, StreamReader reader)
        {
            string channelInfo = string.Empty;
            using (reader)
            {
                channelInfo = reader.ReadToEnd();
            }
            return channelInfo;
        }

    }



    public class TestDecode
    {
        public static void TestDecodeISO88591(string RssUrl)
        {

            string sResult = "";

            System.IO.Stream ResponseStream = null;
            HttpWebResponse hwrp = null;

            System.IO.StreamReader oStreamReader = null;

            Encoding UrlEncoding;
            System.Net.HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(RssUrl);
            hwr.Method = "GET";
            hwrp = (HttpWebResponse)hwr.GetResponse();
            UrlEncoding = Encoding.GetEncoding(hwrp.CharacterSet);
            ResponseStream = hwrp.GetResponseStream();
            oStreamReader = new System.IO.StreamReader(ResponseStream, Encoding.Default);
            sResult = oStreamReader.ReadToEnd();
            if (hwrp.CharacterSet == "ISO-8859-1") //如果编码为ISO-8859-1才转换
            {
                sResult = ConvertISO88591ToEncoding(sResult, UrlEncoding, Encoding.GetEncoding("gb2312"));
            }

            hwrp.Close();

            //处理RSS返回的数据

            //.......

        }

        //转换

        private static string ConvertISO88591ToEncoding(string srcString, Encoding srcEncode, Encoding dstEncode)
        {
            byte[] srcBytes = srcEncode.GetBytes(srcString);

            byte[] destBytes = Encoding.Convert(srcEncode, dstEncode, srcBytes);
            char[] destChars = new char[dstEncode.GetCharCount(destBytes, 0, destBytes.Length)];

            dstEncode.GetChars(destBytes, 0, destBytes.Length, destChars, 0);

            return new string(destChars);


            //String sResult;

            //Encoding ISO88591Encoding = Encoding.GetEncoding("ISO-8859-1");
            //Encoding GB2312Encoding = Encoding.GetEncoding("GB2312"); //这个地方很特殊，必须利用GB2312编码
            //byte[] srcBytes = srcEncode.GetBytes(srcString);

            ////将原本存储ISO-8859-1的字节数组当成GB2312转换成目标编码(关键步骤)
            //byte[] dstBytes = Encoding.Convert(srcEncode, dstEncode, srcBytes);

            //char[] dstChars = new char[dstEncode.GetCharCount(dstBytes, 0, dstBytes.Length)];

            //dstEncode.GetChars(dstBytes, 0, dstBytes.Length, dstChars, 0);//利用char数组存储字符
            //sResult = new string(dstChars);
            //return sResult;

        }


    }

    /// <summary>
    /// 网页抓取类
    /// </summary>
    public class WebCrawl
    {
        public WebCrawl() { }

        //获取网页字符根据url  
        public static string getHtml(string url)
        {
            try
            {
                string str = "";
                Encoding en = Encoding.GetEncoding(getEncoding(url));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Set("Pragma", "no-cache");
                request.Timeout = 30000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK && response.ContentLength < 1024 * 1024)
                {
                    //此处不要用StreamReader 直接读取，需要判断gzip情况
                    //否则可能出现乱码现象，如www.sohu.com
                    //Stream strM = response.GetResponseStream();
                    //StreamReader sr = new StreamReader(strM, en);
                    StreamReader sr;
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                        sr = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), en);
                    else
                        sr = new StreamReader(response.GetResponseStream(), en);
                    str = sr.ReadToEnd();

                    str = ConvertISO88591ToEncoding(str, Encoding.GetEncoding("gbk"));
                    //strM.Close();
                    sr.Close();
                }
                return str;
            }
            catch
            {
                return String.Empty;
            }
        }
        private static string ConvertISO88591ToEncoding(string srcString, Encoding dstEncode)
        {

            String sResult;

            Encoding ISO88591Encoding = Encoding.GetEncoding("ISO-8859-1");
            Encoding GB2312Encoding = Encoding.GetEncoding("GB2312"); //这个地方很特殊，必须利用GB2312编码
            byte[] srcBytes = ISO88591Encoding.GetBytes(srcString);

            //将原本存储ISO-8859-1的字节数组当成GB2312转换成目标编码(关键步骤)
            byte[] dstBytes = Encoding.Convert(GB2312Encoding, dstEncode, srcBytes);

            char[] dstChars = new char[dstEncode.GetCharCount(dstBytes, 0, dstBytes.Length)];

            dstEncode.GetChars(dstBytes, 0, dstBytes.Length, dstChars, 0);//利用char数组存储字符
            sResult = new string(dstChars);
            return sResult;

        }

        public static string GetEncoding(string url)
        {
            return "";
        }

        //获取编码
        public static string getEncoding(string url)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            string temp = string.Empty;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 30000;
                request.AllowAutoRedirect = false;
                string html = "";
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK && response.ContentLength < 1024 * 1024)
                {
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                        reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress));
                    else
                        reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);

                    //此处不用ReadToEnd 方法，采用ReadLine 当读到charset时跳出。
                    //string html = reader.ReadToEnd();
                    StringBuilder htmlBuilder = new StringBuilder();
                    while ((temp = reader.ReadLine()) != null)
                    {
                        htmlBuilder.Append(temp);
                        html = htmlBuilder.ToString();
                        if (html.IndexOf("charset", StringComparison.InvariantCultureIgnoreCase) > 0)
                        {
                            break;
                        }
                    }

                    Regex reg_charset = new Regex(@"charset\b\s*=\s*(?<charset>[^""]*)");
                    if (reg_charset.IsMatch(html))
                    {
                        return reg_charset.Match(html).Groups["charset"].Value;
                    }
                    else if (response.CharacterSet != string.Empty)
                    {
                        return response.CharacterSet;
                    }
                    else
                        return Encoding.Default.BodyName;
                }
                else if (response.StatusCode == HttpStatusCode.MovedPermanently || response.StatusCode == HttpStatusCode.Found)
                {
                    //页面跳转返回301，如：www.sina.com
                    //重新读取跳转地址
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                        reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress));
                    else
                        reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
                    html = reader.ReadToEnd();
                    Regex reg_href = new Regex("<a[\\s]+href[\\s]*=[\\s]*\"([^<\"]+)\"");
                    if (reg_href.IsMatch(html))
                    {
                        var targetUrl = reg_href.Match(html).Groups[1].Value;
                        if (!IsURL(targetUrl))
                        {
                            url = url + targetUrl;
                        }
                        else
                        {
                            url = targetUrl;
                        }
                        return getEncoding(url);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (reader != null)
                    reader.Close();

                if (request != null)
                    request = null;
            }
            return Encoding.Default.BodyName;
        }

        public static bool IsURL(string url)
        {
            try
            {
                Uri uri = new Uri(url);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //根据内容--获取标题
        public static string getTitle(string url)
        {
            if (url.Equals("about:blank")) return null; ;
            if (!url.StartsWith("http://") && !url.StartsWith("https://")) { url = "http://" + url; }

            string title = string.Empty;
            string htmlStr = getHtml(url);//获取网页
            Match TitleMatch = Regex.Match(htmlStr, "<title>([^<]*)</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            title = TitleMatch.Groups[1].Value;
            title = Regex.Replace(title, @"\W", "");//去除空格
            return title;

        }

        //根据内容--获取描述信息
        public static string getDescription(string url)
        {
            string htmlStr = getHtml(url);
            Match Desc = Regex.Match(htmlStr, "<meta name=\"Description\" content=\"([^<]*)\"*>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string mdd = Desc.Groups[1].Value;
            return Regex.Replace(Desc.Groups[1].Value, @"\W", "");
        }


        //根据内容--获取所有链接
        public static List<string> getLink(string htmlStr)
        {
            List<string> list = new List<string>(); //用来存放链接       
            String reg = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";  //链接的正则表达式      
            Regex regex = new Regex(reg, RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(htmlStr);
            for (int i = 0; i < mc.Count; i++) //存放匹配的集合
            {
                bool hasExist = false;   //链接存在与否的标记         
                String name = mc[i].ToString();
                foreach (String one in list)
                {
                    if (name == one)
                    {
                        hasExist = true; //链接已存在                   
                        break;
                    }
                }
                if (!hasExist) list.Add(name); //链接不存在，添加
            }
            return list;

        }

        //根据内容--取得body内的内容
        public static string getBody(string url)
        {
            string htmlStr = getHtml(url);
            string result = string.Empty;
            Regex regBody = new Regex(@"(?is)<body[^>]*>(?:(?!</?body\b).)*</body>");
            Match m = regBody.Match(htmlStr);
            if (m.Success)
            {
                result = parseHtml(m.Value);
            }
            return result;
        }

        //获取所有图片
        public static List<string> getImg(string url)
        {
            List<string> list = new List<string>();
            string temp = string.Empty;
            string htmlStr = getHtml(url);
            MatchCollection matchs = Regex.Matches(htmlStr, @"<(IMG|img)[^>]+>"); //抽取所有图片
            for (int i = 0; i < matchs.Count; i++)
            {
                list.Add(matchs[i].Value);
            }
            return list;
        }

        //所有图片路径(如果是相对路径的话，自动设置成绝对路径)
        public static List<string> getImgPath(string url)
        {
            List<string> list = new List<string>();
            string htmlStr = getHtml(url);
            string pat = @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>";
            MatchCollection matches = Regex.Matches(htmlStr, pat, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Match m in matches)
            {
                string imgPath = m.Groups["imgUrl"].Value.Trim();
                if (Regex.IsMatch(imgPath, @"\w+\.(gif|jpg|bmp|png)$")) //用了2次匹配，去除链接是网页的 只留图片
                {
                    if (!imgPath.Contains("http"))//必须包含http 否则无法下载
                    {
                        imgPath = getUrl(url) + imgPath;
                    }
                    list.Add(imgPath);
                }
            }
            return list;
        }

        //下载图片
        public void DownloadImg(string fileurl)
        {
            if (fileurl.Contains('.'))//url路径必须是绝对路径 例如http://xxx.com/img/logo.jpg 
            {
                string imgName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + fileurl.Substring(fileurl.LastIndexOf('.')); // 生成图片的名字
                string filepath = System.Web.HttpContext.Current.Server.MapPath("") + "/" + imgName;
                WebClient mywebclient = new WebClient();
                mywebclient.DownloadFile(fileurl, filepath);
            }
        }

        //过滤html
        public static string parseHtml(string html)
        {
            string value = Regex.Replace(html, "<[^>]*>", string.Empty);
            value = value.Replace("<", string.Empty);
            value = value.Replace(">", string.Empty);
            //return value.Replace("&nbsp;", string.Empty);

            return Regex.Replace(value, @"\s+", "");
        }

        //处理url路径问题
        public static string getUrl(string url)
        {
            //如果是http://www.xxx.com           返回http://www.xxx.com/
            //如果是http://www.xxx.com/art.aspx  返回http://www.xxx.com/
            return url = url.Substring(0, url.LastIndexOf('/')) + "/";
        }
    }



}
