using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisDatabase
    {
        public bool HashAdd<T>(string key, string entityKey, T entity) =>
            _db.HashSet(key, entityKey, _serializer.Serialize(entity));

        public void HashAddRange<T>(string key, IDictionary<string, T> entities)
        {
            var bytes = entities.Select(kv =>
                new HashEntry(kv.Key, _serializer.Serialize(kv.Value))).ToArray();
            _db.HashSet(key, bytes);
        }

        public bool HashDelete(string key, string entityKey) => _db.HashDelete(key, entityKey);

        public long HashDeleteRange(string key, IEnumerable<string> entityKeys) =>
            _db.HashDelete(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());

        public T HashGet<T>(string key, string entityKey)
        {
            var value = _db.HashGet(key, entityKey);
            return value.HasValue ? _serializer.Deserialize<T>(value) : default;
        }

        public IList<T> HashGet<T>(string key)
        {
            var kvs = _db.HashGetAll(key);
            return kvs?.Select(kv => _serializer.Deserialize<T>(kv.Value)).ToList() ?? new List<T>();
        }

        public IList<T> HashGetRange<T>(string key, IEnumerable<string> entityKeys)
        {
            var values = _db.HashGet(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
            return values?.Select(value => _serializer.Deserialize<T>(value)).ToList() ?? new List<T>();
        }

        public IList<string> HashGetAllEntityKeys(string key) =>
            _db.HashKeys(key).Select(entityKey => entityKey.ToString()).ToList();

        public long HashCount(string key) => _db.HashLength(key);
    }
}