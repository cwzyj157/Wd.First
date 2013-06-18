using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.Extensions.ObjectExtensions;

namespace Wd.Extensions.ValueTypeExtensions
{
    public static class ValueTypeExtensions
    {
        #region ToBase64String
        /// <summary>
        /// 将一个字节数组转换成64位字符串
        /// </summary>
        /// <param name="Input">字节数组</param>
        /// <returns>转换后的字符串</returns>
        public static string ToBase64String(this byte[] Input)
        {
            return Input.IsNull() ? "" : Convert.ToBase64String(Input);
        }

        #endregion
    }
}
