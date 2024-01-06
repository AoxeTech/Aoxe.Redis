namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisDatabase
{
    ValueTask<bool> SortedSetAddAsync<T>(string key, T? member, double score);

    ValueTask<long> SortedSetAddAsync<T>(string key, Dictionary<T?, double> values);

    ValueTask<double> SortedSetDecrementAsync<T>(string key, T? member, double value);

    ValueTask<double> SortedSetIncrementAsync<T>(string key, T? member, double value);

    ValueTask<long> SortedSetLengthAsync<T>(string key);

    ValueTask<long> SortedSetLengthByValueAsync(string key, int min, int max);

    ValueTask<long> SortedSetLengthByValueAsync(string key, long min, long max);

    ValueTask<long> SortedSetLengthByValueAsync(string key, float min, float max);

    ValueTask<long> SortedSetLengthByValueAsync(string key, double min, double max);

    ValueTask<long> SortedSetLengthByValueAsync(string key, string min, string max);

    ValueTask<List<T?>> SortedSetRangeByScoreAscendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    ValueTask<List<T?>> SortedSetRangeByScoreDescendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    ValueTask<Dictionary<T?, double>> SortedSetRangeByScoreWithScoresAscendingAsync<T>(
        string key,
        long start = 0,
        long stop = -1
    );

    ValueTask<Dictionary<T?, double>> SortedSetRangeByScoreWithScoresDescendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    ValueTask<List<int>> SortedSetRangeByValueAsync(
        string key,
        int min,
        int max,
        long skip,
        long take = -1
    );

    ValueTask<List<long>> SortedSetRangeByValueAsync(
        string key,
        long min,
        long max,
        long skip,
        long take = -1
    );

    ValueTask<List<float>> SortedSetRangeByValueAsync(
        string key,
        float min,
        float max,
        long skip,
        long take = -1
    );

    ValueTask<List<double>> SortedSetRangeByValueAsync(
        string key,
        double min,
        double max,
        long skip,
        long take = -1
    );

    ValueTask<List<string>> SortedSetRangeByValueAsync(
        string key,
        string min,
        string max,
        long skip,
        long take = -1
    );

    ValueTask<List<int>> SortedSetRangeByValueAscendingAsync(
        string key,
        int min = default,
        int max = default,
        long skip = 0,
        long take = -1
    );

    ValueTask<List<long>> SortedSetRangeByValueAscendingAsync(
        string key,
        long min = default,
        long max = default,
        long skip = 0,
        long take = -1
    );

    ValueTask<List<float>> SortedSetRangeByValueAscendingAsync(
        string key,
        float min = default,
        float max = default,
        long skip = 0,
        long take = -1
    );

    ValueTask<List<double>> SortedSetRangeByValueAscendingAsync(
        string key,
        double min = default,
        double max = default,
        long skip = 0,
        long take = -1
    );

    ValueTask<List<string>> SortedSetRangeByValueAscendingAsync(
        string key,
        string? min = default,
        string? max = default,
        long skip = 0,
        long take = -1
    );

    ValueTask<List<int>> SortedSetRangeByValueDescendingAsync(
        string key,
        int min = default,
        int max = default,
        long skip = 0,
        long take = -1
    );

    ValueTask<List<long>> SortedSetRangeByValueDescendingAsync(
        string key,
        long min = default,
        long max = default,
        long skip = 0,
        long take = -1
    );

    ValueTask<List<float>> SortedSetRangeByValueDescendingAsync(
        string key,
        float min = default,
        float max = default,
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

    ValueTask<List<string>> SortedSetRangeByValueDescendingAsync(
        string key,
        string? min = default,
        string? max = default,
        long skip = 0,
        long take = -1
    );

    ValueTask<bool> SortedSetRemoveAsync<T>(string key, T? member);

    ValueTask<long> SortedSetRemoveRangeAsync<T>(string key, IEnumerable<T> members);

    ValueTask<long> SortedSetRemoveRangeByScoreAsync<T>(string key, double start, double stop);

    ValueTask<long> SortedSetRemoveRangeByValueAsync(string key, int min, int max);

    ValueTask<long> SortedSetRemoveRangeByValueAsync(string key, long min, long max);

    ValueTask<long> SortedSetRemoveRangeByValueAsync(string key, float min, float max);

    ValueTask<long> SortedSetRemoveRangeByValueAsync(string key, double min, double max);

    ValueTask<long> SortedSetRemoveRangeByValueAsync(string key, string min, string max);
}
