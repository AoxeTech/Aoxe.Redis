using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisDatabase
    {
        public bool Add<T>(string key, T entity, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            expiry ??= _defaultExpiry;
            var bytes = _serializer.Serialize(entity);
            return _db.StringSet(key, bytes, expiry);
        }

        public void AddRange<T>(IDictionary<string, T> entities, TimeSpan? expiry = null, bool isBatch = false)
        {
            if (entities is null || !entities.Any()) return;
            expiry ??= _defaultExpiry;
            if (isBatch)
            {
                var batch = _db.CreateBatch();
                Task.WhenAll(entities.Select(entity =>
                    batch.StringSetAsync(entity.Key, _serializer.Serialize(entity.Value), expiry)));
                batch.Execute();
            }
            else
            {
                foreach (var kv in entities)
                    Add(kv.Key, kv.Value, expiry);
            }
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default;
            var value = _db.StringGet(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default;
        }

        public IList<T> Get<T>(IEnumerable<string> keys, bool isBatch = false)
        {
            if (keys is null || !keys.Any()) return new List<T>();
            List<T> result;
            if (isBatch)
            {
                var values = _db.StringGet(keys.Select(p => (RedisKey) p).ToArray());
                result = values.Select(value => _serializer.Deserialize<T>(value)).ToList();
            }
            else
            {
                result = keys.Select(Get<T>).ToList();
            }

            return result;
        }

        public bool Add(string key, long value, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            expiry ??= _defaultExpiry;
            return _db.StringSet(key, value, expiry);
        }

        public bool Add(string key, double value, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            expiry ??= _defaultExpiry;
            return _db.StringSet(key, value, expiry);
        }

        public double Increment(string key, double value) => _db.StringIncrement(key, value);

        public long Increment(string key, long value) => _db.StringIncrement(key, value);
    }
}