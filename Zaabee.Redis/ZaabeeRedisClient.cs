using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using Zaabee.Redis.Abstractions;
using Zaabee.Redis.ISerialize;

namespace Zaabee.Redis
{
    public class ZaabeeRedisClient : IZaabeeRedisClient, IDisposable
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _db;
        private readonly ISerializer _serializer;
        private readonly TimeSpan _defaultExpiry;
        private static readonly object LockObj = new object();

        public ZaabeeRedisClient(RedisConfig config, ISerializer serializer)
        {
            if (_conn != null) return;
            lock (LockObj)
            {
                if (_conn != null) return;
                _defaultExpiry = config.DefaultExpiry;
                _serializer = serializer;
                _conn = ConnectionMultiplexer.Connect(config.ConnectionString);
                _db = _conn.GetDatabase();
            }
        }

        #region string

        public void Add<T>(string key, T entity, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (entity == null) return;
            expiry = expiry ?? _defaultExpiry;
            _db.StringSet(key, _serializer.Serialize(entity), expiry);
        }

        public void AddAsync<T>(string key, T entity, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            expiry = expiry ?? _defaultExpiry;
            _db.StringSetAsync(key, _serializer.Serialize(entity), expiry);
        }

        public void AddRange<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null)
        {
            if (entities == null || !entities.Any()) return;
            expiry = expiry ?? _defaultExpiry;
            var batch = _db.CreateBatch();
            foreach (var entity in entities)
                batch.StringSetAsync(entity.Item1, _serializer.Serialize(entity.Item2), expiry);

            batch.Execute();
        }

        public void AddRangeAsync<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null)
        {
            if (entities == null || !entities.Any()) return;
            expiry = expiry ?? _defaultExpiry;
            var batch = _db.CreateBatch();
            foreach (var entity in entities)
                batch.StringSetAsync(entity.Item1, _serializer.Serialize(entity.Item2), expiry);

            batch.ExecuteAsync(null);
        }

        public void Delete(string key)
        {
            _db.KeyDelete(key);
        }

        public void DeleteAsync(string key)
        {
            _db.KeyDeleteAsync(key);
        }

        public void DeleteAll(IList<string> keys)
        {
            _db.KeyDelete(keys.Select(x => (RedisKey) x).ToArray());
        }

        public void DeleteAllAsync(IList<string> keys)
        {
            _db.KeyDeleteAsync(keys.Select(x => (RedisKey) x).ToArray());
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            var value = _db.StringGet(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public Dictionary<string, T> Get<T>(IList<string> keys)
        {
            if (keys == null || !keys.Any()) return new Dictionary<string, T>();
            var keyArr = keys.Select(p => (RedisKey) p).ToArray();
            var valueArr = _db.StringGet(keyArr);
            var result = new Dictionary<string, T>();
            for (var i = 0; i < keyArr.Length; i++)
            {
                var value = valueArr[i];
                if (value.HasValue)
                    result.Add(keyArr[i], _serializer.Deserialize<T>(value));
            }

            return result;
        }

        #endregion

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}