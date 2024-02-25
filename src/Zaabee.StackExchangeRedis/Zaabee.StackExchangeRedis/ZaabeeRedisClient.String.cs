namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient
{
    public bool Add<T>(string key, T? entity, TimeSpan? expiry = null)
    {
        var bytes = serializer.ToBytes(entity);
        return db.StringSet(key, bytes, expiry ?? defaultExpiry);
    }

    public T? Get<T>(string key)
    {
        var value = db.StringGet(key);
        if(!value.HasValue) return default;
        return typeof(T) switch
        {
            { } t when t == typeof(long) => (T)(object)long.Parse(value!),
            { } t when t == typeof(double) => (T)(object)double.Parse(value!),
            _ => serializer.FromBytes<T>(value)
        };
    }

    public List<T?> Get<T>(IEnumerable<string> keys) =>
        db.StringGet(keys.Select(p => (RedisKey)p).ToArray())
            .Select(value => serializer.FromBytes<T>(value))
            .ToList();

    public bool Add(string key, long value, TimeSpan? expiry = null) =>
        db.StringSet(key, value, expiry ?? defaultExpiry);

    public bool Add(string key, double value, TimeSpan? expiry = null) =>
        db.StringSet(key, value, expiry ?? defaultExpiry);

    public double Increment(string key, double value) => db.StringIncrement(key, value);

    public long Increment(string key, long value) => db.StringIncrement(key, value);
}
