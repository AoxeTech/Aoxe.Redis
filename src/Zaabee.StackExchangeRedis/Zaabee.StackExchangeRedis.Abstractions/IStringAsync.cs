using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface IStringAsync
    {
        Task<bool> AddAsync<T>(string key, T entity, TimeSpan? expiry = null);

        Task AddRangeAsync<T>(Dictionary<string, T> entities, TimeSpan? expiry = null, bool isBatch = false);

        Task<T> GetAsync<T>(string key);

        Task<IList<T>> GetAsync<T>(IEnumerable<string> keys, bool isBatch = false);

        Task<bool> AddAsync(string key, long value, TimeSpan? expiry = null);

        Task<bool> AddAsync(string key, double value, TimeSpan? expiry = null);

        Task<double> IncrementAsync(string key, double value);

        Task<long> IncrementAsync(string key, long value);
    }
}