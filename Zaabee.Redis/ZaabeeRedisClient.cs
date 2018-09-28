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

        #region Key

        public bool Delete(string key)
        {
            return _db.KeyDelete(key);
        }

        public long DeleteAll(IEnumerable<string> keys)
        {
            return _db.KeyDelete(keys.Select(x => (RedisKey) x).ToArray());
        }

        public bool Exists(string key)
        {
            return _db.KeyExists(key);
        }

        public bool Expire(string key, TimeSpan? timeSpan)
        {
            return _db.KeyExpire(key, timeSpan);
        }

        #endregion

        #region KeyAsync

        public async Task<bool> DeleteAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        public async Task<long> DeleteAllAsync(IEnumerable<string> keys)
        {
            return await _db.KeyDeleteAsync(keys.Select(x => (RedisKey) x).ToArray());
        }

        public Task<bool> ExistsAsync(string key)
        {
            return _db.KeyExistsAsync(key);
        }

        public Task<bool> ExpireAsync(string key, TimeSpan? timeSpan)
        {
            return _db.KeyExpireAsync(key, timeSpan);
        }

        #endregion

        #region String

        public bool Add<T>(string key, T entity, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            expiry = expiry ?? _defaultExpiry;
            var bytes = _serializer.Serialize(entity);
            return _db.StringSet(key, bytes, expiry);
        }

        public void AddRange<T>(IEnumerable<Tuple<string, T>> entities, TimeSpan? expiry = null)
        {
            if (entities?.Any() == null || !entities.Any()) return;
            expiry = expiry ?? _defaultExpiry;
            var batch = _db.CreateBatch();
            Task.WhenAll(entities.Select(entity =>
                batch.StringSetAsync(entity.Item1, _serializer.Serialize(entity.Item2), expiry)));
            batch.Execute();
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            var value = _db.StringGet(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public IList<T> Get<T>(IEnumerable<string> keys)
        {
            if (keys == null || !keys.Any()) return new List<T>();
            var values = _db.StringGet(keys.Select(p => (RedisKey) p).ToArray());
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        #endregion

        #region StringAsync

        public async Task<bool> AddAsync<T>(string key, T entity, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            expiry = expiry ?? _defaultExpiry;
            var bytes = await _serializer.SerializeAsync(entity);
            return await _db.StringSetAsync(key, bytes, expiry);
        }

        public Task AddRangeAsync<T>(IEnumerable<Tuple<string, T>> entities, TimeSpan? expiry = null)
        {
            if (entities == null || !entities.Any()) return Task.FromResult(0);
            expiry = expiry ?? _defaultExpiry;
            var batch = _db.CreateBatch();
            Task.WhenAll(entities.Select(async entity =>
                await batch.StringSetAsync(entity.Item1, _serializer.Serialize(entity.Item2), expiry)));
            batch.Execute();
            return Task.FromResult(0);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default(T);
            var value = await _db.StringGetAsync(key);
            return await Task.FromResult(value.HasValue ? await _serializer.DeserializeAsync<T>(value) : default(T));
        }

        public async Task<IList<T>> GetAsync<T>(IEnumerable<string> keys)
        {
            if (keys == null || !keys.Any()) return new List<T>();
            var values = await _db.StringGetAsync(keys.Select(p => (RedisKey) p).ToArray());
            var resultTasks = values.Select(async value => await _serializer.DeserializeAsync<T>(value));
            return Task.WhenAll(resultTasks).Result.ToList();
        }

        #endregion

        #region Set

        public bool SetAdd<T>(string key, T value)
        {
            return _db.SetAdd(key, (RedisValue) _serializer.Serialize(value));
        }

        public long SetAddRange<T>(string key, IEnumerable<T> values)
        {
            return _db.SetAdd(key, values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public IList<T> SetCombineUnion<T>(string firstKey, string secondKey)
        {
            var values = _db.SetCombine(SetOperation.Union, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public IList<T> SetCombineUnion<T>(IEnumerable<string> keys)
        {
            var values = _db.SetCombine(SetOperation.Union, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public IList<T> SetCombineIntersect<T>(string firstKey, string secondKey)
        {
            var values = _db.SetCombine(SetOperation.Intersect, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public IList<T> SetCombineIntersect<T>(IEnumerable<string> keys)
        {
            var values = _db.SetCombine(SetOperation.Intersect, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public IList<T> SetCombineDifference<T>(string firstKey, string secondKey)
        {
            var values = _db.SetCombine(SetOperation.Difference, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public IList<T> SetCombineDifference<T>(IEnumerable<string> keys)
        {
            var values = _db.SetCombine(SetOperation.Difference, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public long SetCombineAndStoreUnion<T>(string destination, string firstKey, string secondKey)
        {
            return _db.SetCombineAndStore(SetOperation.Union, destination, firstKey, secondKey);
        }

        public long SetCombineAndStoreUnion<T>(string destination, IEnumerable<string> keys)
        {
            return _db.SetCombineAndStore(SetOperation.Union, destination,
                keys.Select(key => (RedisKey) key).ToArray());
        }

        public long SetCombineAndStoreIntersect<T>(string destination, string firstKey, string secondKey)
        {
            return _db.SetCombineAndStore(SetOperation.Intersect, destination, firstKey, secondKey);
        }

        public long SetCombineAndStoreIntersect<T>(string destination, IEnumerable<string> keys)
        {
            return _db.SetCombineAndStore(SetOperation.Intersect, destination,
                keys.Select(key => (RedisKey) key).ToArray());
        }

        public long SetCombineAndStoreDifference<T>(string destination, string firstKey, string secondKey)
        {
            return _db.SetCombineAndStore(SetOperation.Difference, destination, firstKey, secondKey);
        }

        public long SetCombineAndStoreDifference<T>(string destination, IEnumerable<string> keys)
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

        public IList<T> SetMembers<T>(string key)
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

        public IList<T> SetPop<T>(string key, long count)
        {
            var values = _db.SetPop(key, count);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public T SetRandomMember<T>(string key)
        {
            var value = _db.SetRandomMember(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public IList<T> SetRandomMembers<T>(string key, long count)
        {
            var values = _db.SetRandomMembers(key, count);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public bool SetRemove<T>(string key, T value)
        {
            return _db.SetRemove(key, _serializer.Serialize(value));
        }

        public long SetRemoveRange<T>(string key, IEnumerable<T> values)
        {
            return _db.SetRemove(key, values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public IList<T> SetScan<T>(string key, T pattern = default(T), int pageSize = 10, long cursor = 0,
            int pageOffset = 0)
        {
            var values = _db.SetScan(key, _serializer.Serialize(pattern), pageSize, cursor, pageOffset);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        #endregion

        #region SetAsync

        public async Task<bool> SetAddAsync<T>(string key, T value)
        {
            return await _db.SetAddAsync(key, _serializer.Serialize(value));
        }

        public async Task<long> SetAddRangeAsync<T>(string key, IEnumerable<T> values)
        {
            return await _db.SetAddAsync(key,
                values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public async Task<IList<T>> SetCombineUnionAsync<T>(string firstKey, string secondKey)
        {
            var values = await _db.SetCombineAsync(SetOperation.Union, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public async Task<IList<T>> SetCombineUnionAsync<T>(IEnumerable<string> keys)
        {
            var values = await _db.SetCombineAsync(SetOperation.Union, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public async Task<IList<T>> SetCombineIntersectAsync<T>(string firstKey, string secondKey)
        {
            var values = await _db.SetCombineAsync(SetOperation.Intersect, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public async Task<IList<T>> SetCombineIntersectAsync<T>(IEnumerable<string> keys)
        {
            var values =
                await _db.SetCombineAsync(SetOperation.Intersect, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public async Task<IList<T>> SetCombineDifferenceAsync<T>(string firstKey, string secondKey)
        {
            var values = await _db.SetCombineAsync(SetOperation.Difference, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public async Task<IList<T>> SetCombineDifferenceAsync<T>(IEnumerable<string> keys)
        {
            var values =
                await _db.SetCombineAsync(SetOperation.Difference, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public async Task<long> SetCombineAndStoreUnionAsync<T>(string destination, string firstKey, string secondKey)
        {
            return await _db.SetCombineAndStoreAsync(SetOperation.Union, destination, firstKey, secondKey);
        }

        public async Task<long> SetCombineAndStoreUnionAsync<T>(string destination, IEnumerable<string> keys)
        {
            return await _db.SetCombineAndStoreAsync(SetOperation.Union, destination,
                keys.Select(key => (RedisKey) key).ToArray());
        }

        public async Task<long> SetCombineAndStoreIntersectAsync<T>(string destination, string firstKey,
            string secondKey)
        {
            return await _db.SetCombineAndStoreAsync(SetOperation.Intersect, destination, firstKey, secondKey);
        }

        public async Task<long> SetCombineAndStoreIntersectAsync<T>(string destination, IEnumerable<string> keys)
        {
            return await _db.SetCombineAndStoreAsync(SetOperation.Intersect, destination,
                keys.Select(key => (RedisKey) key).ToArray());
        }

        public async Task<long> SetCombineAndStoreDifferenceAsync<T>(string destination, string firstKey,
            string secondKey)
        {
            return await _db.SetCombineAndStoreAsync(SetOperation.Difference, destination, firstKey, secondKey);
        }

        public async Task<long> SetCombineAndStoreDifferenceAsync<T>(string destination, IEnumerable<string> keys)
        {
            return await _db.SetCombineAndStoreAsync(SetOperation.Difference, destination,
                keys.Select(key => (RedisKey) key).ToArray());
        }

        public async Task<bool> SetContainsAsync<T>(string key, T value)
        {
            return await _db.SetContainsAsync(key, _serializer.Serialize(value));
        }

        public async Task<long> SetLengthAsync<T>(string key)
        {
            return await _db.SetLengthAsync(key);
        }

        public async Task<IList<T>> SetMembersAsync<T>(string key)
        {
            var results = await _db.SetMembersAsync(key);
            return results.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T))
                .ToList();
        }

        public async Task<bool> SetMoveAsync<T>(string source, string destination, T value)
        {
            return await _db.SetMoveAsync(source, destination, _serializer.Serialize(value));
        }

        public async Task<T> SetPopAsync<T>(string key)
        {
            var value = await _db.SetPopAsync(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public async Task<IList<T>> SetPopAsync<T>(string key, long count)
        {
            var values = await _db.SetPopAsync(key, count);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public async Task<T> SetRandomMemberAsync<T>(string key)
        {
            var value = await _db.SetRandomMemberAsync(key);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public async Task<IList<T>> SetRandomMembersAsync<T>(string key, long count)
        {
            var values = await _db.SetRandomMembersAsync(key, count);
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        public async Task<bool> SetRemoveAsync<T>(string key, T value)
        {
            return await _db.SetRemoveAsync(key, (RedisValue) _serializer.Serialize(value));
        }

        public async Task<long> SetRemoveRangeAsync<T>(string key, IEnumerable<T> values)
        {
            return await _db.SetRemoveAsync(key,
                values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public async Task<IList<T>> SetScanAsync<T>(string key, T pattern = default(T), int pageSize = 10,
            long cursor = 0, int pageOffset = 0)
        {
            var values = await Task.Factory.StartNew(() =>
                _db.SetScan(key, _serializer.Serialize(pattern), pageSize, cursor, pageOffset));
            return values.Select(value => value.HasValue ? _serializer.Deserialize<T>(value) : default(T)).ToList();
        }

        #endregion

        #region List

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
            return _db.ListLeftPush(key, (RedisValue) _serializer.Serialize(value));
        }

        public long ListLeftPushRange<T>(string key, IEnumerable<T> values)
        {
            return _db.ListLeftPush(key, values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public long ListLength(string key)
        {
            return _db.ListLength(key);
        }

        public IList<T> ListRange<T>(string key, long start = 0, long stop = -1)
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

        public long ListRightPushRange<T>(string key, IEnumerable<T> values)
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

        #region ListAsync

        public async Task<T> ListGetByIndexAsync<T>(string key, long index)
        {
            return await _serializer.DeserializeAsync<T>(_db.ListGetByIndex(key, index));
        }

        public async Task<long> ListInsertAfterAsync<T>(string key, T pivot, T value)
        {
            return await _db.ListInsertAfterAsync(key, _serializer.Serialize(pivot), _serializer.Serialize(value));
        }

        public async Task<long> ListInsertBeforeAsync<T>(string key, T pivot, T value)
        {
            return await _db.ListInsertBeforeAsync(key, _serializer.Serialize(pivot), _serializer.Serialize(value));
        }

        public async Task<T> ListLeftPopAsync<T>(string key)
        {
            return _serializer.Deserialize<T>(await _db.ListLeftPopAsync(key));
        }

        public async Task<long> ListLeftPushAsync<T>(string key, T value)
        {
            return await _db.ListLeftPushAsync(key, _serializer.Serialize(value));
        }

        public async Task<long> ListLeftPushRangeAsync<T>(string key, IEnumerable<T> values)
        {
            return await _db.ListLeftPushAsync(key,
                values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public async Task<long> ListLengthAsync(string key)
        {
            return await _db.ListLengthAsync(key);
        }

        public async Task<IList<T>> ListRangeAsync<T>(string key, long start = 0, long stop = -1)
        {
            var results = await _db.ListRangeAsync(key, start, stop);
            return results.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public async Task<long> ListRemoveAsync<T>(string key, T value, long count = 0)
        {
            return await _db.ListRemoveAsync(key, _serializer.Serialize(value), count);
        }

        public async Task<T> ListRightPopAsync<T>(string key)
        {
            return _serializer.Deserialize<T>(await _db.ListRightPopAsync(key));
        }

        public async Task<T> ListRightPopLeftPushAsync<T>(string source, string destination)
        {
            return _serializer.Deserialize<T>(await _db.ListRightPopLeftPushAsync(source, destination));
        }

        public async Task<long> ListRightPushAsync<T>(string key, T value)
        {
            return await _db.ListRightPushAsync(key, _serializer.Serialize(value));
        }

        public async Task<long> ListRightPushRangeAsync<T>(string key, IEnumerable<T> values)
        {
            return await _db.ListRightPushAsync(key,
                values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());
        }

        public async Task ListSetByIndexAsync<T>(string key, long index, T value)
        {
            await _db.ListSetByIndexAsync(key, index, _serializer.Serialize(value));
        }

        public async Task ListTrimAsync(string key, long start, long stop)
        {
            await _db.ListTrimAsync(key, start, stop);
        }

        #endregion

        #region Hash

        public bool HashAdd<T>(string key, string entityKey, T entity)
        {
            return _db.HashSet(key, entityKey, _serializer.Serialize(entity));
        }

        public void HashAddRange<T>(string key, IEnumerable<Tuple<string, T>> entities)
        {
            _db.HashSet(key,
                entities.Select(tuple => new HashEntry(tuple.Item1, _serializer.Serialize(tuple.Item2))).ToArray());
        }

        public bool HashDelete(string key, string entityKey)
        {
            return _db.HashDelete(key, entityKey);
        }

        public long HashDeleteRange(string key, IEnumerable<string> entityKeys)
        {
            return _db.HashDelete(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
        }

        public T HashGet<T>(string key, string entityKey)
        {
            var value = _db.HashGet(key, entityKey);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public IList<T> HashGet<T>(string key)
        {
            var kvs = _db.HashGetAll(key);
            return kvs == null
                ? new List<T>()
                : kvs.Select(kv => _serializer.Deserialize<T>(kv.Value)).ToList();
        }

        public IList<T> HashGetRange<T>(string key, IEnumerable<string> entityKeys)
        {
            var values = _db.HashGet(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
            return values == null
                ? new List<T>()
                : values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public IList<string> HashGetAllEntityKeys(string key)
        {
            return _db.HashKeys(key).Select(entityKey => entityKey.ToString()).ToList();
        }

        public long HashCount(string key)
        {
            return _db.HashLength(key);
        }

        #endregion

        #region HashAsync

        public async Task<bool> HashAddAsync<T>(string key, string entityKey, T entity)
        {
            var bytes = await _serializer.SerializeAsync(entity);
            return await _db.HashSetAsync(key, entityKey, bytes);
        }

        public async Task HashAddRangeAsync<T>(string key, IEnumerable<Tuple<string, T>> entities)
        {
            var bytes = entities.Select(async tuple =>
                new HashEntry(tuple.Item1, await _serializer.SerializeAsync(tuple.Item2)));
            await _db.HashSetAsync(key, Task.WhenAll(bytes).Result);
        }

        public async Task<bool> HashDeleteAsync(string key, string entityKey)
        {
            return await _db.HashDeleteAsync(key, entityKey);
        }

        public async Task<long> HashDeleteRangeAsync(string key, IEnumerable<string> entityKeys)
        {
            return await _db.HashDeleteAsync(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
        }

        public async Task<T> HashGetAsync<T>(string key, string entityKey)
        {
            var value = await _db.HashGetAsync(key, entityKey);
            return await Task.FromResult(value.HasValue ? await _serializer.DeserializeAsync<T>(value) : default(T));
        }

        public async Task<IList<T>> HashGetAsync<T>(string key)
        {
            var kvs = await _db.HashGetAllAsync(key);
            var result = kvs.Select(async kv => await _serializer.DeserializeAsync<T>(kv.Value)).ToList();
            return Task.WhenAll(result).Result.ToList();
        }

        public async Task<IList<T>> HashGetRangeAsync<T>(string key, IEnumerable<string> entityKeys)
        {
            var values = await _db.HashGetAsync(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
            return values == null
                ? new List<T>()
                : Task.WhenAll(values.Select(async value => await _serializer.DeserializeAsync<T>(value))).Result
                    .ToList();
        }

        public async Task<IList<string>> HashGetAllEntityKeysAsync(string key)
        {
            var keys = await _db.HashKeysAsync(key);
            return keys.Select(entityKey => entityKey.ToString()).ToList();
        }

        public async Task<long> HashCountAsync(string key)
        {
            return await _db.HashLengthAsync(key);
        }

        #endregion

        #region SortedSet

        public bool SortedSetAdd<T>(string key, T member, long score)
        {
            return _db.SortedSetAdd(key, _serializer.Serialize(member), score);
        }

        public long SortedSetAdd<T>(string key, IEnumerable<Tuple<T, long>> values)
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

        public IList<T> SortedSetRangeByScoreAscending<T>(string key, long start = 0, long stop = -1)
        {
            var values = _db.SortedSetRangeByScore(key, start, stop);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public IList<T> SortedSetRangeByScoreDescending<T>(string key, long start = 0, long stop = -1)
        {
            var values = _db.SortedSetRangeByScore(key, start, stop, order: Order.Descending);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public IList<Tuple<T, double>> SortedSetRangeByScoreWithScoresAscending<T>(string key, long start = 0,
            long stop = -1)
        {
            var values = _db.SortedSetRangeByScoreWithScores(key, start, stop);
            return values.Select(value => new Tuple<T, double>(_serializer.Deserialize<T>(value.Element), value.Score))
                .ToList();
        }

        public IList<Tuple<T, double>> SortedSetRangeByScoreWithScoresDescending<T>(string key, long start = 0,
            long stop = -1)
        {
            var values = _db.SortedSetRangeByScoreWithScores(key, start, stop, order: Order.Descending);
            return values.Select(value => new Tuple<T, double>(_serializer.Deserialize<T>(value.Element), value.Score))
                .ToList();
        }

        public IList<T> SortedSetRangeByValue<T>(string key, T min, T max, long skip, long take = -1)
        {
            var values =
                _db.SortedSetRangeByValue(key, _serializer.Serialize(min), _serializer.Serialize(max), Exclude.None,
                    skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public IList<T> SortedSetRangeByValueAscending<T>(string key, T min = default(T), T max = default(T),
            long skip = 0,
            long take = -1)
        {
            var values = _db.SortedSetRangeByValue(key, _serializer.Serialize(min), _serializer.Serialize(max),
                Exclude.None, Order.Ascending, skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public IList<T> SortedSetRangeByValueDescending<T>(string key, T min = default(T), T max = default(T),
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

        public long SortedSetRemoveRange<T>(string key, IEnumerable<T> members)
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

        public IList<Tuple<T, double>> SortedSetScan<T>(string key, T pattern = default(T), int pageSize = 10,
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

        #region SortedSetAsync

        public async Task<bool> SortedSetAddAsync<T>(string key, T member, long score)
        {
            return await _db.SortedSetAddAsync(key, _serializer.Serialize(member), score);
        }

        public async Task<long> SortedSetAddAsync<T>(string key, IEnumerable<Tuple<T, long>> values)
        {
            return await _db.SortedSetAddAsync(key,
                values.Select(value => new SortedSetEntry(_serializer.Serialize(value.Item1), value.Item2)).ToArray());
        }

        public async Task<double> SortedSetDecrementAsync<T>(string key, T member, long value)
        {
            return await _db.SortedSetDecrementAsync(key, _serializer.Serialize(member), value);
        }

        public async Task<double> SortedSetIncrementAsync<T>(string key, T member, long value)
        {
            return await _db.SortedSetIncrementAsync(key, _serializer.Serialize(member), value);
        }

        public async Task<long> SortedSetLengthAsync<T>(string key)
        {
            return await _db.SortedSetLengthAsync(key);
        }

        public async Task<long> SortedSetLengthByValueAsync<T>(string key, T min, T max)
        {
            return await _db.SortedSetLengthByValueAsync(key, _serializer.Serialize(min), _serializer.Serialize(max));
        }

        public async Task<IList<T>> SortedSetRangeByScoreAscendingAsync<T>(string key, long start = 0, long stop = -1)
        {
            var values = await _db.SortedSetRangeByScoreAsync(key, start, stop);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public async Task<IList<T>> SortedSetRangeByScoreDescendingAsync<T>(string key, long start = 0, long stop = -1)
        {
            var values = await _db.SortedSetRangeByScoreAsync(key, start, stop, order: Order.Descending);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public async Task<IList<Tuple<T, double>>> SortedSetRangeByScoreWithScoresAscendingAsync<T>(string key,
            long start = 0, long stop = -1)
        {
            var values = await _db.SortedSetRangeByScoreWithScoresAsync(key, start, stop);
            return values.Select(value => new Tuple<T, double>(_serializer.Deserialize<T>(value.Element), value.Score))
                .ToList();
        }

        public async Task<IList<Tuple<T, double>>> SortedSetRangeByScoreWithScoresDescendingAsync<T>(string key,
            long start = 0, long stop = -1)
        {
            var values = await _db.SortedSetRangeByScoreWithScoresAsync(key, start, stop, order: Order.Descending);
            return values.Select(value => new Tuple<T, double>(_serializer.Deserialize<T>(value.Element), value.Score))
                .ToList();
        }

        public async Task<IList<T>> SortedSetRangeByValueAsync<T>(string key, T min, T max, long skip, long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, _serializer.Serialize(min),
                _serializer.Serialize(max), Exclude.None, skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public async Task<IList<T>> SortedSetRangeByValueAscendingAsync<T>(string key, T min = default(T),
            T max = default(T), long skip = 0,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, _serializer.Serialize(min),
                _serializer.Serialize(max),
                Exclude.None, Order.Ascending, skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public async Task<IList<T>> SortedSetRangeByValueDescendingAsync<T>(string key, T min = default(T),
            T max = default(T), long skip = 0,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, _serializer.Serialize(min),
                _serializer.Serialize(max),
                Exclude.None, Order.Descending, skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public async Task<bool> SortedSetRemoveAsync<T>(string key, T member)
        {
            return await _db.SortedSetRemoveAsync(key, _serializer.Serialize(member));
        }

        public async Task<long> SortedSetRemoveRangeAsync<T>(string key, IEnumerable<T> members)
        {
            return await _db.SortedSetRemoveAsync(key,
                members.Select(member => (RedisValue) _serializer.Serialize(member)).ToArray());
        }

        public async Task<long> SortedSetRemoveRangeByScoreAsync<T>(string key, long start, long stop)
        {
            return await _db.SortedSetRemoveRangeByScoreAsync(key, start, stop);
        }

        public async Task<long> SortedSetRemoveRangeByValueAsync<T>(string key, T min, T max)
        {
            return await _db.SortedSetRemoveRangeByValueAsync(key, _serializer.Serialize(min),
                _serializer.Serialize(max));
        }

        public async Task<IList<Tuple<T, double>>> SortedSetScanAsync<T>(string key, T pattern = default(T),
            int pageSize = 10, long cursor = 0, int pageOffset = 0)
        {
            var values = await Task.Factory.StartNew(() =>
                _db.SortedSetScan(key, _serializer.Serialize(pattern), pageSize, cursor, pageOffset));
            return values.Select(value => new Tuple<T, double>(_serializer.Deserialize<T>(value.Element), value.Score))
                .ToList();
        }

        public async Task<double?> SortedSetScoreAsync<T>(string key, T member)
        {
            return await _db.SortedSetScoreAsync(key, _serializer.Serialize(member));
        }

        #endregion

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}