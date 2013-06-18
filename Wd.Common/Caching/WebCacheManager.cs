using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;

namespace Wd.Common.Caching
{
    /// <summary>
    /// 此类还得继续修改
    /// </summary>
    public class WebCacheManager
    {
        #region 系统缓存建议使用方式
        /// <summary>
        /// HttpRuntime.Cache(运行时缓存，适用了一切对象，对象存储在内存中。效率相对LWCache稍差)
        /// </summary>
        public static System.Web.Caching.Cache HttpRuntimeCache
        {
            get { return HttpRuntime.Cache; }
        }
        #endregion

        #region 常用方法
        /// <summary>
        /// 通过Key取得缓存对象，如果不存在，则新建一个对象，并将对象添加进缓存
        /// </summary>
        /// <typeparam name="T">创建对象类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>返回缓存对象</returns>
        public static T GetIfNullInsert<T>(string key)
            where T : class, new()
        {
            T result = HttpRuntimeCache[key] as T;
            if (result == null)
            {
                result = new T();
                HttpRuntimeCache[key] = result;
            }
            return result;
        }
        /// <summary>
        /// 通过Key取得缓存对象，如果不存在，则执行Func，并将Func执行结果添加进缓存中
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="func">Func</param>
        /// <returns>返回缓存对象</returns>
        public static T GetIfNullFunc<T>(string key, Func<T> func)
        {
            object o = HttpRuntimeCache[key];
            if (o == null)
            {
                o = func();
                HttpRuntimeCache[key] = o;
            }
            return (T)o;
        }
        /// <summary>
        /// 通过Key取得缓存对象，如果不存在，则新建一个对象，原后执行Func填充该对象，并返回对象
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="func">Func</param>
        /// <returns>返回缓存对象</returns>
        public static T GetIfNullInsertThenFunc<T>(string key, Func<T, T> func)
             where T : class, new()
        {
            T result = HttpRuntimeCache[key] as T;
            if (result == null)
            {
                result = new T();
            }
            result = func(result);
            HttpRuntimeCache[key] = result;
            return result;
        }
        #endregion
    }
}
