namespace Aoxe.StackExchangeRedis;

public partial class AoxeRedisClient
{
    public bool Delete(string key) => db.KeyDelete(key);

    public long DeleteAll(IEnumerable<string> keys) =>
        db.KeyDelete(keys.Select(x => (RedisKey)x).ToArray());

    public bool Exists(string key) => db.KeyExists(key);

    public bool Expire(string key, TimeSpan? timeSpan) => db.KeyExpire(key, timeSpan);
}
