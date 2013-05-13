using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Wd.Extensions;

namespace Wd.Common
{
    public class UrlParams
    {
        // 定义结果集
        private NameValueCollection urlParams;

        public UrlParams(string url)
        {
            if (urlParams == null)
            {
                urlParams = new NameValueCollection();
                GetParams(url);
            }
        }

        /// <summary>
        /// 所有参数列表集
        /// </summary>
        public NameValueCollection Params
        {
            get
            {
                return urlParams;
            }
        }
        /// <summary>
        /// 取得所有的url参数列表
        /// </summary>
        /// <param name="url">URL</param>
        /// <remarks>author:cw 2012-05-24</remarks>
        private void GetParams(string url)
        {
            int paramStartPosition = url.LastIndexOf("?");
            if (paramStartPosition <= 0) return;

            string pattern = @"(?<=\?|&)(.*?=.*?)(?=&|#|$)";
            MatchCollection mc = url.Matches(pattern, StringRegexOptions.IgnoreCase | StringRegexOptions.Multiline);

            if (mc.Count > 0)
            {
                foreach (Match m in mc)
                {
                    AddToDict(m.Value);
                }
            }
        }
        /// <summary>
        /// 添加到列表中
        /// </summary>
        /// <param name="input">匹配字符串</param>
        /// <remarks>author:cw 2012-05-24</remarks>
        private void AddToDict(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string[] keyValue = input.Split('=');
                if (keyValue.Length == 2)
                {
                    urlParams.Add(keyValue[0], keyValue[1]);
                }
            }
        }
    }
}
