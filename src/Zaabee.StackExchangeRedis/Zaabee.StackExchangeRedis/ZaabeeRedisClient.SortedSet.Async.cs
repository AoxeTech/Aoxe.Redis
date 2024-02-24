namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient
{
    public async ValueTask<bool> SortedSetAddAsync<T>(string key, T member, double score) =>
        await db.SortedSetAddAsync(key, serializer.ToBytes(member), score);

    public async ValueTask<long> SortedSetAddAsync<T>(string key, IDictionary<T, double> values) =>
        await db.SortedSetAddAsync(
            key,
            values
                .Select(value => new SortedSetEntry(serializer.ToBytes(value.Key), value.Value))
                .ToArray()
        );

    public async ValueTask<double> SortedSetDecrementAsync<T>(
        string key,
        T member,
        double value
    ) => await db.SortedSetDecrementAsync(key, serializer.ToBytes(member), value);

    public async ValueTask<double> SortedSetIncrementAsync<T>(
        string key,
        T member,
        double value
    ) => await db.SortedSetIncrementAsync(key, serializer.ToBytes(member), value);

    public async ValueTask<long> SortedSetLengthAsync<T>(string key) =>
        await db.SortedSetLengthAsync(key);

    public async ValueTask<long> SortedSetLengthByValueAsync(string key, int min, int max) =>
        await db.SortedSetLengthByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetLengthByValueAsync(string key, long min, long max) =>
        await db.SortedSetLengthByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetLengthByValueAsync(string key, float min, float max) =>
        await db.SortedSetLengthByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetLengthByValueAsync(string key, double min, double max) =>
        await db.SortedSetLengthByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetLengthByValueAsync(string key, string min, string max) =>
        await db.SortedSetLengthByValueAsync(key, min, max);

    public async ValueTask<List<T>> SortedSetRangeByScoreAscendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    )
    {
        var values = await db.SortedSetRangeByScoreAsync(key, start, stop);
        return values.Select(value => serializer.FromBytes<T>(value)!).ToList();
    }

    public async ValueTask<List<T>> SortedSetRangeByScoreDescendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    )
    {
        var values = await db.SortedSetRangeByScoreAsync(key, start, stop, order: Order.Descending);
        return values.Select(value => serializer.FromBytes<T>(value)!).ToList();
    }

    public async ValueTask<Dictionary<T, double>> SortedSetRangeByScoreWithScoresAscendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    )
    {
        var values = await db.SortedSetRangeByScoreWithScoresAsync(key, start, stop);
        return values.ToDictionary(k => serializer.FromBytes<T>(k.Element), v => v.Score);
    }

    public async ValueTask<Dictionary<T, double>> SortedSetRangeByScoreWithScoresDescendingAsync<T>(
        string key,
        double start = 0,
        double stop = -1
    )
    {
        var values = await db.SortedSetRangeByScoreWithScoresAsync(
            key,
            start,
            stop,
            order: Order.Descending
        );
        return values.ToDictionary(k => serializer.FromBytes<T>(k.Element), v => v.Score);
    }

    public async ValueTask<List<int>> SortedSetRangeByValueAsync(
        string key,
        int min,
        int max,
        long skip,
        long take = -1
    )
    {
        var values = await db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
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
        var values = await db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
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
        var values = await db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
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
        var values = await db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
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
        var values = await db.SortedSetRangeByValueAsync(key, min, max, Exclude.None, skip, take);
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
        var values = await db.SortedSetRangeByValueAsync(
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
        var values = await db.SortedSetRangeByValueAsync(
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
        var values = await db.SortedSetRangeByValueAsync(
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
        var values = await db.SortedSetRangeByValueAsync(
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
        var values = await db.SortedSetRangeByValueAsync(
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
        var values = await db.SortedSetRangeByValueAsync(
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
        var values = await db.SortedSetRangeByValueAsync(
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
        var values = await db.SortedSetRangeByValueAsync(
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
        var values = await db.SortedSetRangeByValueAsync(
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
        var values = await db.SortedSetRangeByValueAsync(
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

    public async ValueTask<bool> SortedSetRemoveAsync<T>(string key, T member) =>
        await db.SortedSetRemoveAsync(key, serializer.ToBytes(member));

    public async ValueTask<long> SortedSetRemoveRangeAsync<T>(string key, IEnumerable<T> members) =>
        await db.SortedSetRemoveAsync(
            key,
            members.Select(member => (RedisValue)serializer.ToBytes(member)).ToArray()
        );

    public async ValueTask<long> SortedSetRemoveRangeByScoreAsync<T>(
        string key,
        double start,
        double stop
    ) => await db.SortedSetRemoveRangeByScoreAsync(key, start, stop);

    public async ValueTask<long> SortedSetRemoveRangeByValueAsync(string key, int min, int max) =>
        await db.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetRemoveRangeByValueAsync(string key, long min, long max) =>
        await db.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetRemoveRangeByValueAsync(
        string key,
        float min,
        float max
    ) => await db.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetRemoveRangeByValueAsync(
        string key,
        double min,
        double max
    ) => await db.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async ValueTask<long> SortedSetRemoveRangeByValueAsync(
        string key,
        string min,
        string max
    ) => await db.SortedSetRemoveRangeByValueAsync(key, min, max);
}
