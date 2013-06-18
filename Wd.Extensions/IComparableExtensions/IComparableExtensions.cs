using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Extensions.IComparableExtensions
{
    public static class IComparableExtensions
    {
        /// <summary>
        /// 比较两个可以比较对象的大小
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">IComparable<T>对象</param>
        /// <param name="lowerBound">最小边界</param>
        /// <param name="upperBound">最大边界</param>
        /// <param name="includeLowerBound">是否包含最小值（默认为false）</param>
        /// <param name="includeUpperBound">是否包含最大值（默认为false）</param>
        /// <returns>比较结果</returns>
        public static bool IsBetween<T>(this IComparable<T> t, T lowerBound, T upperBound,
                                        bool includeLowerBound = false, bool includeUpperBound = false)
        {
            if (t == null) throw new ArgumentNullException("t");

            var lowerCompareResult = t.CompareTo(lowerBound);
            var upperCompareResult = t.CompareTo(upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }
        /// <summary>
        /// 通过特定比较方法比较两对象的大小
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象类型</param>
        /// <param name="lowerBound">最小边界</param>
        /// <param name="upperBound">最大边界</param>
        /// <param name="comparer">比较方法</param>
        /// <param name="includeLowerBound">是否包含最小值（默认为false）</param>
        /// <param name="includeUpperBound">是否包含最大值（默认为false）</param>
        /// <returns>比较结果</returns>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound, IComparer<T> comparer,
                                        bool includeLowerBound = false, bool includeUpperBound = false)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");

            var lowerCompareResult = comparer.Compare(t, lowerBound);
            var upperCompareResult = comparer.Compare(t, upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }
    }
}
