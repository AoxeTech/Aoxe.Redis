// namespace Aoxe.StackExchangeRedis.Client;
//
// public partial class AoxeRedisClient
// {
//     public bool SortedSetAdd<T>(string key, T member, double score) =>
//         db.SortedSetAdd(key, ToRedisValue(member), score);
//
//     public long SortedSetAdd<T>(string key, IDictionary<T, double> values) =>
//         db.SortedSetAdd(
//             key,
//             values
//                 .Select(value => new SortedSetEntry(ToRedisValue(value.Key), value.Value))
//                 .ToArray()
//         );
//
//     public double SortedSetDecrement<T>(string key, T member, double value) =>
//         db.SortedSetDecrement(key, ToRedisValue(member), value);
//
//     public double SortedSetIncrement<T>(string key, T member, double value) =>
//         db.SortedSetIncrement(key, ToRedisValue(member), value);
//
//     public long SortedSetLength<T>(string key) => db.SortedSetLength(key);
//
//     public long SortedSetLengthByValue(string key, int min, int max) =>
//         db.SortedSetLengthByValue(key, min, max);
//
//     public long SortedSetLengthByValue(string key, long min, long max) =>
//         db.SortedSetLengthByValue(key, min, max);
//
//     public long SortedSetLengthByValue(string key, float min, float max) =>
//         db.SortedSetLengthByValue(key, min, max);
//
//     public long SortedSetLengthByValue(string key, double min, double max) =>
//         db.SortedSetLengthByValue(key, min, max);
//
//     public long SortedSetLengthByValue(string key, string min, string max) =>
//         db.SortedSetLengthByValue(key, min, max);
//
//     public long SortedSetLengthByValue<T>(string key, T min, T max) =>
//         db.SortedSetLengthByValue(key, ToRedisValue(min), ToRedisValue(max));
//
//     public List<T> SortedSetRangeByScoreAscending<T>(
//         string key,
//         double start = 0,
//         double stop = -1
//     ) =>
//         db.SortedSetRangeByScore(key, start, stop)
//             .Select(value => FromRedisValue<T>(value)!)
//             .ToList();
//
//     public List<T> SortedSetRangeByScoreDescending<T>(
//         string key,
//         double start = 0,
//         double stop = -1
//     ) =>
//         db.SortedSetRangeByScore(key, start, stop, order: Order.Descending)
//             .Select(value => FromRedisValue<T>(value)!)
//             .ToList();
//
//     public Dictionary<T, double> SortedSetRangeByScoreWithScoresAscending<T>(
//         string key,
//         double start = 0,
//         double stop = -1
//     ) =>
//         db.SortedSetRangeByScoreWithScores(key, start, stop)
//             .ToDictionary(k => FromRedisValue<T>(k.Element)!, v => v.Score);
//
//     public Dictionary<T, double> SortedSetRangeByScoreWithScoresDescending<T>(
//         string key,
//         double start = 0,
//         double stop = -1
//     ) =>
//         db.SortedSetRangeByScoreWithScores(key, start, stop, order: Order.Descending)
//             .ToDictionary(k => FromRedisValue<T>(k.Element)!, v => v.Score);
//
//     public List<int> SortedSetRangeByValue(
//         string key,
//         int min,
//         int max,
//         long skip,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, skip, take)
//             .Select(value => (int)value)
//             .ToList();
//
//     public List<long> SortedSetRangeByValue(
//         string key,
//         long min,
//         long max,
//         long skip,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, skip, take)
//             .Select(value => (long)value)
//             .ToList();
//
//     public List<float> SortedSetRangeByValue(
//         string key,
//         float min,
//         float max,
//         long skip,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, skip, take)
//             .Select(value => (float)value)
//             .ToList();
//
//     public List<double> SortedSetRangeByValue(
//         string key,
//         double min,
//         double max,
//         long skip,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, skip, take)
//             .Select(value => (double)value)
//             .ToList();
//
//     public List<string> SortedSetRangeByValue(
//         string key,
//         string min,
//         string max,
//         long skip,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, skip, take)
//             .Select(value => (string)value!)
//             .ToList();
//
//     public List<int> SortedSetRangeByValueAscending(
//         string key,
//         int min = default,
//         int max = default,
//         long skip = 0,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, Order.Ascending, skip, take)
//             .Select(value => (int)value)
//             .ToList();
//
//     public List<long> SortedSetRangeByValueAscending(
//         string key,
//         long min = default,
//         long max = default,
//         long skip = 0,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, Order.Ascending, skip, take)
//             .Select(value => (long)value)
//             .ToList();
//
//     public List<float> SortedSetRangeByValueAscending(
//         string key,
//         float min = default,
//         float max = default,
//         long skip = 0,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, Order.Ascending, skip, take)
//             .Select(value => (float)value)
//             .ToList();
//
//     public List<double> SortedSetRangeByValueAscending(
//         string key,
//         double min = default,
//         double max = default,
//         long skip = 0,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, Order.Ascending, skip, take)
//             .Select(value => (double)value)
//             .ToList();
//
//     public List<string> SortedSetRangeByValueAscending(
//         string key,
//         string? min = default,
//         string? max = default,
//         long skip = 0,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, Order.Ascending, skip, take)
//             .Select(value => (string)value!)
//             .ToList();
//
//     public List<int> SortedSetRangeByValueDescending(
//         string key,
//         int min = default,
//         int max = default,
//         long skip = 0,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, Order.Descending, skip, take)
//             .Select(value => (int)value)
//             .ToList();
//
//     public List<long> SortedSetRangeByValueDescending(
//         string key,
//         long min = default,
//         long max = default,
//         long skip = 0,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, Order.Descending, skip, take)
//             .Select(value => (long)value)
//             .ToList();
//
//     public List<float> SortedSetRangeByValueDescending(
//         string key,
//         float min = default,
//         float max = default,
//         long skip = 0,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, Order.Descending, skip, take)
//             .Select(value => (float)value)
//             .ToList();
//
//     public List<double> SortedSetRangeByValueDescending(
//         string key,
//         double min = default,
//         double max = default,
//         long skip = 0,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, Order.Descending, skip, take)
//             .Select(value => (double)value)
//             .ToList();
//
//     public List<string> SortedSetRangeByValueDescending(
//         string key,
//         string? min = default,
//         string? max = default,
//         long skip = 0,
//         long take = -1
//     ) =>
//         db.SortedSetRangeByValue(key, min, max, Exclude.None, Order.Descending, skip, take)
//             .Select(value => (string)value!)
//             .ToList();
//
//     public bool SortedSetRemove<T>(string key, T member) =>
//         db.SortedSetRemove(key, ToRedisValue(member));
//
//     public long SortedSetRemoveRange<T>(string key, IEnumerable<T> members) =>
//         db.SortedSetRemove(key, members.Select(ToRedisValue).ToArray());
//
//     public long SortedSetRemoveRangeByScore<T>(string key, double start, double stop) =>
//         db.SortedSetRemoveRangeByScore(key, start, stop);
//
//     public long SortedSetRemoveRangeByValue(string key, int min, int max) =>
//         db.SortedSetRemoveRangeByValue(key, min, max);
//
//     public long SortedSetRemoveRangeByValue(string key, long min, long max) =>
//         db.SortedSetRemoveRangeByValue(key, min, max);
//
//     public long SortedSetRemoveRangeByValue(string key, float min, float max) =>
//         db.SortedSetRemoveRangeByValue(key, min, max);
//
//     public long SortedSetRemoveRangeByValue(string key, double min, double max) =>
//         db.SortedSetRemoveRangeByValue(key, min, max);
//
//     public long SortedSetRemoveRangeByValue(string key, string min, string max) =>
//         db.SortedSetRemoveRangeByValue(key, min, max);
//
//     public Dictionary<T, double> SortedSetScan<T>(
//         string key,
//         T pattern = default,
//         int pageSize = 10,
//         long cursor = 0,
//         int pageOffset = 0
//     ) =>
//         db.SortedSetScan(key, ToRedisValue(pattern), pageSize, cursor, pageOffset)
//             .ToDictionary(k => FromRedisValue<T>(k.Element)!, v => v.Score);
//
//     public double? SortedSetScore<T>(string key, T member) =>
//         db.SortedSetScore(key, ToRedisValue(member));
// }
