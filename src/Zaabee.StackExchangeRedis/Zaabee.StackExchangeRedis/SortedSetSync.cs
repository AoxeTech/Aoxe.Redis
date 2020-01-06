using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient
    {
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
    }
}