using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Common.Caching.Interface
{
    /// <summary>
    /// 缓存项接口
    /// </summary>
    /// <typeparam name="KeyType"></typeparam>
    public interface ICacheItem<KeyType>
    {
        #region 属性
        /// <summary>
        /// 缓存项对应的Key
        /// </summary>
        KeyType Key { get; set; }
        #endregion
    }
    public interface ICacheItem
    {
        #region 属性
        /// <summary>
        /// 缓存值
        /// </summary>
        object Value { get; set; }
        #endregion
    }
}
