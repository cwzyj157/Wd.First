using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.Common.Caching.Interface;

namespace Wd.Common.Caching
{
    public class CacheItem<KeyType> : ICacheItem, ICacheItem<KeyType>
    {
        public virtual object Value { get; set; }
        public virtual KeyType Key { get; set; }

        public CacheItem(KeyType key, object value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
