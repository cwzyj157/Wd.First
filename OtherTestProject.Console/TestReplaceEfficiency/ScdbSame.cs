using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;


namespace OtherTestProject.Console
{
    public class ScdbSame
    {
        private Dictionary<string, NodeInfo> dictScdb;
        private string folderPath;
        public ScdbSame(string folderPath)
        {
            this.folderPath = folderPath;
            if (Directory.Exists(folderPath) && File.Exists(folderPath + "/SCDB.xml"))
            {
                dictScdb = new XmlDocumentHelper(folderPath + "/SCDB.xml").GetAllZoneNode();
            }
        }
        /// <summary>
        /// 对于单文件
        /// </summary>
        /// <param name="cacheFilePath"></param>
        private void CompareFile(string cacheFilePath)
        {
            if (dictScdb == null || cacheFilePath.IndexOf("SCDB.xml") >= 0) return;

            XmlDocumentHelper xmlDocHelper = new XmlDocumentHelper(cacheFilePath);

            Dictionary<string, NodeInfo> dictCacheFile = xmlDocHelper.GetAllZoneNode();
            if (dictCacheFile != null && dictCacheFile.Count > 0)
            {
                List<KeyValuePair<string, NodeInfo>> enumerator = dictCacheFile.Where(m =>
                {
                    return dictScdb.ContainsKey(m.Key) && dictScdb[m.Key].ZoneHtml == m.Value.ZoneHtml;
                }).ToList();

                if (enumerator.Count > 0)
                {
                    System.Console.WriteLine("文件【{0}】中有以下【{1}】项在SCDB.XML中重复存在",
                        Path.GetFileName(cacheFilePath), enumerator.Count.ToString());

                    enumerator.ForEach(m =>
                    {
                        System.Console.WriteLine("zoneId:\t{0}", m.Key);
                    });

                    System.Console.WriteLine();

                    xmlDocHelper.ModifyXmlFile(enumerator);
                }
            }
        }
        /// <summary>
        /// 对比所有文件
        /// </summary>
        public void CompareAllFile()
        {
            List<string> list = Directory.GetFiles(folderPath, "*.xml").ToList();
            list.ForEach(m => CompareFile(m));
        }
    }
    public class XmlDocumentHelper
    {
        XmlDocument doc;
        private string xmlPath;
        public XmlDocumentHelper(string xmlPath)
        {
            this.xmlPath = xmlPath;
            GetXmlDocument();
        }

        private void GetXmlDocument()
        {
            doc = new XmlDocument();
            try
            {
                doc.Load(xmlPath);
            }
            catch (Exception ex)
            {
            }

        }
        private bool IsExistZoneNode(XmlDocument doc)
        {
            XmlNode firstNode = doc.FirstChild;
            if (firstNode != null && doc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                firstNode = doc.FirstChild.NextSibling;
            return firstNode != null && firstNode.Name == "DataConfiger";
        }
        public Dictionary<string, NodeInfo> GetAllZoneNode()
        {
            Dictionary<string, NodeInfo> dict = null;
            if (!IsExistZoneNode(doc)) return dict;

            XmlNodeList nodeList = doc.DocumentElement.SelectNodes("/DataConfiger/zone");
            if (nodeList != null && nodeList.Count > 0)
            {
                dict = new Dictionary<string, NodeInfo>();
                string zoneId = string.Empty;
                foreach (XmlNode node in nodeList)
                {
                    if (node.NodeType == XmlNodeType.Comment) continue;
                    zoneId = node.Attributes["id"].Value;
                    if (!dict.Keys.Contains(zoneId))
                    {
                        dict.Add(zoneId, new NodeInfo()
                        {
                            ZoneHtml = ReplaceComment(ReplaceSpace(node.InnerText)),
                            node = node,
                            preNode = node.PreviousSibling != null && node.PreviousSibling.NodeType == XmlNodeType.Comment
                                      ? node.PreviousSibling : null
                        });
                    }
                }
            }
            return dict;
        }


        #region 字符串替换
        //对html内容进行去掉空格处理
        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s*", RegexOptions.Compiled);
        private static readonly Regex REGEX_LINE_SPACE = new Regex(@"\r\s*", RegexOptions.Compiled);
        private static readonly Regex REGEX_SPACE = new Regex(@"( )+", RegexOptions.Compiled);

        private static readonly Regex REGEX_COMMENT = new Regex("(<!--.*?-->)", RegexOptions.Compiled);
        /// <summary>
        /// 文件压缩,返回压缩后内容
        /// </summary>
        /// <param name="fileConent">文件内容</param>
        /// <returns></returns>
        private string ReplaceSpace(string content)
        {
            content = REGEX_LINE_BREAKS.Replace(content, "");
            content = REGEX_LINE_SPACE.Replace(content, "");
            content = REGEX_SPACE.Replace(content, " ");
            return content;
        }

        private string ReplaceComment(string content)
        {
            content = REGEX_COMMENT.Replace(content, "");
            return content;
        }
        #endregion

        #region 修改Xml文件
        public void ModifyXmlFile(List<KeyValuePair<string, NodeInfo>> enumerator)
        {
            enumerator.ForEach(m =>
            {
                if (m.Value.preNode != null)
                    doc.DocumentElement.RemoveChild(m.Value.preNode);
                doc.DocumentElement.RemoveChild(m.Value.node);
            });

            doc.Save(xmlPath);
        }
        #endregion
    }


    public class NodeInfo
    {
        public string ZoneHtml;
        public XmlNode node;
        public XmlNode preNode;
    }
}
