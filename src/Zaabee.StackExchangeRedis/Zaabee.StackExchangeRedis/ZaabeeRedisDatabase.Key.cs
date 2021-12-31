namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public bool Delete(string key) => _db.KeyDelete(key);

    public long DeleteAll(IEnumerable<string> keys, bool isBatch = false) =>
        isBatch ? _db.KeyDelete(keys.Select(x => (RedisKey)x).ToArray()) : keys.Count(Delete);

    public bool Exists(string key) => _db.KeyExists(key);

    public bool Expire(string key, TimeSpan? timeSpan) => _db.KeyExpire(key, timeSpan);
}