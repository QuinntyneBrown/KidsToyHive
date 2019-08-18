using KidsToyHive.Core.Interfaces;
using System.Collections.Concurrent;

namespace KidsToyHive.Core.Services
{
    public class InMemoryCache : ICache
    {
        private ConcurrentDictionary<string, object> _concurrentDictionary = new ConcurrentDictionary<string, object>();

        public void Add(object objectToCache, string key) => _concurrentDictionary.TryAdd(key, objectToCache);

        public void Add<T>(object objectToCache, string key) => Add(objectToCache, key);

        public void Add<T>(object objectToCache, string key, double cacheDuration) => Add(objectToCache, key);

        public void ClearAll() => _concurrentDictionary = new ConcurrentDictionary<string, object>();

        public bool Exists(string key) => Get(key) != null;

        public T Get<T>(string key) => (T)Get(key);

        public object Get(string key)
        {
            _concurrentDictionary.TryGetValue(key, out object value);

            return value;
        }

        public void Remove(string key) => _concurrentDictionary.TryRemove(key, out _);
    }
}