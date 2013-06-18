using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wd.Common.Caching;
namespace Wd.Common.Test.Cache
{
    partial class Program
    {

    }
    internal class Cache
    {
        internal static void Test()
        {
            // HashCache
            // WebCacheManager
            // DictCache
            Wd.Common.Caching.DictCache<string> dictCache = new Wd.Common.Caching.DictCache<string>();
            dictCache.Add("key1", "value1");

        }
    }
}
