using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SimpleCacheManager
{
    public class CacheManager<T>
    {
        readonly ConcurrentDictionary<string, T> _data = new ConcurrentDictionary<string, T>();
        readonly List<ExpirationCount> _expirationQueue = new List<ExpirationCount>();
        readonly object _sync = new object();

        public bool Contains(string key)
        {
            return _data.ContainsKey(key);
        }

        public T GetData(string key)
        {
            T item;
            _data.TryGetValue(key, out item);
            return item;
        }

        public void Add(string key, T item, DateTime utcNow, TimeSpan lifeSpan)
        {
            _data.AddOrUpdate(key, item, (k, v) => item);
            
            lock (_sync)
            {
                _expirationQueue.Add(new ExpirationCount(key, utcNow, lifeSpan));

                if (!_expirationQueue[0].IsOld(utcNow)) return;

                int i = 0;
                foreach (var expirationCount in _expirationQueue)
                {
                    if (expirationCount.IsOld(utcNow))
                    {
                        T _;
                        _data.TryRemove(expirationCount.Key, out _);
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
                _expirationQueue.RemoveRange(0, i);
            }
        }
    }
}
