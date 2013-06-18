using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Wd.Extensions.ObjectExtensions;

namespace Wd.Extensions.IEnumerableExtensions
{
    public static class IEnumerableExtensions
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
        /// <summary>
        /// 列表是否为空
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="Value">数据列表</param>
        /// <returns>[True:数据列表为NULL或空，False:数据列表不为空]</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> Value)
        {
            return Value.IsNull() || Value.Count() == 0;
        }
    }
}
