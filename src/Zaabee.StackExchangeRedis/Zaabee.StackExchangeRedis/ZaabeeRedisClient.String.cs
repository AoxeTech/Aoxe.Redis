namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient
{
    public bool Add<T>(string key, T? entity, TimeSpan? expiry = null)
    {
        var bytes = ToRedisValue(entity);
        return db.StringSet(key, bytes, expiry ?? defaultExpiry);
    }

    public T? Get<T>(string key) => FromRedisValue<T>(db.StringGet(key));

    public List<T?> Get<T>(IEnumerable<string> keys) =>
        db.StringGet(keys.Select(p => (RedisKey)p).ToArray()).Select(FromRedisValue<T>).ToList();

    public bool Add(string key, long value, TimeSpan? expiry = null) =>
        db.StringSet(key, value, expiry ?? defaultExpiry);

    public bool Add(string key, double value, TimeSpan? expiry = null) =>
        db.StringSet(key, value, expiry ?? defaultExpiry);

    public double Increment(string key, double value) => db.StringIncrement(key, value);

    public long Increment(string key, long value) => db.StringIncrement(key, value);
}
