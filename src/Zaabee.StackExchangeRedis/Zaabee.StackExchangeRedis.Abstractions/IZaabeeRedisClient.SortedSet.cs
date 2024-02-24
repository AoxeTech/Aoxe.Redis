namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisClient
{
    bool SortedSetAdd<T>(string key, T member, double score);

    long SortedSetAdd<T>(string key, IDictionary<T, double> values);

    double SortedSetDecrement<T>(string key, T member, double value);

    double SortedSetIncrement<T>(string key, T member, double value);

    long SortedSetLength<T>(string key);

    long SortedSetLengthByValue(string key, double min, double max);

    List<T> SortedSetRangeByScoreAscending<T>(string key, double start = 0, double stop = -1);

    List<T> SortedSetRangeByScoreDescending<T>(string key, double start = 0, double stop = -1);

    Dictionary<T, double> SortedSetRangeByScoreWithScoresAscending<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    Dictionary<T, double> SortedSetRangeByScoreWithScoresDescending<T>(
        string key,
        double start = 0,
        double stop = -1
    );

    List<double> SortedSetRangeByValue(
        string key,
        double min,
        double max,
        long skip,
        long take = -1
    );

    List<double> SortedSetRangeByValueAscending(
        string key,
        double min = default,
        double max = default,
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

    bool SortedSetRemove<T>(string key, T member);

    long SortedSetRemoveRange<T>(string key, IEnumerable<T> members);

    long SortedSetRemoveRangeByScore<T>(string key, double start, double stop);

    long SortedSetRemoveRangeByValue(string key, double min, double max);

    Dictionary<T, double> SortedSetScan<T>(
        string key,
        T pattern = default,
        int pageSize = 10,
        long cursor = 0,
        int pageOffset = 0
    );

    double? SortedSetScore<T>(string key, T member);
}
