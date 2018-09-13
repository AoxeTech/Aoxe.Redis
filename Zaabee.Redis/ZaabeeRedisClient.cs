using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        #region key

        public bool Delete(string key)
        {
            return _db.KeyDelete(key);
        }

        public async Task<bool> DeleteAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        public long DeleteAll(IList<string> keys)
        {
            return _db.KeyDelete(keys.Select(x => (RedisKey) x).ToArray());
        }

        public async Task<long> DeleteAllAsync(IList<string> keys)
        {
            return await _db.KeyDeleteAsync(keys.Select(x => (RedisKey) x).ToArray());
        }

        #endregion

        #region string

        public bool Add<T>(string key, T entity, TimeSpan? expiry = null)
        {
            return AddAsync(key, entity, expiry).Result;
        }

        public async Task<bool> AddAsync<T>(string key, T entity, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            expiry = expiry ?? _defaultExpiry;
            return await _db.StringSetAsync(key, _serializer.Serialize(entity), expiry);
        }

        public void AddRange<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null)
        {
            if (entities == null || !entities.Any()) return;
            expiry = expiry ?? _defaultExpiry;
            var batch = _db.CreateBatch();
            Task.WhenAll(entities.Select(entity =>
                batch.StringSetAsync(entity.Item1, _serializer.Serialize(entity.Item2), expiry)));

            batch.Execute();
        }

        public Task AddRangeAsync<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null)
        {
            if (entities == null || !entities.Any()) return Task.FromResult(0);
            expiry = expiry ?? _defaultExpiry;
            var batch = _db.CreateBatch();
            Task.WhenAll(entities.Select(entity =>
                batch.StringSetAsync(entity.Item1, _serializer.Serialize(entity.Item2), expiry)));
            batch.Execute();
            return Task.FromResult(0);
        }

        public T Get<T>(string key)
        {
            return GetAsync<T>(key).Result;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return await Task.FromResult(default(T));
            var value = _db.StringGetAsync(key);
            var result = value.Result;
            return await Task.FromResult(result.HasValue ? _serializer.Deserialize<T>(result) : default(T));
        }

        public List<T> Get<T>(IList<string> keys)
        {
            return GetAsync<T>(keys).Result;
        }

        public async Task<List<T>> GetAsync<T>(IList<string> keys)
        {
            if (keys == null || !keys.Any()) return await Task.FromResult(new List<T>());
            var values = await _db.StringGetAsync(keys.Select(p => (RedisKey) p).ToArray());
            var result = values.Select(value => _serializer.Deserialize<T>(value)).ToList();
            return await Task.FromResult(result);
        }

        #endregion

        #region hash

        public bool HashAdd<T>(string key, string entityKey, T entity)
        {
            return _db.HashSet(key, entityKey, _serializer.Serialize(entity));
        }

        public void HashAddRange<T>(string key, Dictionary<string, T> entities)
        {
            _db.HashSet(key, entities.Select(kv => new HashEntry(kv.Key, _serializer.Serialize(kv.Value))).ToArray());
        }

        public bool HashDelete(string key, string entityKey)
        {
            return _db.HashDelete(key, entityKey);
        }

        public long HashDeleteAll(string key, IList<string> entityKeys)
        {
            return _db.HashDelete(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
        }

        public T HashGet<T>(string key, string entityKey)
        {
            var value = _db.HashGet(key, entityKey);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public List<T> HashGetAll<T>(string key)
        {
            var kvs = _db.HashGetAll(key);
            return kvs == null
                ? new List<T>()
                : kvs.Select(kv => _serializer.Deserialize<T>(kv.Value)).ToList();
        }

        public List<T> HashGet<T>(string key, IList<string> entityKeys)
        {
            var values = _db.HashGet(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
            return values == null
                ? new List<T>()
                : values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public List<string> HashGetAllEntityKeys(string key)
        {
            return _db.HashKeys(key).Select(entityKey => entityKey.ToString()).ToList();
        }

        public List<T> HashGetAllEntities<T>(string key)
        {
            var kvs = _db.HashGetAll(key);
            return kvs == null
                ? new List<T>()
                : kvs.Select(kv => _serializer.Deserialize<T>(kv.Value)).ToList();
        }

        public long HashCount(string key)
        {
            return _db.HashLength(key);
        }

        #endregion

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}