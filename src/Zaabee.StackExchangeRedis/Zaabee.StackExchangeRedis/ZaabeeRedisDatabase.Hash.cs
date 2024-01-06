namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public bool HashAdd<T>(string key, string entityKey, T? entity) =>
        db.HashSet(key, entityKey, serializer.ToBytes(entity));

    public void HashAddRange<T>(string key, IDictionary<string, T?> entities)
    {
        var bytes = entities
            .Select(kv => new HashEntry(kv.Key, serializer.ToBytes(kv.Value)))
            .ToArray();
        db.HashSet(key, bytes);
    }

    public bool HashDelete(string key, string entityKey) => db.HashDelete(key, entityKey);

    public long HashDeleteRange(string key, IEnumerable<string> entityKeys) =>
        db.HashDelete(key, entityKeys.Select(entityKey => (RedisValue)entityKey).ToArray());

    public T? HashGet<T>(string key, string entityKey)
    {
        var value = db.HashGet(key, entityKey);
        return value.HasValue ? serializer.FromBytes<T>(value) : default;
    }

    public Dictionary<string, T?> HashGet<T>(string key) =>
        db.HashGetAll(key)
            .ToDictionary(kv => kv.Name.ToString(), kv => serializer.FromBytes<T>(kv.Value));

    public List<T?> HashGetRange<T>(string key, IEnumerable<string> entityKeys)
    {
        var values = db.HashGet(
            key,
            entityKeys.Select(entityKey => (RedisValue)entityKey).ToArray()
        );
        return values?.Select(value => serializer.FromBytes<T>(value)).ToList() ?? [];
    }

    public List<string> HashGetAllEntityKeys(string key) =>
        db.HashKeys(key).Select(entityKey => entityKey.ToString()).ToList();

    public long HashCount(string key) => db.HashLength(key);
}
