using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisDatabase
    {
        public async Task<bool> SortedSetAddAsync<T>(string key, T member, double score) =>
            await _db.SortedSetAddAsync(key, _serializer.SerializeToBytes(member), score);

        public async Task<long> SortedSetAddAsync<T>(string key, IDictionary<T, double> values) =>
            await _db.SortedSetAddAsync(key,
                values.Select(value => new SortedSetEntry(_serializer.SerializeToBytes(value.Key), value.Value)).ToArray());

        public async Task<double> SortedSetDecrementAsync<T>(string key, T member, double value) =>
            await _db.SortedSetDecrementAsync(key, _serializer.SerializeToBytes(member), value);

        public async Task<double> SortedSetIncrementAsync<T>(string key, T member, double value) =>
            await _db.SortedSetIncrementAsync(key, _serializer.SerializeToBytes(member), value);

        public async Task<long> SortedSetLengthAsync<T>(string key) => await _db.SortedSetLengthAsync(key);

        public async Task<long> SortedSetLengthByValueAsync(string key, int min, int max) =>
            await _db.SortedSetLengthByValueAsync(key, min, max);

        public async Task<long> SortedSetLengthByValueAsync(string key, long min, long max) =>
            await _db.SortedSetLengthByValueAsync(key, min, max);

        public async Task<long> SortedSetLengthByValueAsync(string key, float min, float max) =>
            await _db.SortedSetLengthByValueAsync(key, min, max);

        public async Task<long> SortedSetLengthByValueAsync(string key, double min, double max) =>
            await _db.SortedSetLengthByValueAsync(key, min, max);

        public async Task<long> SortedSetLengthByValueAsync(string key, string min, string max) =>
            await _db.SortedSetLengthByValueAsync(key, min, max);

        public async Task<IList<T>> SortedSetRangeByScoreAscendingAsync<T>(string key, double start = 0,
            double stop = -1)
        {
            var values = await _db.SortedSetRangeByScoreAsync(key, start, stop);
            return values.Select(value => _serializer.DeserializeFromBytes<T>(value)).ToList();
        }

        public async Task<IList<T>> SortedSetRangeByScoreDescendingAsync<T>(string key, double start = 0,
            double stop = -1)
        {
            var values = await _db.SortedSetRangeByScoreAsync(key, start, stop, order: Order.Descending);
            return values.Select(value => _serializer.DeserializeFromBytes<T>(value)).ToList();
        }

        public async Task<IDictionary<T, double>> SortedSetRangeByScoreWithScoresAscendingAsync<T>(string key,
            long start = 0, long stop = -1)
        {
            var values = await _db.SortedSetRangeByScoreWithScoresAsync(key, start, stop);
            return values.ToDictionary(k => _serializer.DeserializeFromBytes<T>(k.Element), v => v.Score);
        }

        public async Task<IDictionary<T, double>> SortedSetRangeByScoreWithScoresDescendingAsync<T>(string key,
            double start = 0, double stop = -1)
        {
            var values = await _db.SortedSetRangeByScoreWithScoresAsync(key, start, stop, order: Order.Descending);
            return values.ToDictionary(k => _serializer.DeserializeFromBytes<T>(k.Element), v => v.Score);
        }

        public async Task<IList<int>> SortedSetRangeByValueAsync(string key, int min, int max, long skip,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
            return values.Select(value => (int) value).ToList();
        }

        public async Task<IList<long>> SortedSetRangeByValueAsync(string key, long min, long max, long skip,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
            return values.Select(value => (long) value).ToList();
        }

        public async Task<IList<float>> SortedSetRangeByValueAsync(string key, float min, float max, long skip,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
            return values.Select(value => (float) value).ToList();
        }

        public async Task<IList<double>> SortedSetRangeByValueAsync(string key, double min, double max, long skip,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
            return values.Select(value => (double) value).ToList();
        }

        public async Task<IList<string>> SortedSetRangeByValueAsync(string key, string min, string max, long skip,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
            return values.Select(value => (string) value).ToList();
        }

        public async Task<IList<int>> SortedSetRangeByValueAscendingAsync(string key, int min = default,
            int max = default, long skip = 0,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, Order.Ascending, skip, take);
            return values.Select(value => (int) value).ToList();
        }

        public async Task<IList<long>> SortedSetRangeByValueAscendingAsync(string key, long min = default,
            long max = default, long skip = 0,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, Order.Ascending, skip, take);
            return values.Select(value => (long) value).ToList();
        }

        public async Task<IList<float>> SortedSetRangeByValueAscendingAsync(string key, float min = default,
            float max = default, long skip = 0,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, Order.Ascending, skip, take);
            return values.Select(value => (float) value).ToList();
        }

        public async Task<IList<double>> SortedSetRangeByValueAscendingAsync(string key, double min = default,
            double max = default, long skip = 0,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, Order.Ascending, skip, take);
            return values.Select(value => (double) value).ToList();
        }

        public async Task<IList<string>> SortedSetRangeByValueAscendingAsync(string key, string min = default,
            string max = default, long skip = 0,
            long take = -1)
        {
            var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, Order.Ascending, skip, take);
            return values.Select(value => (string) value).ToList();
        }

        public async Task<IList<int>> SortedSetRangeByValueDescendingAsync(string key, int min = default,
            int max = default, long skip = 0,
            long take = -1)
        {
            var values =
                await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, Order.Descending, skip, take);
            return values.Select(value => (int) value).ToList();
        }

        public async Task<IList<long>> SortedSetRangeByValueDescendingAsync(string key, long min = default,
            long max = default, long skip = 0,
            long take = -1)
        {
            var values =
                await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, Order.Descending, skip, take);
            return values.Select(value => (long) value).ToList();
        }

        public async Task<IList<float>> SortedSetRangeByValueDescendingAsync(string key, float min = default,
            float max = default, long skip = 0,
            long take = -1)
        {
            var values =
                await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, Order.Descending, skip, take);
            return values.Select(value => (float) value).ToList();
        }

        public async Task<IList<double>> SortedSetRangeByValueDescendingAsync(string key, double min = default,
            double max = default, long skip = 0,
            long take = -1)
        {
            var values =
                await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, Order.Descending, skip, take);
            return values.Select(value => (double) value).ToList();
        }

        public async Task<IList<string>> SortedSetRangeByValueDescendingAsync(string key, string min = default,
            string max = default, long skip = 0,
            long take = -1)
        {
            var values =
                await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, Order.Descending, skip, take);
            return values.Select(value => (string) value).ToList();
        }

        public async Task<bool> SortedSetRemoveAsync<T>(string key, T member) =>
            await _db.SortedSetRemoveAsync(key, _serializer.SerializeToBytes(member));

        public async Task<long> SortedSetRemoveRangeAsync<T>(string key, IEnumerable<T> members) =>
            await _db.SortedSetRemoveAsync(key,
                members.Select(member => (RedisValue) _serializer.SerializeToBytes(member)).ToArray());

        public async Task<long> SortedSetRemoveRangeByScoreAsync<T>(string key, double start, double stop) =>
            await _db.SortedSetRemoveRangeByScoreAsync(key, start, stop);

        public async Task<long> SortedSetRemoveRangeByValueAsync(string key, int min, int max) =>
            await _db.SortedSetRemoveRangeByValueAsync(key, min, max);

        public async Task<long> SortedSetRemoveRangeByValueAsync(string key, long min, long max) =>
            await _db.SortedSetRemoveRangeByValueAsync(key, min, max);

        public async Task<long> SortedSetRemoveRangeByValueAsync(string key, float min, float max) =>
            await _db.SortedSetRemoveRangeByValueAsync(key, min, max);

        public async Task<long> SortedSetRemoveRangeByValueAsync(string key, double min, double max) =>
            await _db.SortedSetRemoveRangeByValueAsync(key, min, max);

        public async Task<long> SortedSetRemoveRangeByValueAsync(string key, string min, string max) =>
            await _db.SortedSetRemoveRangeByValueAsync(key, min, max);

        public async Task<IDictionary<T, double>> SortedSetScanAsync<T>(string key, T pattern = default,
            int pageSize = 10, long cursor = 0, int pageOffset = 0)
        {
            var values = await Task.Factory.StartNew(() =>
                _db.SortedSetScan(key, _serializer.SerializeToBytes(pattern), pageSize, cursor, pageOffset));
            return values.ToDictionary(k => _serializer.DeserializeFromBytes<T>(k.Element), v => v.Score);
        }

        public async Task<double?> SortedSetScoreAsync<T>(string key, T member) =>
            await _db.SortedSetScoreAsync(key, _serializer.SerializeToBytes(member));
    }
}