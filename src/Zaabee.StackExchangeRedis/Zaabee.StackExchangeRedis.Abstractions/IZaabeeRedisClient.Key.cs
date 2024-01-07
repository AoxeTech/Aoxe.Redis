namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisClient
{
    bool Delete(string key);

    long DeleteAll(IEnumerable<string> keys);

    bool Exists(string key);

    bool Expire(string key, TimeSpan? timeSpan);
}
