using System;
using System.Collections.Generic;
using System.Text;

namespace Middleware_Tool
{
    public interface ICache
    {   /// <summary>
        /// 根据Key取对应的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">过期时间（秒）</param>
        void Set<T>(string key, T data, int cacheTime = 30);

        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">过期时间（秒）</param>
        void Set<T>(string key, T data);

        /// <summary>
        /// 添加到缓存项，滑动过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">过期时间（秒）</param>
        void SetSliding<T>(string key, T data, int cacheTime = 30);

        /// <summary>
        /// 缓存容器中是否存在对应的Key,true:存在,false:不存在
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        /// 从缓存容器中移除对应的Key值项
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 清空缓存
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// 根据Key索引值，获取缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// 获取缓存总数据项的个数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 通过值获取所有的Key
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        List<string> GetKeys<T>(T value);


        /// <summary>
        /// 获取所有缓存键
        /// </summary>
        /// <returns></returns>
        List<string> GetCacheKeys();
    }
}