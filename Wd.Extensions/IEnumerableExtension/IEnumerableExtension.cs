using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Wd.Extensions
{
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 将数组以指定字符拼接
        /// </summary>
        /// <param name="eumerable">数组</param>
        /// <param name="splitChar">指定字符</param>
        /// <returns>拼接后结果</returns>
        public static string Join(this IEnumerable eumerable, string splitChar)
        {
            StringBuilder sbResult = new StringBuilder();
            IEnumerator enumerator = eumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current != null)
                    sbResult.Append(enumerator.Current.ToString())
                            .Append(splitChar);
            }
            return sbResult.ToString();
        }
    }
}
