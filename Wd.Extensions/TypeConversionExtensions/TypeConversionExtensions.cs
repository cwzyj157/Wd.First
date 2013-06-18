using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.Extensions.ObjectExtensions;
using Wd.Extensions.IEnumerableExtensions;

namespace Wd.Extensions.TypeConversionExtensions
{
    public static class TypeConversionExtensions
    {

        #region 异常为空判断
        /// <summary>
        /// 判断对象是否为空，为空则抛出参数空异常
        /// </summary>
        /// <param name="o">判断对象</param>
        /// <param name="param">参数名称</param>
        public static void ThrowIfNull(this object o, string paramName)
        {
            if (o.IsNull()) throw new ArgumentNullException(paramName);
        }

        public static void ThrowIfNullOrEmpty<T>(this IEnumerable<T> enumerable, string paramName)
        {
            if (enumerable.IsNullOrEmpty()) throw new ArgumentNullException(paramName);
        }
        #endregion


        #region NullCheck
        /// <summary>
        /// 对对象进行空检查，如果为空返回默认值，否则返回对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="Object">对象</param>
        /// <param name="DefaultValue">对像为空时默认值</param>
        /// <returns>如果为空返回默认值，否则返回对象</returns>
        public static T NullCheck<T>(this T Object, T DefaultValue = default(T))
        {
            return Object == null ? DefaultValue : Object;
        }
        #endregion
    }
}
