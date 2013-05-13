using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Wd.Common
{
    /// <summary>
    /// 正则表达式辅助类
    /// </summary>
    public class RegexHelper
    {
        /// <summary>
        /// 常用正则表达式字符串
        /// </summary>
        public struct RegexString
        {
            /// <summary>
            /// 匹配html空格字符@nbsp;
            /// </summary>
            public static readonly Regex matchHtmlSpace = new Regex(@"&nbsp;|\t", RegexOptions.Compiled);
            /// <summary>
            /// 匹配右尖括号到左尖括号之间的内容,已被matchGt2Lt替换 
            /// </summary>
            public static readonly Regex matchGt2Lt1 = new Regex(@"(?<=[>]).*?(?=[<])", RegexOptions.Compiled);
            /// <summary>
            /// 匹配input开头的 
            /// </summary>
            public static readonly Regex matchInput = new Regex(@"(?<=\<input).*?(?=[>])", RegexOptions.Compiled);
            /// <summary>
            /// 匹配input中value的值 
            /// </summary>
            public static readonly Regex matchValue = new Regex("(?<=value=\")(.*?)(?=\")", RegexOptions.Compiled);
            /// <summary>
            /// 匹配右尖括号到左尖括号之间的内容,取代matchGt2Lt1
            /// </summary>
            public static readonly Regex matchGt2Lt = new Regex(@"(?<=\>)(?'group')[^\<\>]+(?=<)(?'-group')", RegexOptions.Compiled);
            /// <summary>
            /// 匹配汉字
            /// </summary>
            public static readonly Regex matchChinese = new Regex(@"[\u4e00-\u9fa5]", RegexOptions.Compiled);
            /// <summary>
            /// 匹配全数字
            /// </summary>
            public static readonly Regex matchAllNumber = new Regex(@"^\d+$", RegexOptions.Compiled);
            /// <summary>
            /// 匹配Title之间的内容
            /// </summary>
            public static readonly Regex matchTitle = new Regex("(?<=[<])[^%/].*?title=\"(.*?)\".*?(?=[>])", RegexOptions.Compiled);
        }
    }

}
