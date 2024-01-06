namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public bool Add<T>(string key, T? entity, TimeSpan? expiry = null)
    {
        expiry ??= defaultExpiry;
        var bytes = serializer.ToBytes(entity);
        return db.StringSet(key, bytes, expiry);
    }

    public T? Get<T>(string key)
    {
        var value = db.StringGet(key);
        return value.HasValue ? serializer.FromBytes<T>(value) : default;
    }

    public List<T?> Get<T>(IEnumerable<string> keys) =>
        db.StringGet(keys.Select(p => (RedisKey)p).ToArray())
            .Select(value => serializer.FromBytes<T>(value)).ToList();
}
