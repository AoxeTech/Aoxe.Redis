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

        public async Task AddRangeAsync<T>(IEnumerable<Tuple<string, T>> entities, TimeSpan? expiry = null, bool isBatch = false)
        {
            if (entities == null || !entities.Any()) return;
            expiry = expiry ?? _defaultExpiry;
            if (isBatch)
            {
                var batch = _db.CreateBatch();
                foreach (var (key, entity) in entities)
                    await batch.StringSetAsync(key, _serializer.Serialize(entity), expiry);
                batch.Execute();
            }
            else
            {
                foreach (var (key, entity) in entities)
                    await AddAsync(key, entity, expiry);
            }
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            var value = await _db.StringGetAsync(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public async Task<IList<T>> GetAsync<T>(IEnumerable<string> keys, bool isBatch = false)
        {
            if (keys is null || !keys.Any()) return new List<T>();
            List<T> result;
            if (isBatch)
            {
                var values = await _db.StringGetAsync(keys.Select(p => (RedisKey) p).ToArray());
                result = values.Select(value => _serializer.Deserialize<T>(value)).ToList();
            }
            else
            {
                result = new List<T>();
                foreach (var key in keys) result.Add(await GetAsync<T>(key));
            }

            return result;
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