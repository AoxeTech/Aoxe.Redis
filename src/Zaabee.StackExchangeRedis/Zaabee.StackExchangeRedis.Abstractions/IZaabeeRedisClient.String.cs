namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisClient
{
    bool Add<T>(string key, T? entity, TimeSpan? expiry = null);

    T? Get<T>(string key);

    List<T?> Get<T>(IEnumerable<string> keys);

    bool Add(string key, long value, TimeSpan? expiry = null);

    bool Add(string key, double value, TimeSpan? expiry = null);

    double Increment(string key, double value);

    long Increment(string key, long value);
}
