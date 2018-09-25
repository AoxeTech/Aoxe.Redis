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

        public bool Exists(string key)
        {
            return _db.KeyExists(key);
        }

        public Task<bool> ExistsAsync(string key)
        {
            return _db.KeyExistsAsync(key);
        }

        public bool Expire(string key, TimeSpan? timeSpan)
        {
            return _db.KeyExpire(key, timeSpan);
        }

        public Task<bool> ExpireAsync(string key, TimeSpan? timeSpan)
        {
            return _db.KeyExpireAsync(key, timeSpan);
        }

        #endregion

        #region string

        public bool Add<T>(string key, T entity, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            expiry = expiry ?? _defaultExpiry;
            var bytes = _serializer.Serialize(entity);
            return _db.StringSet(key, bytes, expiry);
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
            Task.WhenAll(entities.Select(async entity =>
                await batch.StringSetAsync(entity.Item1, _serializer.Serialize(entity.Item2), expiry)));
            batch.Execute();
            return Task.FromResult(0);
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            var value = _db.StringGet(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            var value = await _db.StringGetAsync(key);
            return await Task.FromResult(value.HasValue ? await _serializer.DeserializeAsync<T>(value) : default(T));
        }

        public List<T> Get<T>(IList<string> keys)
        {
            if (keys == null || !keys.Any()) return new List<T>();
            var values = _db.StringGet(keys.Select(p => (RedisKey) p).ToArray());
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public async Task<List<T>> GetAsync<T>(IList<string> keys)
        {
            if (keys == null || !keys.Any()) return new List<T>();
            var values = await _db.StringGetAsync(keys.Select(p => (RedisKey) p).ToArray());
            var resultTasks = values.Select(async value => await _serializer.DeserializeAsync<T>(value));
            return Task.WhenAll(resultTasks).Result.ToList();
        }

        #endregion

        #region set

        public bool SetAdd<T>(string key, T value)
        {
            return _db.SetAdd(key, _serializer.Serialize(value));
        }

        public long SetAdd<T>(string key, List<T> values)
        {
            return _db.SetAdd(key, values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public List<T> SetCombineUnion<T>(string firstKey, string secondKey)
        {
            var values = _db.SetCombine(SetOperation.Union, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public List<T> SetCombineUnion<T>(string[] keys)
        {
            var values = _db.SetCombine(SetOperation.Union, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public List<T> SetCombineIntersect<T>(string firstKey, string secondKey)
        {
            var values = _db.SetCombine(SetOperation.Intersect, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public List<T> SetCombineIntersect<T>(string[] keys)
        {
            var values = _db.SetCombine(SetOperation.Intersect, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public List<T> SetCombineDifference<T>(string firstKey, string secondKey)
        {
            var values = _db.SetCombine(SetOperation.Difference, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public List<T> SetCombineDifference<T>(string[] keys)
        {
            var values = _db.SetCombine(SetOperation.Difference, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public long SetCombineAndStoreUnion<T>(string destination, string firstKey, string secondKey)
        {
            return _db.SetCombineAndStore(SetOperation.Union, destination, firstKey, secondKey);
        }

        public long SetCombineAndStoreUnion<T>(string destination, string[] keys)
        {
            return _db.SetCombineAndStore(SetOperation.Union, destination,
                keys.Select(key => (RedisKey) key).ToArray());
        }

        public long SetCombineAndStoreIntersect<T>(string destination, string firstKey, string secondKey)
        {
            return _db.SetCombineAndStore(SetOperation.Intersect, destination, firstKey, secondKey);
        }

        public long SetCombineAndStoreIntersect<T>(string destination, string[] keys)
        {
            return _db.SetCombineAndStore(SetOperation.Intersect, destination,
                keys.Select(key => (RedisKey) key).ToArray());
        }

        public long SetCombineAndStoreDifference<T>(string destination, string firstKey, string secondKey)
        {
            return _db.SetCombineAndStore(SetOperation.Difference, destination, firstKey, secondKey);
        }

        public long SetCombineAndStoreDifference<T>(string destination, string[] keys)
        {
            return _db.SetCombineAndStore(SetOperation.Difference, destination,
                keys.Select(key => (RedisKey) key).ToArray());
        }

        public bool SetContains<T>(string key, T value)
        {
            return _db.SetContains(key, _serializer.Serialize(value));
        }

        public long SetLength<T>(string key)
        {
            return _db.SetLength(key);
        }

        public List<T> SetMembers<T>(string key)
        {
            return _db.SetMembers(key).Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T))
                .ToList();
        }

        public bool SetMove<T>(string source, string destination, T value)
        {
            return _db.SetMove(source, destination, _serializer.Serialize(value));
        }

        public T SetPop<T>(string key)
        {
            var value = _db.SetPop(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public List<T> SetPop<T>(string key, long count)
        {
            var values = _db.SetPop(key, count);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public T SetRandomMember<T>(string key)
        {
            var value = _db.SetRandomMember(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public List<T> SetRandomMembers<T>(string key, long count)
        {
            var values = _db.SetRandomMembers(key, count);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public bool SetRemove<T>(string key, T value)
        {
            return _db.SetRemove(key, _serializer.Serialize(value));
        }

        public long SetRemove<T>(string key, List<T> values)
        {
            return _db.SetRemove(key, values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public IEnumerable<T> SetScan<T>(string key, T pattern = default(T), int pageSize = 10, long cursor = 0,
            int pageOffset = 0)
        {
            var values = _db.SetScan(key, _serializer.Serialize(pattern), pageSize, cursor, pageOffset);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        #endregion

        #region list

        public T ListGetByIndex<T>(string key, long index)
        {
            return _serializer.Deserialize<T>(_db.ListGetByIndex(key, index));
        }

        public long ListInsertAfter<T>(string key, T pivot, T value)
        {
            return _db.ListInsertAfter(key, _serializer.Serialize(pivot), _serializer.Serialize(value));
        }

        public long ListInsertBefore<T>(string key, T pivot, T value)
        {
            return _db.ListInsertBefore(key, _serializer.Serialize(pivot), _serializer.Serialize(value));
        }

        public T ListLeftPop<T>(string key)
        {
            return _serializer.Deserialize<T>(_db.ListLeftPop(key));
        }

        public long ListLeftPush<T>(string key, T value)
        {
            return _db.ListLeftPush(key, _serializer.Serialize(value));
        }

        public long ListLeftPush<T>(string key, List<T> values)
        {
            return _db.ListLeftPush(key, values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public long ListLength(string key)
        {
            return _db.ListLength(key);
        }

        public List<T> ListRange<T>(string key, long start = 0, long stop = -1)
        {
            return _db.ListRange(key, start, stop).Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public long ListRemove<T>(string key, T value, long count = 0)
        {
            return _db.ListRemove(key, _serializer.Serialize(value), count);
        }

        public T ListRightPop<T>(string key)
        {
            return _serializer.Deserialize<T>(_db.ListRightPop(key));
        }

        public T ListRightPopLeftPush<T>(string source, string destination)
        {
            return _serializer.Deserialize<T>(_db.ListRightPopLeftPush(source, destination));
        }

        public long ListRightPush<T>(string key, T value)
        {
            return _db.ListRightPush(key, _serializer.Serialize(value));
        }

        public long ListRightPush<T>(string key, List<T> values)
        {
            return _db.ListRightPush(key, values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public void ListSetByIndex<T>(string key, long index, T value)
        {
            _db.ListSetByIndex(key, index, _serializer.Serialize(value));
        }

        public void ListTrim(string key, long start, long stop)
        {
            _db.ListTrim(key, start, stop);
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
            return await _db.HashSetAsync(key, entityKey, bytes);
        }

        public void HashAddRange<T>(string key, IList<Tuple<string, T>> entities)
        {
            _db.HashSet(key,
                entities.Select(tuple => new HashEntry(tuple.Item1, _serializer.Serialize(tuple.Item2))).ToArray());
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

        #region sorted set

        public bool SortedSetAdd<T>(string key, T member, long score)
        {
            return _db.SortedSetAdd(key, _serializer.Serialize(member), score);
        }

        public long SortedSetAdd<T>(string key, List<Tuple<T, long>> values)
        {
            return _db.SortedSetAdd(key,
                values.Select(value => new SortedSetEntry(_serializer.Serialize(value.Item1), value.Item2)).ToArray());
        }

        public double SortedSetDecrement<T>(string key, T member, long value)
        {
            return _db.SortedSetDecrement(key, _serializer.Serialize(member), value);
        }

        public double SortedSetIncrement<T>(string key, T member, long value)
        {
            return _db.SortedSetIncrement(key, _serializer.Serialize(member), value);
        }

        public long SortedSetLength<T>(string key)
        {
            return _db.SortedSetLength(key);
        }

        public long SortedSetLengthByValue<T>(string key, T min, T max)
        {
            return _db.SortedSetLengthByValue(key, _serializer.Serialize(min), _serializer.Serialize(max));
        }

        public List<T> SortedSetRangeByScoreAscending<T>(string key, long start = 0, long stop = -1)
        {
            var values = _db.SortedSetRangeByScore(key, start, stop);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public List<T> SortedSetRangeByScoreDescending<T>(string key, long start = 0, long stop = -1)
        {
            var values = _db.SortedSetRangeByScore(key, start, stop, order: Order.Descending);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public List<Tuple<T, double>> SortedSetRangeByScoreWithScoresAscending<T>(string key, long start = 0,
            long stop = -1)
        {
            var values = _db.SortedSetRangeByScoreWithScores(key, start, stop);
            return values.Select(value => new Tuple<T, double>(_serializer.Deserialize<T>(value.Element), value.Score))
                .ToList();
        }

        public List<Tuple<T, double>> SortedSetRangeByScoreWithScoresDescending<T>(string key, long start = 0,
            long stop = -1)
        {
            var values = _db.SortedSetRangeByScoreWithScores(key, start, stop, order: Order.Descending);
            return values.Select(value => new Tuple<T, double>(_serializer.Deserialize<T>(value.Element), value.Score))
                .ToList();
        }

        public List<T> SortedSetRangeByValue<T>(string key, T min, T max, long skip, long take = -1)
        {
            var values =
                _db.SortedSetRangeByValue(key, _serializer.Serialize(min), _serializer.Serialize(max), Exclude.None,
                    skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public List<T> SortedSetRangeByValueAscending<T>(string key, T min = default(T), T max = default(T),
            long skip = 0,
            long take = -1)
        {
            var values = _db.SortedSetRangeByValue(key, _serializer.Serialize(min), _serializer.Serialize(max),
                Exclude.None, Order.Ascending, skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public List<T> SortedSetRangeByValueDescending<T>(string key, T min = default(T), T max = default(T),
            long skip = 0,
            long take = -1)
        {
            var values = _db.SortedSetRangeByValue(key, _serializer.Serialize(min), _serializer.Serialize(max),
                Exclude.None, Order.Descending, skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public bool SortedSetRemove<T>(string key, T member)
        {
            return _db.SortedSetRemove(key, _serializer.Serialize(member));
        }

        public long SortedSetRemove<T>(string key, List<T> members)
        {
            return _db.SortedSetRemove(key,
                members.Select(member => (RedisValue) _serializer.Serialize(member)).ToArray());
        }

        public long SortedSetRemoveRangeByScore<T>(string key, long start, long stop)
        {
            return _db.SortedSetRemoveRangeByScore(key, start, stop);
        }

        public long SortedSetRemoveRangeByValue<T>(string key, T min, T max)
        {
            return _db.SortedSetRemoveRangeByValue(key, _serializer.Serialize(min), _serializer.Serialize(max));
        }

        public List<Tuple<T, double>> SortedSetScan<T>(string key, T pattern = default(T), int pageSize = 10,
            long cursor = 0, int pageOffset = 0)
        {
            var values = _db.SortedSetScan(key, _serializer.Serialize(pattern), pageSize, cursor, pageOffset);
            return values.Select(value => new Tuple<T, double>(_serializer.Deserialize<T>(value.Element), value.Score))
                .ToList();
        }

        public double? SortedSetScore<T>(string key, T member)
        {
            return _db.SortedSetScore(key, _serializer.Serialize(member));
        }

        #endregion

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}