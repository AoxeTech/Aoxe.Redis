namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient
{
    public async ValueTask<bool> AddAsync<T>(string key, T? entity, TimeSpan? expiry = null)
    {
        expiry ??= defaultExpiry;
        var bytes = serializer.ToBytes(entity);
        return await db.StringSetAsync(key, bytes, expiry);
    }

    public async ValueTask<T?> GetAsync<T>(string key)
    {
        var value = await db.StringGetAsync(key);
        return value.HasValue ? serializer.FromBytes<T>(value) : default;
    }

    public async ValueTask<List<T?>> GetAsync<T>(IEnumerable<string> keys)
    {
        var values = await db.StringGetAsync(keys.Select(p => (RedisKey)p).ToArray());
        return values.Select(value => serializer.FromBytes<T>(value)).ToList();
    }
}
