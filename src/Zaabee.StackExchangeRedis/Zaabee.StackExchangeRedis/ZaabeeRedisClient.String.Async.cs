namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient
{
    public async ValueTask<bool> AddAsync<T>(string key, T? entity, TimeSpan? expiry = null)
    {
        var bytes = ToRedisValue(entity);
        return await db.StringSetAsync(key, bytes, expiry ?? defaultExpiry);
    }

    public async ValueTask<T?> GetAsync<T>(string key)
    {
        var value = await db.StringGetAsync(key);
        if(!value.HasValue) return default;
        return typeof(T) switch
        {
            { } t when t == typeof(long) => (T)(object)long.Parse(value!),
            { } t when t == typeof(double) => (T)(object)double.Parse(value!),
            _ => FromRedisValue<T>(value)
        };
    }

    public async ValueTask<List<T?>> GetAsync<T>(IEnumerable<string> keys)
    {
        var values = await db.StringGetAsync(keys.Select(p => (RedisKey)p).ToArray());
        return values.Select(value => FromRedisValue<T>(value)).ToList();
    }

    public async Task<bool> AddAsync(string key, long value, TimeSpan? expiry = null) =>
        await db.StringSetAsync(key, value, expiry ?? defaultExpiry);

    public async Task<bool> AddAsync(string key, double value, TimeSpan? expiry = null) =>
        await db.StringSetAsync(key, value, expiry ?? defaultExpiry);

    public async Task<double> IncrementAsync(string key, double value) =>
        await db.StringIncrementAsync(key, value);

    public async Task<long> IncrementAsync(string key, long value) =>
        await db.StringIncrementAsync(key, value);
}
