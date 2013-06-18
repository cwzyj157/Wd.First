using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.Extensions.TypeConversionExtensions;

namespace Wd.Extensions.StringExtensions
{
    public static class StringExtensions
    {
        #region ToByteArray
        /// <summary>
        /// 将字符串转换成指定编码的字节数组
        /// </summary>
        /// <param name="Input">字符串输入</param>
        /// <param name="EncodingUsing">指定编码（默认为UTF8）</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] ToByteArray(this string input, Encoding encodingUsing = null)
        {
            return string.IsNullOrEmpty(input) ? null : encodingUsing.NullCheck(new UTF8Encoding()).GetBytes(input);
        }
        #endregion
    }
}
