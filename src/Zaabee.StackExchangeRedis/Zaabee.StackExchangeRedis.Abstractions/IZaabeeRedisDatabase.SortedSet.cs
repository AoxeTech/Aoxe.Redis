namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisDatabase
{
    bool SortedSetAdd<T>(string key, T? member, double score);

    long SortedSetAdd<T>(string key, IDictionary<T, double> values);

    double SortedSetDecrement<T>(string key, T? member, double value);

    double SortedSetIncrement<T>(string key, T? member, double value);

    long SortedSetLength<T>(string key);

    long SortedSetLengthByValue(string key, int min, int max);

    long SortedSetLengthByValue(string key, long min, long max);

    long SortedSetLengthByValue(string key, float min, float max);

    long SortedSetLengthByValue(string key, double min, double max);

    long SortedSetLengthByValue(string key, string min, string max);

    List<T?> SortedSetRangeByScoreAscending<T>(string key, double start = 0, double stop = -1);

    List<T?> SortedSetRangeByScoreDescending<T>(string key, double start = 0, double stop = -1);

    Dictionary<T?, double> SortedSetRangeByScoreWithScoresAscending<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    Dictionary<T?, double> SortedSetRangeByScoreWithScoresDescending<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    List<int> SortedSetRangeByValue(string key, int min, int max, long skip, long take = -1);

    List<long> SortedSetRangeByValue(string key, long min, long max, long skip, long take = -1);

    List<float> SortedSetRangeByValue(string key, float min, float max, long skip, long take = -1);

    List<double> SortedSetRangeByValue(
        string key,
        double min,
        double max,
        long skip,
        long take = -1
    );

    List<string> SortedSetRangeByValue(
        string key,
        string min,
        string max,
        long skip,
        long take = -1
    );

    List<int> SortedSetRangeByValueAscending(
        string key,
        int min = default,
        int max = default,
        long skip = 0,
        long take = -1
    );

    List<long> SortedSetRangeByValueAscending(
        string key,
        long min = default,
        long max = default,
        long skip = 0,
        long take = -1
    );

    List<float> SortedSetRangeByValueAscending(
        string key,
        float min = default,
        float max = default,
        long skip = 0,
        long take = -1
    );

    List<double> SortedSetRangeByValueAscending(
        string key,
        double min = default,
        double max = default,
        long skip = 0,
        long take = -1
    );

    List<string> SortedSetRangeByValueAscending(
        string key,
        string? min = default,
        string? max = default,
        long skip = 0,
        long take = -1
    );

    List<int> SortedSetRangeByValueDescending(
        string key,
        int min = default,
        int max = default,
        long skip = 0,
        long take = -1
    );

    List<long> SortedSetRangeByValueDescending(
        string key,
        long min = default,
        long max = default,
        long skip = 0,
        long take = -1
    );

    List<float> SortedSetRangeByValueDescending(
        string key,
        float min = default,
        float max = default,
        long skip = 0,
        long take = -1
    );

    List<double> SortedSetRangeByValueDescending(
        string key,
        double min = default,
        double max = default,
        long skip = 0,
        long take = -1
    );

    List<string> SortedSetRangeByValueDescending(
        string key,
        string? min = default,
        string? max = default,
        long skip = 0,
        long take = -1
    );

    bool SortedSetRemove<T>(string key, T? member);

    long SortedSetRemoveRange<T>(string key, IEnumerable<T> members);

    long SortedSetRemoveRangeByScore<T>(string key, double start, double stop);

    long SortedSetRemoveRangeByValue(string key, int min, int max);

    long SortedSetRemoveRangeByValue(string key, long min, long max);

    long SortedSetRemoveRangeByValue(string key, float min, float max);

    long SortedSetRemoveRangeByValue(string key, double min, double max);

    long SortedSetRemoveRangeByValue(string key, string min, string max);

    Dictionary<T?, double> SortedSetScan<T>(
        string key,
        T? pattern = default,
        int pageSize = 10,
        long cursor = 0,
        int pageOffset = 0
    );

    double? SortedSetScore<T>(string key, T? member);
}
