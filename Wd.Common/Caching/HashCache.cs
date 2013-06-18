using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.Common.Caching.Interface;

namespace Wd.Common.Caching
{
    /// <summary>
    /// 轻量级缓存(使用Hashtable存放key=value形式的小数据量的缓存e.g "IsWriteLog"="T"。效率高)
    /// </summary>
    public class HashCache
    {
        // init a thread-safe hashtable that contains object's life cycle
        private static Hashtable htLightWeightCache = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 轻量级缓存(使用Hashtable存放key=value形式的小数据量的缓存e.g "IsWriteLog"="T"。效率高)
        /// </summary>
        public static Hashtable HtCache
        {
            get { return htLightWeightCache; }
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void Clear()
        {
            htLightWeightCache.Clear();
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        public static void Remove(object key)
        {
            htLightWeightCache.Remove(key);
        }
        /// <summary>
        /// 检查在缓存中Key是否存在
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <returns>[True:存在，False:不存在]</returns>
        public static bool Exists(object key)
        {
            return htLightWeightCache.ContainsKey(key);
        }
        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        public static void Add(object key, object value)
        {
            htLightWeightCache.Add(key, value);
        }
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="ValueType">返回值类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns>缓存Key值对应的值</returns>
        public static object Get(object key)
        {
            return Exists(key) ? htLightWeightCache[key] : null;
        }
        public static ICollection Keys
        {
            get { return htLightWeightCache.Keys; }
        }

        public static int Count
        {
            get { return htLightWeightCache.Count; }
        }
    }
}
