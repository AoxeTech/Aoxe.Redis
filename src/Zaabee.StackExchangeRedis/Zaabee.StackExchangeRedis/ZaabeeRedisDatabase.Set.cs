using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisDatabase
    {
        public bool SetAdd<T>(string key, T value) => _db.SetAdd(key, (RedisValue) _serializer.SerializeToBytes(value));

        public long SetAddRange<T>(string key, IEnumerable<T> values) => _db.SetAdd(key,
            values.Select(value => (RedisValue) _serializer.SerializeToBytes(value)).ToArray());

        public IList<T> SetCombineUnion<T>(string firstKey, string secondKey)
        {
            var values = _db.SetCombine(SetOperation.Union, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default).ToList();
        }

        public IList<T> SetCombineUnion<T>(IEnumerable<string> keys)
        {
            var values = _db.SetCombine(SetOperation.Union, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default).ToList();
        }

        public IList<T> SetCombineIntersect<T>(string firstKey, string secondKey)
        {
            var values = _db.SetCombine(SetOperation.Intersect, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default).ToList();
        }

        public IList<T> SetCombineIntersect<T>(IEnumerable<string> keys)
        {
            var values = _db.SetCombine(SetOperation.Intersect, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default).ToList();
        }

        public IList<T> SetCombineDifference<T>(string firstKey, string secondKey)
        {
            var values = _db.SetCombine(SetOperation.Difference, firstKey, secondKey);
            return values.Select(value => value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default).ToList();
        }

        public IList<T> SetCombineDifference<T>(IEnumerable<string> keys)
        {
            var values = _db.SetCombine(SetOperation.Difference, keys.Select(key => (RedisKey) key).ToArray());
            return values.Select(value => value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default).ToList();
        }

        public long SetCombineAndStoreUnion<T>(string destination, string firstKey, string secondKey) =>
            _db.SetCombineAndStore(SetOperation.Union, destination, firstKey, secondKey);

        public long SetCombineAndStoreUnion<T>(string destination, IEnumerable<string> keys) =>
            _db.SetCombineAndStore(SetOperation.Union, destination, keys.Select(key => (RedisKey) key).ToArray());

        public long SetCombineAndStoreIntersect<T>(string destination, string firstKey, string secondKey) =>
            _db.SetCombineAndStore(SetOperation.Intersect, destination, firstKey, secondKey);

        public long SetCombineAndStoreIntersect<T>(string destination, IEnumerable<string> keys) =>
            _db.SetCombineAndStore(SetOperation.Intersect, destination, keys.Select(key => (RedisKey) key).ToArray());

        public long SetCombineAndStoreDifference<T>(string destination, string firstKey, string secondKey) =>
            _db.SetCombineAndStore(SetOperation.Difference, destination, firstKey, secondKey);

        public long SetCombineAndStoreDifference<T>(string destination, IEnumerable<string> keys) =>
            _db.SetCombineAndStore(SetOperation.Difference, destination, keys.Select(key => (RedisKey) key).ToArray());

        public bool SetContains<T>(string key, T value) => _db.SetContains(key, _serializer.SerializeToBytes(value));

        public long SetLength<T>(string key) => _db.SetLength(key);

        public IList<T> SetMembers<T>(string key) => _db.SetMembers(key)
            .Select(value => value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default).ToList();

        public bool SetMove<T>(string source, string destination, T value) =>
            _db.SetMove(source, destination, _serializer.SerializeToBytes(value));

        public T SetPop<T>(string key)
        {
            var value = _db.SetPop(key);
            return value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default;
        }

        public IList<T> SetPop<T>(string key, long count)
        {
            var values = _db.SetPop(key, count);
            return values.Select(value => value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default).ToList();
        }

        public T SetRandomMember<T>(string key)
        {
            var value = _db.SetRandomMember(key);
            return value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default;
        }

        public IList<T> SetRandomMembers<T>(string key, long count)
        {
            var values = _db.SetRandomMembers(key, count);
            return values.Select(value => value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default).ToList();
        }

        public bool SetRemove<T>(string key, T value) => _db.SetRemove(key, _serializer.SerializeToBytes(value));

        public long SetRemoveRange<T>(string key, IEnumerable<T> values) => _db.SetRemove(key,
            values.Select(value => (RedisValue) _serializer.SerializeToBytes(value)).ToArray());

        public IList<T> SetScan<T>(string key, T pattern = default, int pageSize = 10, long cursor = 0,
            int pageOffset = 0)
        {
            var values = _db.SetScan(key, _serializer.SerializeToBytes(pattern), pageSize, cursor, pageOffset);
            return values.Select(value => value.HasValue ? _serializer.DeserializeFromBytes<T>(value) : default).ToList();
        }
    }
}