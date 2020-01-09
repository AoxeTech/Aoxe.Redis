using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisDatabase
    {
        public bool SortedSetAdd<T>(string key, T member, double score) =>
            _db.SortedSetAdd(key, _serializer.Serialize(member), score);

        public long SortedSetAdd<T>(string key, IDictionary<T, double> values) => _db.SortedSetAdd(key,
            values.Select(value => new SortedSetEntry(_serializer.Serialize(value.Key), value.Value)).ToArray());

        public double SortedSetDecrement<T>(string key, T member, double value) =>
            _db.SortedSetDecrement(key, _serializer.Serialize(member), value);

        public double SortedSetIncrement<T>(string key, T member, double value) =>
            _db.SortedSetIncrement(key, _serializer.Serialize(member), value);

        public long SortedSetLength<T>(string key) => _db.SortedSetLength(key);

        public long SortedSetLengthByValue<T>(string key, T min, T max) =>
            _db.SortedSetLengthByValue(key, _serializer.Serialize(min), _serializer.Serialize(max));

        public IList<T> SortedSetRangeByScoreAscending<T>(string key, double start = 0, double stop = -1)
        {
            var values = _db.SortedSetRangeByScore(key, start, stop);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public IList<T> SortedSetRangeByScoreDescending<T>(string key, double start = 0, double stop = -1)
        {
            var values = _db.SortedSetRangeByScore(key, start, stop, order: Order.Descending);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public IDictionary<T, double> SortedSetRangeByScoreWithScoresAscending<T>(string key, double start = 0,
            double stop = -1)
        {
            var values = _db.SortedSetRangeByScoreWithScores(key, start, stop);
            return values.ToDictionary(k => _serializer.Deserialize<T>(k.Element), v => v.Score);
        }

        public IDictionary<T, double> SortedSetRangeByScoreWithScoresDescending<T>(string key, double start = 0,
            double stop = -1)
        {
            var values = _db.SortedSetRangeByScoreWithScores(key, start, stop, order: Order.Descending);
            return values.ToDictionary(k => _serializer.Deserialize<T>(k.Element), v => v.Score);
        }

        public IList<T> SortedSetRangeByValue<T>(string key, T min, T max, long skip, long take = -1)
        {
            var values =
                _db.SortedSetRangeByValue(key, _serializer.Serialize(min), _serializer.Serialize(max), Exclude.None,
                    skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public IList<T> SortedSetRangeByValueAscending<T>(string key, T min = default, T max = default,
            long skip = 0, long take = -1)
        {
            var values = _db.SortedSetRangeByValue(key, _serializer.Serialize(min), _serializer.Serialize(max),
                Exclude.None, Order.Ascending, skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public IList<T> SortedSetRangeByValueDescending<T>(string key, T min = default, T max = default,
            long skip = 0, long take = -1)
        {
            var values = _db.SortedSetRangeByValue(key, _serializer.Serialize(min), _serializer.Serialize(max),
                Exclude.None, Order.Descending, skip, take);
            return values.Select(value => _serializer.Deserialize<T>(value)).ToList();
        }

        public bool SortedSetRemove<T>(string key, T member) => _db.SortedSetRemove(key, _serializer.Serialize(member));

        public long SortedSetRemoveRange<T>(string key, IEnumerable<T> members) => _db.SortedSetRemove(key,
            members.Select(member => (RedisValue) _serializer.Serialize(member)).ToArray());

        public long SortedSetRemoveRangeByScore<T>(string key, double start, double stop) =>
            _db.SortedSetRemoveRangeByScore(key, start, stop);

        public long SortedSetRemoveRangeByValue<T>(string key, T min, T max) =>
            _db.SortedSetRemoveRangeByValue(key, _serializer.Serialize(min), _serializer.Serialize(max));

        public IDictionary<T, double> SortedSetScan<T>(string key, T pattern = default, int pageSize = 10,
            long cursor = 0, int pageOffset = 0)
        {
            var values = _db.SortedSetScan(key, _serializer.Serialize(pattern), pageSize, cursor, pageOffset);
            return values.ToDictionary(k => _serializer.Deserialize<T>(k.Element), v => v.Score);
        }

        public double? SortedSetScore<T>(string key, T member) =>
            _db.SortedSetScore(key, _serializer.Serialize(member));
    }
}