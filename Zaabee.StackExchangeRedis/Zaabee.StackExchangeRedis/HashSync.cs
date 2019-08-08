using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient
    {
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
    }
}