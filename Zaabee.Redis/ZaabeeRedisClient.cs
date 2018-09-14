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
            var bytes = await _serializer.SerializeAsync(entity);
            return await _db.StringSetAsync(key, bytes, expiry);
        }

        public void AddRange<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null)
        {
            AddRangeAsync(entities, expiry).Wait();
        }

        public Task AddRangeAsync<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null)
        {
            if (entities == null || !entities.Any()) return Task.FromResult(0);
            expiry = expiry ?? _defaultExpiry;
            var batch = _db.CreateBatch();
            Task.WhenAll(entities.Select(async entity =>
                await batch.StringSetAsync(entity.Item1, _serializer.Serialize(entity.Item2), expiry)));
            batch.Execute();
            return Task.FromResult(0);
        }

        public T Get<T>(string key)
        {
            return GetAsync<T>(key).Result;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            var value = await _db.StringGetAsync(key);
            return await Task.FromResult(value.HasValue ? await _serializer.DeserializeAsync<T>(value) : default(T));
        }

        public List<T> Get<T>(IList<string> keys)
        {
            return GetAsync<T>(keys).Result;
        }

        public async Task<List<T>> GetAsync<T>(IList<string> keys)
        {
            if (keys == null || !keys.Any()) return new List<T>();
            var values = await _db.StringGetAsync(keys.Select(p => (RedisKey) p).ToArray());
            var resultTasks = values.Select(async value => await _serializer.DeserializeAsync<T>(value));
            return Task.WhenAll(resultTasks).Result.ToList();
        }

        #endregion

        #region hash

        public bool HashAdd<T>(string key, string entityKey, T entity)
        {
            return _db.HashSet(key, entityKey, _serializer.Serialize(entity));
        }

        public async Task<bool> HashAddAsync<T>(string key, string entityKey, T entity)
        {
            var bytes = await _serializer.SerializeAsync(entity);
            return await _db.HashSetAsync(key, entityKey,  bytes);
        }

        public void HashAddRange<T>(string key, IList<Tuple<string, T>> entities)
        {
            _db.HashSet(key, entities.Select(tuple => new HashEntry(tuple.Item1, _serializer.Serialize(tuple.Item2))).ToArray());
        }

        public async Task HashAddRangeAsync<T>(string key, IList<Tuple<string, T>> entities)
        {
            var bytes = entities.Select(async tuple =>
                new HashEntry(tuple.Item1, await _serializer.SerializeAsync(tuple.Item2)));
            await _db.HashSetAsync(key, Task.WhenAll(bytes).Result);
        }

        public bool HashDelete(string key, string entityKey)
        {
            return _db.HashDelete(key, entityKey);
        }

        public async Task<bool> HashDeleteAsync(string key, string entityKey)
        {
            return await _db.HashDeleteAsync(key, entityKey);
        }

        public long HashDelete(string key, IList<string> entityKeys)
        {
            return _db.HashDelete(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
        }

        public async Task<long> HashDeleteAsync(string key, IList<string> entityKeys)
        {
            return await _db.HashDeleteAsync(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
        }

        public T HashGet<T>(string key, string entityKey)
        {
            var value = _db.HashGet(key, entityKey);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public async Task<T> HashGetAsync<T>(string key, string entityKey)
        {
            var value = await _db.HashGetAsync(key, entityKey);
            return await Task.FromResult(value.HasValue ? await _serializer.DeserializeAsync<T>(value) : default(T));
        }

        public List<T> HashGet<T>(string key)
        {
            var kvs = _db.HashGetAll(key);
            return kvs == null
                ? new List<T>()
                : kvs.Select(kv => _serializer.Deserialize<T>(kv.Value)).ToList();
        }

        public async Task<List<T>> HashGetAsync<T>(string key)
        {
            var kvs = await _db.HashGetAllAsync(key);
            var result = kvs.Select(async kv => await _serializer.DeserializeAsync<T>(kv.Value)).ToList();
            return Task.WhenAll(result).Result.ToList();
        }

        public List<T> HashGet<T>(string key, IList<string> entityKeys)
        {
            var values = _db.HashGet(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
            return values == null
                ? new List<T>()
                : values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public async Task<List<T>> HashGetAsync<T>(string key, IList<string> entityKeys)
        {
            var values = await _db.HashGetAsync(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
            return values == null
                ? new List<T>()
                : Task.WhenAll(values.Select(async value => await _serializer.DeserializeAsync<T>(value))).Result
                    .ToList();
        }

        public List<string> HashGetAllEntityKeys(string key)
        {
            return _db.HashKeys(key).Select(entityKey => entityKey.ToString()).ToList();
        }

        public async Task<List<string>> HashGetAllEntityKeysAsync(string key)
        {
            var keys = await _db.HashKeysAsync(key);
            return keys.Select(entityKey => entityKey.ToString()).ToList();
        }

        public long HashCount(string key)
        {
            return _db.HashLength(key);
        }

        public async Task<long> HashCountAsync(string key)
        {
            return await _db.HashLengthAsync(key);
        }

        #endregion

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}