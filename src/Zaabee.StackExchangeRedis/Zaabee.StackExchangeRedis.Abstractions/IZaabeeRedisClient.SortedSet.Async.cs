namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisClient
{
    ValueTask<bool> SortedSetAddAsync<T>(string key, T member, double score);

    ValueTask<long> SortedSetAddAsync<T>(string key, IDictionary<T, double> values);

    ValueTask<double> SortedSetDecrementAsync<T>(string key, T member, double value);

    ValueTask<double> SortedSetIncrementAsync<T>(string key, T member, double value);

    ValueTask<long> SortedSetLengthAsync<T>(string key);

    ValueTask<long> SortedSetLengthByValueAsync(string key, double min, double max);

    ValueTask<List<T>> SortedSetRangeByScoreAscendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    ValueTask<List<T>> SortedSetRangeByScoreDescendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    ValueTask<Dictionary<T, double>> SortedSetRangeByScoreWithScoresAscendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    ValueTask<Dictionary<T, double>> SortedSetRangeByScoreWithScoresDescendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    ValueTask<List<double>> SortedSetRangeByValueAsync(
        string key,
        double min,
        double max,
        long skip,
        long take = -1
    );

    ValueTask<List<double>> SortedSetRangeByValueAscendingAsync(
        string key,
        double min = default,
        double max = default,
        long skip = 0,
        long take = -1
    );

    ValueTask<List<double>> SortedSetRangeByValueDescendingAsync(
        string key,
        double min = default,
        double max = default,
        long skip = 0,
        long take = -1
    );

    ValueTask<bool> SortedSetRemoveAsync<T>(string key, T member);

    ValueTask<long> SortedSetRemoveRangeAsync<T>(string key, IEnumerable<T> members);

    ValueTask<long> SortedSetRemoveRangeByScoreAsync<T>(string key, double start, double stop);

    ValueTask<long> SortedSetRemoveRangeByValueAsync(string key, double min, double max);
}
