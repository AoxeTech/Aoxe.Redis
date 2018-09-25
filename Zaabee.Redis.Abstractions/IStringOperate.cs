using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.Redis.Abstractions
{
    public interface IStringOperate
    {
        bool Add<T>(string key, T entity, TimeSpan? expiry = null);

        Task<bool> AddAsync<T>(string key, T entity, TimeSpan? expiry = null);

        void AddRange<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null);

        Task AddRangeAsync<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null);

        T Get<T>(string key);

        Task<T> GetAsync<T>(string key);

        List<T> Get<T>(IList<string> keys);

        Task<List<T>> GetAsync<T>(IList<string> keys);
    }
}