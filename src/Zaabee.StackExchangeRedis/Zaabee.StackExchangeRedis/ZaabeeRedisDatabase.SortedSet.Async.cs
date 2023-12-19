namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public async ValueTask<bool> SortedSetAddAsync<T>(string key, T? member, double score) =>
        await _db.SortedSetAddAsync(key, _serializer.ToBytes(member), score);

    public async ValueTask<long> SortedSetAddAsync<T>(string key, IDictionary<T, double> values) =>
        await _db.SortedSetAddAsync(
            key,
            values
                .Select(value => new SortedSetEntry(_serializer.ToBytes(value.Key), value.Value))
                .ToArray()
        );

    public async ValueTask<double> SortedSetDecrementAsync<T>(
        string key,
        T? member,
        double value
    ) => await _db.SortedSetDecrementAsync(key, _serializer.ToBytes(member), value);

    public async ValueTask<double> SortedSetIncrementAsync<T>(
        string key,
        T? member,
        double value
    ) => await _db.SortedSetIncrementAsync(key, _serializer.ToBytes(member), value);

    public async ValueTask<long> SortedSetLengthAsync<T>(string key) =>
        await _db.SortedSetLengthAsync(key);

    public async ValueTask<long> SortedSetLengthByValueAsync(string key, int min, int max) =>
        await _db.SortedSetLengthByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetLengthByValueAsync(string key, long min, long max) =>
        await _db.SortedSetLengthByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetLengthByValueAsync(string key, float min, float max) =>
        await _db.SortedSetLengthByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetLengthByValueAsync(string key, double min, double max) =>
        await _db.SortedSetLengthByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetLengthByValueAsync(string key, string min, string max) =>
        await _db.SortedSetLengthByValueAsync(key, min, max);

    public async ValueTask<List<T>> SortedSetRangeByScoreAscendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    )
    {
        var values = await _db.SortedSetRangeByScoreAsync(key, start, stop);
        return values.Select(value => _serializer.FromBytes<T>(value)).ToList();
    }

    public async ValueTask<List<T>> SortedSetRangeByScoreDescendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    )
    {
        var values = await _db.SortedSetRangeByScoreAsync(
            key,
            start,
            stop,
            order: Order.Descending
        );
        return values.Select(value => _serializer.FromBytes<T>(value)).ToList();
    }

    public async ValueTask<IDictionary<T, double>> SortedSetRangeByScoreWithScoresAscendingAsync<T>(
        string key,
        long start = 0,
        long stop = -1
    )
    {
        var values = await _db.SortedSetRangeByScoreWithScoresAsync(key, start, stop);
        return values.ToDictionary(k => _serializer.FromBytes<T>(k.Element), v => v.Score);
    }

    public async ValueTask<
        IDictionary<T, double>
    > SortedSetRangeByScoreWithScoresDescendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    )
    {
        var values = await _db.SortedSetRangeByScoreWithScoresAsync(
            key,
            start,
            stop,
            order: Order.Descending
        );
        return values.ToDictionary(k => _serializer.FromBytes<T>(k.Element), v => v.Score);
    }

    public async ValueTask<List<int>> SortedSetRangeByValueAsync(
        string key,
        int min,
        int max,
        long skip,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
        return values.Select(value => (int)value).ToList();
    }

    public async ValueTask<List<long>> SortedSetRangeByValueAsync(
        string key,
        long min,
        long max,
        long skip,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
        return values.Select(value => (long)value).ToList();
    }

    public async ValueTask<List<float>> SortedSetRangeByValueAsync(
        string key,
        float min,
        float max,
        long skip,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
        return values.Select(value => (float)value).ToList();
    }

    public async ValueTask<List<double>> SortedSetRangeByValueAsync(
        string key,
        double min,
        double max,
        long skip,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
        return values.Select(value => (double)value).ToList();
    }

    public async ValueTask<List<string>> SortedSetRangeByValueAsync(
        string key,
        string min,
        string max,
        long skip,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
        return values.Select(value => (string)value).ToList();
    }

    public async ValueTask<List<int>> SortedSetRangeByValueAscendingAsync(
        string key,
        int min = default,
        int max = default,
        long skip = 0,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(
            key,
            min,
            max,
            Exclude.None,
            Order.Ascending,
            skip,
            take
        );
        return values.Select(value => (int)value).ToList();
    }

    public async ValueTask<List<long>> SortedSetRangeByValueAscendingAsync(
        string key,
        long min = default,
        long max = default,
        long skip = 0,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(
            key,
            min,
            max,
            Exclude.None,
            Order.Ascending,
            skip,
            take
        );
        return values.Select(value => (long)value).ToList();
    }

    public async ValueTask<List<float>> SortedSetRangeByValueAscendingAsync(
        string key,
        float min = default,
        float max = default,
        long skip = 0,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(
            key,
            min,
            max,
            Exclude.None,
            Order.Ascending,
            skip,
            take
        );
        return values.Select(value => (float)value).ToList();
    }

    public async ValueTask<List<double>> SortedSetRangeByValueAscendingAsync(
        string key,
        double min = default,
        double max = default,
        long skip = 0,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(
            key,
            min,
            max,
            Exclude.None,
            Order.Ascending,
            skip,
            take
        );
        return values.Select(value => (double)value).ToList();
    }

    public async ValueTask<List<string>> SortedSetRangeByValueAscendingAsync(
        string key,
        string? min = default,
        string? max = default,
        long skip = 0,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(
            key,
            min,
            max,
            Exclude.None,
            Order.Ascending,
            skip,
            take
        );
        return values.Select(value => (string)value).ToList();
    }

    public async ValueTask<List<int>> SortedSetRangeByValueDescendingAsync(
        string key,
        int min = default,
        int max = default,
        long skip = 0,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(
            key,
            min,
            max,
            Exclude.None,
            Order.Descending,
            skip,
            take
        );
        return values.Select(value => (int)value).ToList();
    }

    public async ValueTask<List<long>> SortedSetRangeByValueDescendingAsync(
        string key,
        long min = default,
        long max = default,
        long skip = 0,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(
            key,
            min,
            max,
            Exclude.None,
            Order.Descending,
            skip,
            take
        );
        return values.Select(value => (long)value).ToList();
    }

    public async ValueTask<List<float>> SortedSetRangeByValueDescendingAsync(
        string key,
        float min = default,
        float max = default,
        long skip = 0,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(
            key,
            min,
            max,
            Exclude.None,
            Order.Descending,
            skip,
            take
        );
        return values.Select(value => (float)value).ToList();
    }

    public async ValueTask<List<double>> SortedSetRangeByValueDescendingAsync(
        string key,
        double min = default,
        double max = default,
        long skip = 0,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(
            key,
            min,
            max,
            Exclude.None,
            Order.Descending,
            skip,
            take
        );
        return values.Select(value => (double)value).ToList();
    }

    public async ValueTask<List<string>> SortedSetRangeByValueDescendingAsync(
        string key,
        string? min = default,
        string? max = default,
        long skip = 0,
        long take = -1
    )
    {
        var values = await _db.SortedSetRangeByValueAsync(
            key,
            min,
            max,
            Exclude.None,
            Order.Descending,
            skip,
            take
        );
        return values.Select(value => (string)value).ToList();
    }

    public async ValueTask<bool> SortedSetRemoveAsync<T>(string key, T? member) =>
        await _db.SortedSetRemoveAsync(key, _serializer.ToBytes(member));

    public async ValueTask<long> SortedSetRemoveRangeAsync<T>(string key, IEnumerable<T> members) =>
        await _db.SortedSetRemoveAsync(
            key,
            members.Select(member => (RedisValue)_serializer.ToBytes(member)).ToArray()
        );

    public async ValueTask<long> SortedSetRemoveRangeByScoreAsync<T>(
        string key,
        double start,
        double stop
    ) => await _db.SortedSetRemoveRangeByScoreAsync(key, start, stop);

    public async ValueTask<long> SortedSetRemoveRangeByValueAsync(string key, int min, int max) =>
        await _db.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetRemoveRangeByValueAsync(string key, long min, long max) =>
        await _db.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetRemoveRangeByValueAsync(
        string key,
        float min,
        float max
    ) => await _db.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetRemoveRangeByValueAsync(
        string key,
        double min,
        double max
    ) => await _db.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetRemoveRangeByValueAsync(
        string key,
        string min,
        string max
    ) => await _db.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async ValueTask<IDictionary<T, double>> SortedSetScanAsync<T>(
        string key,
        T? pattern = default,
        int pageSize = 10,
        long cursor = 0,
        int pageOffset = 0
    )
    {
        var values = await ValueTask
            .Factory
            .StartNew(
                () =>
                    _db.SortedSetScan(
                        key,
                        _serializer.ToBytes(pattern),
                        pageSize,
                        cursor,
                        pageOffset
                    )
            );
        return values.ToDictionary(k => _serializer.FromBytes<T>(k.Element), v => v.Score);
    }

    public async ValueTask<double?> SortedSetScoreAsync<T>(string key, T? member) =>
        await _db.SortedSetScoreAsync(key, _serializer.ToBytes(member));
}
