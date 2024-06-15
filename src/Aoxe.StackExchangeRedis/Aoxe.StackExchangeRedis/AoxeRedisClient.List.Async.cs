namespace Aoxe.StackExchangeRedis;

public partial class AoxeRedisClient
{
    public async ValueTask<T?> ListGetByIndexAsync<T>(string key, long index) =>
        FromRedisValue<T>(await db.ListGetByIndexAsync(key, index));

    public async ValueTask<long> ListInsertAfterAsync<T>(string key, T? pivot, T? value) =>
        await db.ListInsertAfterAsync(key, ToRedisValue(pivot), ToRedisValue(value));

    public async ValueTask<long> ListInsertBeforeAsync<T>(string key, T? pivot, T? value) =>
        await db.ListInsertBeforeAsync(key, ToRedisValue(pivot), ToRedisValue(value));

    public async ValueTask<T?> ListLeftPopAsync<T>(string key) =>
        FromRedisValue<T>(await db.ListLeftPopAsync(key));

    public async ValueTask<long> ListLeftPushAsync<T>(string key, T? value) =>
        await db.ListLeftPushAsync(key, ToRedisValue(value));

    public async ValueTask<long> ListLeftPushRangeAsync<T>(string key, IEnumerable<T> values) =>
        await db.ListLeftPushAsync(key, values.Select(ToRedisValue).ToArray());

    public async ValueTask<long> ListLengthAsync(string key) => await db.ListLengthAsync(key);

    public async ValueTask<List<T?>> ListRangeAsync<T>(string key, long start = 0, long stop = -1)
    {
        var results = await db.ListRangeAsync(key, start, stop);
        return results.Select(FromRedisValue<T>).ToList();
    }

    public async ValueTask<long> ListRemoveAsync<T>(string key, T? value, long count = 0) =>
        await db.ListRemoveAsync(key, ToRedisValue(value), count);

    public async ValueTask<T?> ListRightPopAsync<T>(string key) =>
        FromRedisValue<T>(await db.ListRightPopAsync(key));

    public async ValueTask<long> ListRightPushAsync<T>(string key, T? value) =>
        await db.ListRightPushAsync(key, ToRedisValue(value));

    public async ValueTask<long> ListRightPushRangeAsync<T>(string key, IEnumerable<T> values) =>
        await db.ListRightPushAsync(key, values.Select(ToRedisValue).ToArray());

    public async ValueTask ListSetByIndexAsync<T>(string key, long index, T? value) =>
        await db.ListSetByIndexAsync(key, index, ToRedisValue(value));

    public async ValueTask ListTrimAsync(string key, long start, long stop) =>
        await db.ListTrimAsync(key, start, stop);

    public async ValueTask<T?> ListRightPopLeftPushAsync<T>(string source, string destination) =>
        FromRedisValue<T>(await db.ListRightPopLeftPushAsync(source, destination));
}
