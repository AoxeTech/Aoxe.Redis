using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient
    {
        public async Task<bool> AddAsync<T>(string key, T entity, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            expiry = expiry ?? _defaultExpiry;
            var bytes = _serializer.Serialize(entity);
            return await _db.StringSetAsync(key, bytes, expiry);
        }

        public Task AddRangeAsync<T>(IEnumerable<Tuple<string, T>> entities, TimeSpan? expiry = null)
        {
            if (entities == null || !entities.Any()) return Task.CompletedTask;
            expiry = expiry ?? _defaultExpiry;
            var batch = _db.CreateBatch();
            Task.WhenAll(entities.Select(async entity =>
                await batch.StringSetAsync(entity.Item1, _serializer.Serialize(entity.Item2), expiry)));
            batch.Execute();
            return Task.CompletedTask;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            var value = await _db.StringGetAsync(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public async Task<IList<T>> GetAsync<T>(IEnumerable<string> keys)
        {
            if (keys == null || !keys.Any()) return new List<T>();
            var values = await _db.StringGetAsync(keys.Select(p => (RedisKey) p).ToArray());
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public async Task<bool> AddAsync(string key, long value, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            expiry = expiry ?? _defaultExpiry;
            return await _db.StringSetAsync(key, value, expiry);
        }

        public async Task<bool> AddAsync(string key, double value, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            expiry = expiry ?? _defaultExpiry;
            return await _db.StringSetAsync(key, value, expiry);
        }

        public async Task<double> IncrementAsync(string key, double value)
        {
            return await _db.StringIncrementAsync(key, value);
        }

        public async Task<long> IncrementAsync(string key, long value)
        {
            return await _db.StringIncrementAsync(key, value);
        }
    }
}