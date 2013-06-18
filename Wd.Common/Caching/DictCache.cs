using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.Common.Caching.Interface;

namespace Wd.Common.Caching
{
    public class DictCache<KeyType> : ICache<KeyType>
    {
        #region Constructor
        public DictCache()
        {
            InternalCache = new Dictionary<KeyType, ICacheItem>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// 内部缓存结构
        /// </summary>
        protected virtual Dictionary<KeyType, ICacheItem> InternalCache { get; set; }

        /// <summary>
        /// 缓存key集合
        /// </summary>
        public virtual ICollection<KeyType> Keys
        {
            get
            {
                lock (InternalCache) return InternalCache.Keys;
            }
        }
        /// <summary>
        /// 缓存项数
        /// </summary>
        public virtual int Count
        {
            get
            {
                lock (InternalCache) return InternalCache.Count;
            }
        }
        /// <summary>
        /// 获取缓存对应KEY的缓存值
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns>缓存KEY的缓存值</returns>
        public virtual object this[KeyType key]
        {
            get
            {
                return this.Get<object>(key);
            }
            set
            {
                this.Add(key, value);
            }
        }
        #endregion

        #region ICache<KeyType> Members
        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            lock (InternalCache) InternalCache.Clear();
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        public void Remove(KeyType key)
        {
            if (Exists(key))
            {
                lock (InternalCache)
                {
                    if (Exists(key)) InternalCache.Remove(key);
                }
            }
        }
        /// <summary>
        /// 检查在缓存中Key是否存在
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <returns>[True:存在，False:不存在]</returns>
        public bool Exists(KeyType key)
        {
            return InternalCache.ContainsKey(key);
        }
        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        public void Add(KeyType key, object value)
        {
            lock (InternalCache)
            {
                if (Exists(key)) InternalCache[key].Value = value;
                else InternalCache.Add(key, new CacheItem<KeyType>(key, value));
            }
        }
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="ValueType">返回值类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns>缓存Key值对应的值</returns>
        public ValueType Get<ValueType>(KeyType key)
        {
            lock (InternalCache)
            {
                return Exists(key) ? (ValueType)InternalCache[key].Value : default(ValueType);
            }
        }
        /// <summary>
        /// 尝试获取缓存项
        /// </summary>
        /// <typeparam name="ValueType">返回值类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Key值对应的值</param>
        /// <returns>是否获取成功</returns>
        public bool TryGet<ValueType>(KeyType key, out ValueType value)
        {
            value = default(ValueType);
            bool result = false;
            lock (InternalCache)
            {
                if (Exists(key))
                {
                    try
                    {
                        value = (ValueType)InternalCache[key].Value;
                        result = true;
                    }
                    catch (InvalidCastException) { result = false; }
                }
            }
            return result;
        }
        #endregion

        #region IEnumerable<object> Members

        public IEnumerator<object> GetEnumerator()
        {
            lock (InternalCache)
            {
                foreach (KeyType key in this.InternalCache.Keys)
                    yield return (object)this.InternalCache[key].Value;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            lock (InternalCache)
            {
                foreach (KeyType key in this.InternalCache.Keys)
                    yield return this.InternalCache[key].Value;
            }
        }

        #endregion
    }
}
