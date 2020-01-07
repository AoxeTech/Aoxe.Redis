using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient
    {
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
    }
}