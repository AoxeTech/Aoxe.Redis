namespace Aoxe.StackExchangeRedis.Abstractions;

public partial interface IAoxeRedisClient
{
    bool Delete(string key);

    long DeleteAll(IEnumerable<string> keys);

    bool Exists(string key);

    bool Expire(string key, TimeSpan? timeSpan);
}
