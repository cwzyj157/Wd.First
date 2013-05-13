using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Wd.Common
{
    /// <summary>
    /// 对Html进行相关的操作
    /// </summary>
    public static class HtmlHelper
    {
        #region 字符串替换
        // 对html内容进行去掉空格处理
        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s*", RegexOptions.Compiled);
        private static readonly Regex REGEX_LINE_SPACE = new Regex(@"\r\s*", RegexOptions.Compiled);
        private static readonly Regex REGEX_SPACE = new Regex(@"( )+", RegexOptions.Compiled);
        // 对html内容进行去掉注释
        private static readonly Regex REGEX_COMMENT = new Regex("(<!--.*?-->)", RegexOptions.Compiled);
        /// <summary>
        /// 对输入字符串替换空格
        /// </summary>
        /// <param name="content">字符内容</param>
        /// <returns>替换后字符内容</returns>
        public static string ReplaceSpace(this string content)
        {
            content = REGEX_LINE_BREAKS.Replace(content, "");
            content = REGEX_LINE_SPACE.Replace(content, "");
            content = REGEX_SPACE.Replace(content, "");
            return content;
        }
        /// <summary>
        /// 对输入字符串替换注释（常用于HTML，XML）
        /// </summary>
        /// <param name="content">字符内容</param>
        /// <returns>替换后字符内容</returns>
        public static string ReplaceComment(this string content)
        {
            content = REGEX_COMMENT.Replace(content, "");
            return content;
        }
        #endregion
    }
}
