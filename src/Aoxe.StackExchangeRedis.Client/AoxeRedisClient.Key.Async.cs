namespace Aoxe.StackExchangeRedis.Client;

public partial class AoxeRedisClient
{
    public async ValueTask<bool> DeleteAsync(string key) => await db.KeyDeleteAsync(key);

    public async ValueTask<long> DeleteAllAsync(IEnumerable<string> keys) =>
        await db.KeyDeleteAsync(keys.Select(x => (RedisKey)x).ToArray());

    public async ValueTask<bool> ExistsAsync(string key) => await db.KeyExistsAsync(key);

    public async ValueTask<bool> ExpireAsync(string key, TimeSpan? timeSpan) =>
        await db.KeyExpireAsync(key, timeSpan);
}
