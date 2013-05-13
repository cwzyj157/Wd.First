using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using OtherTestProject.Console.RssPaper;

namespace OtherTestProject.Console
{
    partial class Program
    {
        static void RssChannelHelperTest()
        {
            //new RssChannelHelper("http://www.sciencenet.cn/xml/news.aspx?news=0", (m) =>
            //{
            //    ((IRssChannelItem)new SqlRssChannelItem()).Handle(m as IEnumerable<RssChannelItem>);
            //}).AsyncGetRssChannelItems();


            List<string> lst = new List<string>();
            lst.Add("http://www.sciencenet.cn/xml/news.aspx?di=0");
            lst.Add("http://www.sciencenet.cn/xml/news.aspx?di=3");
            lst.Add("http://www.sciencenet.cn/xml/news.aspx?di=9");
            lst.Add("http://www.guokr.com/rss/");
            lst.Add("http://feed.feedsky.com/my1510newest");
            lst.Add("http://www.alibuybuy.com/feed");
            lst.Add("http://www.u148.net/rss/");


            new RssChannelHelper(lst, (m) =>
            {
                ((IRssChannelItem)new SqlRssChannelItem()).Handle(m as IEnumerable<RssChannelItem>);
            }).AsyncGetRssChannelItems();
        }
    }
}

namespace OtherTestProject.Console.RssPaper
{
    internal sealed class RssChannelHelper
    {
        private Action<object> itemResolveAction;
        private List<string> lstRssUrl;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rssUrl"></param>
        public RssChannelHelper(string rssUrl, Action<object> itemResolveAction)
        {
            if (string.IsNullOrEmpty(rssUrl))
                throw new ArgumentNullException("rssUrl");

            lstRssUrl = new string[] { rssUrl }.ToList();
            this.itemResolveAction = itemResolveAction;

        }
        public RssChannelHelper(IEnumerable<string> lstRssUrl, Action<object> itemResolveAction)
        {
            if (lstRssUrl == null)
                throw new ArgumentNullException("lstRssUrl");

            this.lstRssUrl = lstRssUrl.ToList();
            this.itemResolveAction = itemResolveAction;
        }
        /// <summary>
        /// 取得源频道文章
        /// </summary>
        /// <returns></returns>
        public void AsyncGetRssChannelItems()
        {
            foreach (string rssUrl in lstRssUrl)
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(rssUrl);
                webRequest.BeginGetResponse(new AsyncCallback(RequestCallBack), webRequest);
            }
        }
        private void RequestCallBack(IAsyncResult ar)
        {
            HttpWebRequest webRequest = ar.AsyncState as HttpWebRequest;
            if (webRequest != null)
            {
                try
                {
                    using (WebResponse response = webRequest.EndGetResponse(ar))
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string channelInfos = reader.ReadToEnd();

                                // 对返回的进行处理
                                if (itemResolveAction != null)
                                {
                                    itemResolveAction(new RssChannelItemResolve(channelInfos).GetItems());
                                }
                            }
                        }
                    }
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }
        }
    }

}
