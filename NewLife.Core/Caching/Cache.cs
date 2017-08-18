﻿using System;
using System.Collections.Generic;

namespace NewLife.Caching
{
    /// <summary>缓存</summary>
    public abstract class Cache : DisposeBase, ICache
    {
        #region 静态默认实现
        /// <summary>默认缓存</summary>
        public static ICache Default { get; set; } = new MyCache();
        #endregion

        #region 属性
        /// <summary>名称</summary>
        public String Name { get; protected set; }

        /// <summary>获取和设置缓存，永不过期</summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Object this[String key] { get { return Get<Object>(key); } set { Set(key, value); } }

        /// <summary>缓存个数</summary>
        public abstract Int32 Count { get; }

        /// <summary>所有键</summary>
        public abstract ICollection<String> Keys { get; }
        #endregion

        #region 方法
        /// <summary>是否包含缓存项</summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract Boolean ContainsKey(String key);

        /// <summary>设置缓存项</summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间，秒</param>
        /// <returns></returns>
        public virtual Boolean Set<T>(String key, T value, Int32 expire = 0)
        {
            if (expire > 0)
                return Set(key, value, new TimeSpan(0, 0, expire));
            else
                return Set(key, value, new TimeSpan(365, 0, 0, 0));
        }

        /// <summary>设置缓存项</summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public abstract Boolean Set<T>(String key, T value, TimeSpan expire);

        /// <summary>获取缓存项</summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public abstract T Get<T>(String key);

        /// <summary>移除缓存项</summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public abstract Boolean Remove(String key);
        #endregion

        #region 高级操作
        /// <summary>批量获取缓存项</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual IDictionary<String, T> GetAll<T>(params String[] keys)
        {
            var dic = new Dictionary<String, T>();
            foreach (var key in keys)
            {
                dic[key] = Get<T>(key);
            }

            return dic;
        }

        /// <summary>批量设置缓存项</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        public virtual void SetAll<T>(IDictionary<String, T> values)
        {
            foreach (var item in values)
            {
                Set(item.Key, item.Value);
            }
        }

        /// <summary>累加，原子操作</summary>
        /// <param name="key"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual Int32 Increment(String key, Int32 amount)
        {
            lock (this)
            {
                var v = Get<Int32>(key);
                v += amount;
                Set(key, v);

                return v;
            }
        }

        /// <summary>递减，原子操作</summary>
        /// <param name="key"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual Int32 Decrement(String key, Int32 amount)
        {
            lock (this)
            {
                var v = Get<Int32>(key);
                v -= amount;
                Set(key, v);

                return v;
            }
        }
        #endregion

        #region 辅助
        /// <summary>已重载。</summary>
        /// <returns></returns>
        public override String ToString() { return Name; }
        #endregion
    }
}