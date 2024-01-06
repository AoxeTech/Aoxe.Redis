namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public async ValueTask<T?> ListGetByIndexAsync<T>(string key, long index) =>
        serializer.FromBytes<T>(await db.ListGetByIndexAsync(key, index));

    public async ValueTask<long> ListInsertAfterAsync<T>(string key, T? pivot, T? value) =>
        await db.ListInsertAfterAsync(key, serializer.ToBytes(pivot), serializer.ToBytes(value));

    public async ValueTask<long> ListInsertBeforeAsync<T>(string key, T? pivot, T? value) =>
        await db.ListInsertBeforeAsync(key, serializer.ToBytes(pivot), serializer.ToBytes(value));

    public async ValueTask<T?> ListLeftPopAsync<T>(string key) =>
        serializer.FromBytes<T>(await db.ListLeftPopAsync(key));

    public async ValueTask<long> ListLeftPushAsync<T>(string key, T? value) =>
        await db.ListLeftPushAsync(key, serializer.ToBytes(value));

    public async ValueTask<long> ListLeftPushRangeAsync<T>(string key, IEnumerable<T> values) =>
        await db.ListLeftPushAsync(
            key,
            values.Select(value => (RedisValue)serializer.ToBytes(value)).ToArray()
        );

    public async ValueTask<long> ListLengthAsync(string key) => await db.ListLengthAsync(key);

    public async ValueTask<List<T?>> ListRangeAsync<T>(string key, long start = 0, long stop = -1)
    {
        var results = await db.ListRangeAsync(key, start, stop);
        return results.Select(value => serializer.FromBytes<T>(value)).ToList();
    }

    public async ValueTask<long> ListRemoveAsync<T>(string key, T? value, long count = 0) =>
        await db.ListRemoveAsync(key, serializer.ToBytes(value), count);

    public async ValueTask<T?> ListRightPopAsync<T>(string key) =>
        serializer.FromBytes<T>(await db.ListRightPopAsync(key));

    public async ValueTask<T?> ListRightPopLeftPushAsync<T>(string source, string destination) =>
        serializer.FromBytes<T>(await db.ListRightPopLeftPushAsync(source, destination));

    public async ValueTask<long> ListRightPushAsync<T>(string key, T? value) =>
        await db.ListRightPushAsync(key, serializer.ToBytes(value));

    public async ValueTask<long> ListRightPushRangeAsync<T>(string key, IEnumerable<T> values) =>
        await db.ListRightPushAsync(
            key,
            values.Select(value => (RedisValue)serializer.ToBytes(value)).ToArray()
        );

    public async ValueTask ListSetByIndexAsync<T>(string key, long index, T? value) =>
        await db.ListSetByIndexAsync(key, index, serializer.ToBytes(value));

    public async ValueTask ListTrimAsync(string key, long start, long stop) =>
        await db.ListTrimAsync(key, start, stop);
}
