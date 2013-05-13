using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace OtherTestProject.Console
{
    partial class Program
    {
        public static void XmlResolveTest()
        {
            FileInfo fi = new FileInfo(@"D:\cw\ProjectCode\Just_Code\OtherTestProject\OtherTestProject.Console\Wd.Extensions.Test\XMLFile1.xml");
            string xmlstring = fi.OpenText().ReadToEnd();

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlstring = XmlString.Format(xmlstring);
                xmlDoc.LoadXml(xmlstring);
            }
            catch (XmlException ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }
    }
}

namespace OtherTestProject.Console
{
    /// <summary>
    /// Xml字符串整理，消除不合法的XML文档
    /// </summary>
    internal class XmlString
    {
        private static readonly string[] xmlNodes = new string[] { 
            "link","description","language","copyRight","ManagingEditor",
            "webMaster","pubDate","lastBuildDate","category","generator","docs",
            "cloud","ttl","rating","skiphours","skipdays",
            "author","comments","title","comments","guid","source","copyright","url"
        };

        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s*", RegexOptions.Compiled);
        private static readonly Regex REGEX_LINE_SPACE = new Regex(@"\r\s*", RegexOptions.Compiled);
        private static string regexNodeContent = "(?<=<{0}[^>]*>).*?(?=</{0}>)";

        public static string Format(string input)
        {
            input = ReplaceSpace(input);
            StringBuilder sbInput = new StringBuilder(input.Length).Append(input);
            foreach (string nodeName in xmlNodes)
            {
                FormatNode(sbInput, nodeName);
            }
            return sbInput.ToString();
        }
        private static string ReplaceSpace(string input)
        {
            input = REGEX_LINE_BREAKS.Replace(input, "");
            input = REGEX_LINE_SPACE.Replace(input, "");
            return input;
        }
        private static void FormatNode(StringBuilder input, string nodeName)
        {
            Regex regex = new Regex(string.Format(regexNodeContent, nodeName), RegexOptions.IgnoreCase);
            Match m = regex.Match(input.ToString());
            int cdataIndex = -1;
            int offset = 0;
            while (m.Success)
            {
                cdataIndex = m.Value.IndexOf("<![CDATA[");
                if (cdataIndex < 0 && !string.IsNullOrEmpty(m.Value))
                {
                    input.Replace(m.Value, "<![CDATA[" + m.Value + "]]>", m.Index + offset, m.Value.Length);
                    offset += 12;
                }
                m = m.NextMatch();
            }
        }
    }
}
