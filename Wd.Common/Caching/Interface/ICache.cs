using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wd.Common.Caching.Interface
{
    /// <summary>
    /// Cache 接口
    /// </summary>
    /// <typeparam name="KeyType"></typeparam>
    public interface ICache<KeyType> : IEnumerable<object>
    {
        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(KeyType key);
        /// <summary>
        /// 检查在缓存中Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(KeyType key);
        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        void Add(KeyType key, object value);
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="ValueType">返回值类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns>缓存Key值对应的值</returns>
        ValueType Get<ValueType>(KeyType key);
        /// <summary>
        /// 尝试获取缓存项
        /// </summary>
        /// <typeparam name="ValueType">返回值类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Key值对应的值</param>
        /// <returns>是否获取成功</returns>
        bool TryGet<ValueType>(KeyType key, out ValueType value);



        #region 属性
        /// <summary>
        /// 缓存key集合
        /// </summary>
        ICollection<KeyType> Keys { get; }
        /// <summary>
        /// 缓存项数
        /// </summary>
        int Count { get; }
        /// <summary>
        /// 获取缓存对应KEY的缓存值
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns>缓存KEY的缓存值</returns>
        object this[KeyType key] { get; set; }
        #endregion
    }
}
