using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace OtherTestProject.Console
{
    public class ReplaceEfficiency
    {
        private Dictionary<string, string> dict = new Dictionary<string, string>();
        private XmlDocument doc;
        public void InitResource()
        {
            GetXmlDocument();

            dict.Add("AAA.BBB.CCC", "3. 检索结果分组筛选：");
            dict.Add("AAA.BBB.CCC1", "仅对前4万篇文献分组，取前60个分组词");
            dict.Add("AAA.BBB.CCC2", "检索结果不错");
            dict.Add("AAA.BBB.CCC3", "生成检索报告");
            dict.Add("AAA.BBB.CCC4", "定制或收藏本次检索式");
        }

        // 1.普通方式
        public string ReplaceByBasicMethod()
        {
            string strReplace = GetZoneHtml("group_1");

            int index = strReplace.IndexOf("<%=");
            if (index < 0)
                return strReplace;

            do
            {
                int nextIndex = strReplace.IndexOf("%>", index);

                string key = strReplace.Substring(index + 3, nextIndex - index - 3);

                strReplace = strReplace.Replace("<%=" + key + "%>", dict[key.Trim()]);

                index = strReplace.IndexOf("<%=", nextIndex);
            }
            while (index > 0);
            return strReplace;
        }
        // 2.正则表达式
        public string ReplaceByRegex()
        {
            string strReplace = GetZoneHtml("group_1");

            Regex regex = new Regex(@"(<%=.*?%>)", RegexOptions.ExplicitCapture);
            MatchCollection mc = regex.Matches(strReplace);

            foreach (Match m in mc)
            {
                string key = m.Value.Substring(3, m.Value.Length - 5);
                strReplace = strReplace.Replace(m.Value, dict[key.Trim()]);
            }
            return strReplace;
        }

        // 3.添加引用方式
        public string ReplaceByRef()
        {
            XmlNode xn = GetZoneNode("group");
            string html = xn.ChildNodes[0].InnerText;

            for (int i = 1; i < xn.ChildNodes.Count; i++)
            {
                html = html.Replace(string.Format("[//ref[@id='{0}']]", xn.ChildNodes[i].Attributes["id"].Value),
                                   dict[xn.ChildNodes[i].Attributes["value"].Value]
                                );
            }
            return html;
        }

        private string GetZoneHtml(string idValue)
        {
            XmlNode xn = GetZoneNode(idValue);
            string html = xn.ChildNodes[0].InnerText;
            return html;
        }

        private XmlNode GetZoneNode(string idValue)
        {
            return doc.SelectSingleNode("//zone[@id='" + idValue + "']");
        }

        public XmlDocument GetXmlDocument()
        {
            if (doc != null) return doc;
            doc = new XmlDocument();
            doc.Load(@"D:\cw\ProjectCode\Just_Code\OtherTestProject\OtherTestProject.Console\OtherTestProject.Console\TestReplaceEfficiency\SCDB.xml");
            return doc;
        }
    }
    public class StringUnderTest
    {
        public static string strReplace = @"<div id='divgrouptxt' name='divgrouptxt' style='display:none'>	
				<table width='98%' border='0' align='center' cellpadding='0' cellspacing='0'>
					<tr>
						<td height='5'></td>
					</tr>
					<tr>
						<td height='1' bgcolor='#d6d6d6'></td>
					</tr>
					<tr>
						<td height='5'></td>
					</tr>
				</table>
				<div class='rightseachtitle' ><div style='float:left' ><strong><%=AAA.BBB.CCC%></strong><span style='color: #666;' >(<%=AAA.BBB.CCC1%>)</span>
            </div><div class='class_grid_jcbg' >
            <ul><li class='xswx_anniutext'><span style='color:#000'><%=AAA.BBB.CCC2%>，</span></li><li style='padding-top:4px;margin-right:16px;'><span id='divreport' ><a href='javascript:SubmitRep('/grid2008/SearchReport/SearchReport.aspx','report');'><%=AAA.BBB.CCC3%></a></span></li>
            <li  id='divresult' style='padding-top:4px;'><span  ><a href='javascript:SubmitUser();'><%=AAA.BBB.CCC4%></a></span></li>
            </ul>
            </div></div>
			</div>";

        // 3. 检索结果分组筛选：<%=AAA.BBB.CCC%>
        // 仅对前4万篇文献分组，取前60个分组词 <%=AAA.BBB.CCC1%>
        // 检索结果不错 <%=AAA.BBB.CCC2%>
        // 生成检索报告 <%=AAA.BBB.CCC3%>
        // 定制或收藏本次检索式 <%=AAA.BBB.CCC4%>
    }
}
