namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisClient
{
    bool Add<T>(string key, T? entity, TimeSpan? expiry = null);

    T? Get<T>(string key);

    List<T?> Get<T>(IEnumerable<string> keys);
}
