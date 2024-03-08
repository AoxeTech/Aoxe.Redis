namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisClient
{
    ValueTask<T?> ListGetByIndexAsync<T>(string key, long index);

    ValueTask<long> ListInsertAfterAsync<T>(string key, T? pivot, T? value);

    ValueTask<long> ListInsertBeforeAsync<T>(string key, T? pivot, T? value);

    ValueTask<T?> ListLeftPopAsync<T>(string key);

    ValueTask<long> ListLeftPushAsync<T>(string key, T? value);

    ValueTask<long> ListLeftPushRangeAsync<T>(string key, IEnumerable<T> values);

    ValueTask<long> ListLengthAsync(string key);

    ValueTask<List<T?>> ListRangeAsync<T>(string key, long start = 0, long stop = -1);

    ValueTask<long> ListRemoveAsync<T>(string key, T? value, long count = 0);

    ValueTask<T?> ListRightPopAsync<T>(string key);

    ValueTask<long> ListRightPushAsync<T>(string key, T? value);

    ValueTask<long> ListRightPushRangeAsync<T>(string key, IEnumerable<T> values);

    ValueTask ListSetByIndexAsync<T>(string key, long index, T? value);

    ValueTask ListTrimAsync(string key, long start, long stop);

    ValueTask<T?> ListRightPopLeftPushAsync<T>(string source, string destination);
}
