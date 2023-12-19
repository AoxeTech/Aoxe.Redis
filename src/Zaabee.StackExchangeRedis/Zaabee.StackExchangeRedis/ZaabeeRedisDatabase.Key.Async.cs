namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public async ValueTask<bool> DeleteAsync(string key) => await _db.KeyDeleteAsync(key);

    public async ValueTask<long> DeleteAllAsync(IEnumerable<string> keys, bool isBatch = false)
    {
        if (isBatch)
            await _db.KeyDeleteAsync(keys.Select(x => (RedisKey)x).ToArray());
        var result = 0;
        foreach (var key in keys)
            if (await DeleteAsync(key))
                result++;

        return result;
    }

    public ValueTask<bool> ExistsAsync(string key) => _db.KeyExistsAsync(key);

    public ValueTask<bool> ExpireAsync(string key, TimeSpan? timeSpan) =>
        _db.KeyExpireAsync(key, timeSpan);
}
