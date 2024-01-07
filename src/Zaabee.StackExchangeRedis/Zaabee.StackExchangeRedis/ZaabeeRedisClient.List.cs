namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient
{
    public T? ListGetByIndex<T>(string key, long index) =>
        serializer.FromBytes<T>(db.ListGetByIndex(key, index));

    public long ListInsertAfter<T>(string key, T? pivot, T? value) =>
        db.ListInsertAfter(key, serializer.ToBytes(pivot), serializer.ToBytes(value));

    public long ListInsertBefore<T>(string key, T? pivot, T? value) =>
        db.ListInsertBefore(key, serializer.ToBytes(pivot), serializer.ToBytes(value));

    public T? ListLeftPop<T>(string key) => serializer.FromBytes<T>(db.ListLeftPop(key));

    public long ListLeftPush<T>(string key, T? value) =>
        db.ListLeftPush(key, (RedisValue)serializer.ToBytes(value));

    public long ListLeftPushRange<T>(string key, IEnumerable<T> values) =>
        db.ListLeftPush(
            key,
            values.Select(value => (RedisValue)serializer.ToBytes(value)).ToArray()
        );

    public long ListLength(string key) => db.ListLength(key);

    public List<T?> ListRange<T>(string key, long start = 0, long stop = -1) =>
        db.ListRange(key, start, stop).Select(value => serializer.FromBytes<T>(value)).ToList();

    public long ListRemove<T>(string key, T? value, long count = 0) =>
        db.ListRemove(key, serializer.ToBytes(value), count);

    public T? ListRightPop<T>(string key) => serializer.FromBytes<T>(db.ListRightPop(key));

    public T? ListRightPopLeftPush<T>(string source, string destination) =>
        serializer.FromBytes<T>(db.ListRightPopLeftPush(source, destination));

    public long ListRightPush<T>(string key, T? value) =>
        db.ListRightPush(key, serializer.ToBytes(value));

    public long ListRightPushRange<T>(string key, IEnumerable<T> values) =>
        db.ListRightPush(
            key,
            values.Select(value => (RedisValue)serializer.ToBytes(value)).ToArray()
        );

    public void ListSetByIndex<T>(string key, long index, T? value) =>
        db.ListSetByIndex(key, index, serializer.ToBytes(value));

    public void ListTrim(string key, long start, long stop) => db.ListTrim(key, start, stop);
}
