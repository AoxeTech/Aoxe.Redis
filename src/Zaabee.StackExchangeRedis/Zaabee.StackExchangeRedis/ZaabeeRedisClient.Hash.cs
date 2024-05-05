namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient
{
    public bool HashAdd<T>(string key, string entityKey, T? entity) =>
        db.HashSet(key, entityKey, ToRedisValue(entity));

    public void HashAddRange<T>(string key, IDictionary<string, T?> entities)
    {
        var bytes = entities.Select(kv => new HashEntry(kv.Key, ToRedisValue(kv.Value))).ToArray();
        db.HashSet(key, bytes);
    }

    public bool HashDelete(string key, string entityKey) => db.HashDelete(key, entityKey);

    public long HashDeleteRange(string key, IEnumerable<string> entityKeys) =>
        db.HashDelete(key, entityKeys.Select(entityKey => (RedisValue)entityKey).ToArray());

    public T? HashGet<T>(string key, string entityKey)
    {
        var value = db.HashGet(key, entityKey);
        return value.HasValue ? FromRedisValue<T>(value) : default;
    }

    public Dictionary<string, T?> HashGet<T>(string key) =>
        db.HashGetAll(key)
            .ToDictionary(kv => kv.Name.ToString(), kv => FromRedisValue<T>(kv.Value));

    public List<T?> HashGetRange<T>(string key, IEnumerable<string> entityKeys)
    {
        var values = db.HashGet(
            key,
            entityKeys.Select(entityKey => (RedisValue)entityKey).ToArray()
        );
        return values?.Select(value => FromRedisValue<T>(value)).ToList() ?? [];
    }

    public List<string> HashGetAllEntityKeys(string key) =>
        db.HashKeys(key).Select(entityKey => entityKey.ToString()).ToList();

    public long HashCount(string key) => db.HashLength(key);
}
