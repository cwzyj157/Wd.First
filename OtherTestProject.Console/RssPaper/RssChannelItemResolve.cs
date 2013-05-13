using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OtherTestProject.Console.RssPaper
{
    internal class RssChannelItemResolve
    {
        private readonly string xmlString;
        private XmlDocument doc;

        public RssChannelItemResolve(string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString))
                throw new ArgumentNullException("xmlString");

            this.xmlString = xmlString;
            doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlString);
            }
            catch (XmlException ex)
            {
                doc = null;
            }
        }

        public IEnumerable<RssChannelItem> GetItems()
        {
            if (doc != null)
            {
                XmlNodeList xnl = doc.SelectNodes("//item");
                foreach (XmlElement xe in xnl)
                {
                    yield return new RssChannelItem(
                      xe["title"] == null ? "" : xe["title"].InnerText,
                      xe["link"] == null ? "" : xe["link"].InnerText,
                      xe["description"] == null ? "" : xe["description"].InnerText,
                      xe["author"] == null ? "" : xe["author"].InnerText,
                      xe["category"] == null ? "" : xe["category"].InnerText,
                      xe["comments"] == null ? "" : xe["comments"].InnerText,
                      xe["guid"] == null ? "" : xe["guid"].InnerText,
                      xe["pubDate"] == null ? "" : xe["pubDate"].InnerText,
                      xe["source"] == null ? "" : xe["source"].InnerText,
                      xe["copyright"] == null ? "" : xe["copyright"].InnerText);
                }
            }
        }
    }

    public struct RssChannelItem
    {
        public string title;
        public string link;
        public string description;
        public string author;
        public string category;
        public string comments;
        public string guid;
        public string pubDate;
        public string source;
        public string copyright;

        public RssChannelItem(string title, string link, string description, string author,
            string category, string comments, string guid, string pubDate, string source, string copyright)
        {
            this.title = title;
            this.link = link;
            this.description = description;
            this.author = author;
            this.category = category;
            this.comments = comments;
            this.guid = guid;
            this.pubDate = pubDate;
            this.source = source;
            this.copyright = copyright;
        }
    }
}
