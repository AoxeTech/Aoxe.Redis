namespace Aoxe.StackExchangeRedis;

public partial class AoxeRedisClient
{
    public async ValueTask<bool> HashAddAsync<T>(string key, string entityKey, T? entity)
    {
        var bytes = ToRedisValue(entity);
        return await db.HashSetAsync(key, entityKey, bytes);
    }

    public async ValueTask HashAddRangeAsync<T>(string key, IDictionary<string, T?> entities)
    {
        var bytes = entities.Select(kv => new HashEntry(kv.Key, ToRedisValue(kv.Value))).ToArray();
        await db.HashSetAsync(key, bytes);
    }

    public async ValueTask<bool> HashDeleteAsync(string key, string entityKey) =>
        await db.HashDeleteAsync(key, entityKey);

    public async ValueTask<long> HashDeleteRangeAsync(string key, IEnumerable<string> entityKeys) =>
        await db.HashDeleteAsync(
            key,
            entityKeys.Select(entityKey => (RedisValue)entityKey).ToArray()
        );

    public async ValueTask<T?> HashGetAsync<T>(string key, string entityKey)
    {
        var value = await db.HashGetAsync(key, entityKey);
        return value.HasValue ? FromRedisValue<T>(value) : default;
    }

    public async ValueTask<Dictionary<string, T?>> HashGetAsync<T>(string key)
    {
        var kvs = await db.HashGetAllAsync(key);
        return kvs.ToDictionary(kv => kv.Name.ToString(), kv => FromRedisValue<T>(kv.Value));
    }

    public async ValueTask<List<T?>> HashGetRangeAsync<T>(
        string key,
        IEnumerable<string> entityKeys
    )
    {
        var values = await db.HashGetAsync(
            key,
            entityKeys.Select(entityKey => (RedisValue)entityKey).ToArray()
        );
        return values?.Select(value => FromRedisValue<T>(value)).ToList() ?? [];
    }

    public async ValueTask<List<string>> HashGetAllEntityKeysAsync(string key)
    {
        var keys = await db.HashKeysAsync(key);
        return keys.Select(entityKey => entityKey.ToString()).ToList();
    }

    public async ValueTask<long> HashCountAsync(string key) => await db.HashLengthAsync(key);
}
