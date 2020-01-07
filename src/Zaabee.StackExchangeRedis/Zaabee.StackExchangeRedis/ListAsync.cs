using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient
    {
        public async Task<T> ListGetByIndexAsync<T>(string key, long index) =>
            _serializer.Deserialize<T>(await _db.ListGetByIndexAsync(key, index));

        public async Task<long> ListInsertAfterAsync<T>(string key, T pivot, T value) =>
            await _db.ListInsertAfterAsync(key, _serializer.Serialize(pivot), _serializer.Serialize(value));

        public async Task<long> ListInsertBeforeAsync<T>(string key, T pivot, T value) =>
            await _db.ListInsertBeforeAsync(key, _serializer.Serialize(pivot), _serializer.Serialize(value));

        public async Task<T> ListLeftPopAsync<T>(string key) =>
            _serializer.Deserialize<T>(await _db.ListLeftPopAsync(key));

        public async Task<long> ListLeftPushAsync<T>(string key, T value) =>
            await _db.ListLeftPushAsync(key, _serializer.Serialize(value));

        public async Task<long> ListLeftPushRangeAsync<T>(string key, IEnumerable<T> values) =>
            await _db.ListLeftPushAsync(key,
                values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());

        public async Task<long> ListLengthAsync(string key) => await _db.ListLengthAsync(key);

        public async Task<IList<T>> ListRangeAsync<T>(string key, long start = 0, long stop = -1)
        {
            var results = await _db.ListRangeAsync(key, start, stop);
            return results.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public async Task<long> ListRemoveAsync<T>(string key, T value, long count = 0) =>
            await _db.ListRemoveAsync(key, _serializer.Serialize(value), count);

        public async Task<T> ListRightPopAsync<T>(string key) =>
            _serializer.Deserialize<T>(await _db.ListRightPopAsync(key));

        public async Task<T> ListRightPopLeftPushAsync<T>(string source, string destination) =>
            _serializer.Deserialize<T>(await _db.ListRightPopLeftPushAsync(source, destination));

        public async Task<long> ListRightPushAsync<T>(string key, T value) =>
            await _db.ListRightPushAsync(key, _serializer.Serialize(value));

        public async Task<long> ListRightPushRangeAsync<T>(string key, IEnumerable<T> values) =>
            await _db.ListRightPushAsync(key,
                values.Select(value => (RedisValue) _serializer.Serialize(value)).ToArray());

        public async Task ListSetByIndexAsync<T>(string key, long index, T value) =>
            await _db.ListSetByIndexAsync(key, index, _serializer.Serialize(value));

        public async Task ListTrimAsync(string key, long start, long stop) => await _db.ListTrimAsync(key, start, stop);
    }
}