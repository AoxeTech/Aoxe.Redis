using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient
    {
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
    }
}