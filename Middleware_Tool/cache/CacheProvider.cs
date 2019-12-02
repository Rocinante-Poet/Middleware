using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Middleware_Tool
{
    public class CacheProvider : ICache
    {
        private IMemoryCache _cache;

        public CacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            T objValue;
            if (!string.IsNullOrEmpty(key) && _cache.TryGetValue<T>(key, out objValue))
            {
                return objValue;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 根据Key索引值，获取缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return _cache.Get(key);
        }

        /// <summary>
        /// 增加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime">秒</param>
        public void Set<T>(string key, T data, int cacheTime = 30)
        {
            if (data == null || string.IsNullOrEmpty(key))
            {
                return;
            }
            _cache.Set<T>(key, data, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheTime)
            });
        }

        /// <summary>
        /// 增加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime">秒</param>
        public void Set<T>(string key, T data)
        {
            if (data == null || string.IsNullOrEmpty(key))
            {
                return;
            }
            _cache.Set<T>(key, data);
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            object objValue = null;
            return !string.IsNullOrEmpty(key) && _cache.TryGetValue(key, out objValue);
        }

        /// <summary>
        /// 获取缓存总数据项的个数
        /// </summary>
        public int Count { get { return 0; } }

        /// <summary>
        /// 单个清除
        /// </summary>
        /// <param name="key">/key</param>
        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// 根据键值返回缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get { return _cache.Get(key); }
            set { Set(key, value); }
        }

        /// <summary>
        /// 清除全部数据
        /// </summary>
        public void RemoveAll()
        {
            var allKeys = GetCacheKeys();
            foreach (var key in allKeys)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// 获取所有缓存键
        /// </summary>
        /// <returns></returns>
        public List<string> GetCacheKeys()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var entries = _cache.GetType().GetField("_entries", flags).GetValue(_cache);
            var cacheItems = entries as IDictionary;
            var keys = new List<string>();
            if (cacheItems == null) return keys;
            foreach (DictionaryEntry cacheItem in cacheItems)
            {
                keys.Add(cacheItem.Key.ToString());
            }
            return keys;
        }

        /// <summary>
        /// 通过值获取所有的Key,
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<string> GetKeys<T>(T value)
        {
            List<string> keys = new List<string>();
            var allKeys = GetCacheKeys();
            foreach (var key in allKeys)
            {
                object objValue = null;
                if (!string.IsNullOrEmpty(key) && _cache.TryGetValue(key, out objValue))
                {
                    if (objValue.GetType() == typeof(T))
                    {
                        var cacheValue = (T)objValue;
                        if (cacheValue.Equals(value))
                        {
                            keys.Add(key);
                        }
                    }
                }
            }
            return keys;
        }

        public void SetSliding<T>(string key, T data, int cacheTime = 30)
        {
            if (data == null || string.IsNullOrEmpty(key))
            {
                return;
            }
            _cache.Set<T>(key, data, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(cacheTime)
            });
        }
    }
}