namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public async Task<bool> HashAddAsync<T>(string key, string entityKey, T? entity)
    {
        var bytes = _serializer.ToBytes(entity);
        return await _db.HashSetAsync(key, entityKey, bytes);
    }

    public async Task HashAddRangeAsync<T>(string key, IDictionary<string, T?> entities)
    {
        var bytes = entities.Select(kv =>
            new HashEntry(kv.Key, _serializer.ToBytes(kv.Value))).ToArray();
        await _db.HashSetAsync(key, bytes);
    }

    public async Task<bool> HashDeleteAsync(string key, string entityKey) =>
        await _db.HashDeleteAsync(key, entityKey);

    public async Task<long> HashDeleteRangeAsync(string key, IEnumerable<string> entityKeys) =>
        await _db.HashDeleteAsync(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());

    public async Task<T?> HashGetAsync<T>(string key, string entityKey)
    {
        var value = await _db.HashGetAsync(key, entityKey);
        return value.HasValue ? _serializer.FromBytes<T>(value) : default;
    }

    public async Task<IList<T?>> HashGetAsync<T>(string key)
    {
        var kvs = await _db.HashGetAllAsync(key);
        return kvs.Select(kv => _serializer.FromBytes<T>(kv.Value)).ToList();
    }

    public async Task<IList<T>> HashGetRangeAsync<T>(string key, IEnumerable<string> entityKeys)
    {
        var values = await _db.HashGetAsync(key, entityKeys.Select(entityKey => (RedisValue) entityKey).ToArray());
        return values?.Select(value => _serializer.FromBytes<T>(value)).ToList() ?? new List<T>();
    }

    public async Task<IList<string>> HashGetAllEntityKeysAsync(string key)
    {
        var keys = await _db.HashKeysAsync(key);
        return keys.Select(entityKey => entityKey.ToString()).ToList();
    }

    public async Task<long> HashCountAsync(string key) => await _db.HashLengthAsync(key);
}