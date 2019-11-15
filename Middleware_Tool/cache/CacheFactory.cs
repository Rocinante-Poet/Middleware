using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Tool.cache
{
    public class CacheFactory
    {
        static CacheFactory()
        {
            GetCache = new CacheProvider(new MemoryCache(Options.Create(new MemoryCacheOptions())));
        }
        public static ICache GetCache { get; }
    }
}
