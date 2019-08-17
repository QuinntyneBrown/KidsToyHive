using System;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Interfaces
{
    public interface ICache
    {
        public virtual TResponse FromCacheOrService<TResponse>(Func<TResponse> action, string key)
        {
            var cached = Get(key);
            if (cached == null)
            {
                cached = action();
                Add(cached, key);
            }
            return (TResponse)cached;
        }

        void Add(object objectToCache, string key);

        T Get<T>(string key);

        object Get(string key);

        void Add<T>(object objectToCache, string key);

        void Add<T>(object objectToCache, string key, double cacheDuration);

        void Remove(string key);

        void ClearAll();

        bool Exists(string key);

        public virtual TResponse FromCacheOrService<TResponse>(Func<TResponse> action, string key, double cacheDuration)
        {
            var cached = Get(key);
            if (cached == null)
            {
                cached = action();
                Add<TResponse>(cached, key, cacheDuration);
            }
            return (TResponse)cached;
        }

        public async Task<TResponse> FromCacheOrServiceAsync<TResponse>(Func<Task<TResponse>> action, string key, double cacheDuration)
        {
            var cached = Get(key);
            if (cached == null)
            {
                cached = await action();
                Add<TResponse>(cached, key, cacheDuration);
            }
            return (TResponse)cached;
        }

        public async Task<TResponse> FromCacheOrServiceAsync<TResponse>(Func<Task<TResponse>> action, string key)
        {
            var cached = Get(key);
            if (cached == null)
            {
                cached = await action();
                Add(cached, key);
            }
            return (TResponse)cached;
        }
    }
}