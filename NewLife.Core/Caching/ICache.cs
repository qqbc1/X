﻿using System;
using System.Collections.Generic;

namespace NewLife.Caching
{
    /// <summary>缓存接口</summary>
    public interface ICache
    {
        /// <summary>名称</summary>
        String Name { get; }

        /// <summary>获取和设置缓存，永不过期</summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Object this[String key] { get; set; }

        /// <summary>缓存个数</summary>
        Int32 Count { get; }

        /// <summary>所有键</summary>
        ICollection<String> Keys { get; }

        /// <summary>是否包含缓存项</summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Boolean ContainsKey(String key);

        /// <summary>设置缓存项</summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间，秒</param>
        /// <returns></returns>
        Boolean Set<T>(String key, T value, Int32 expire = 0);

        /// <summary>设置缓存项</summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        Boolean Set<T>(String key, T value, TimeSpan expire);

        /// <summary>获取缓存项</summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        T Get<T>(String key);

        /// <summary>批量获取缓存项</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        IDictionary<String, T> GetAll<T>(params String[] keys);

        /// <summary>批量设置缓存项</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        void SetAll<T>(IDictionary<String, T> values);

        /// <summary>移除缓存项</summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        Boolean Remove(String key);

        /// <summary>累加，原子操作</summary>
        /// <param name="key"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Int32 Increment(String key, Int32 amount);

        /// <summary>递减，原子操作</summary>
        /// <param name="key"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Int32 Decrement(String key, Int32 amount);
    }
}