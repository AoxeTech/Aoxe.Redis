namespace Aoxe.StackExchangeRedis;

public partial class AoxeRedisClient
{
    public T? ListGetByIndex<T>(string key, long index) =>
        FromRedisValue<T>(db.ListGetByIndex(key, index));

    public long ListInsertAfter<T>(string key, T? pivot, T? value) =>
        db.ListInsertAfter(key, ToRedisValue(pivot), ToRedisValue(value));

    public long ListInsertBefore<T>(string key, T? pivot, T? value) =>
        db.ListInsertBefore(key, ToRedisValue(pivot), ToRedisValue(value));

    public T? ListLeftPop<T>(string key) => FromRedisValue<T>(db.ListLeftPop(key));

    public long ListLeftPush<T>(string key, T? value) =>
        db.ListLeftPush(key, (RedisValue)ToRedisValue(value));

    public long ListLeftPushRange<T>(string key, IEnumerable<T> values) =>
        db.ListLeftPush(key, values.Select(value => (RedisValue)ToRedisValue(value)).ToArray());

    public long ListLength(string key) => db.ListLength(key);

    public List<T?> ListRange<T>(string key, long start = 0, long stop = -1) =>
        db.ListRange(key, start, stop).Select(FromRedisValue<T>).ToList();

    public long ListRemove<T>(string key, T? value, long count = 0) =>
        db.ListRemove(key, ToRedisValue(value), count);

    public T? ListRightPop<T>(string key) => FromRedisValue<T>(db.ListRightPop(key));

    public long ListRightPush<T>(string key, T? value) =>
        db.ListRightPush(key, ToRedisValue(value));

    public long ListRightPushRange<T>(string key, IEnumerable<T> values) =>
        db.ListRightPush(key, values.Select(value => (RedisValue)ToRedisValue(value)).ToArray());

    public void ListSetByIndex<T>(string key, long index, T? value) =>
        db.ListSetByIndex(key, index, ToRedisValue(value));

    public void ListTrim(string key, long start, long stop) => db.ListTrim(key, start, stop);

    public T? ListRightPopLeftPush<T>(string source, string destination) =>
        FromRedisValue<T>(db.ListRightPopLeftPush(source, destination));
}
