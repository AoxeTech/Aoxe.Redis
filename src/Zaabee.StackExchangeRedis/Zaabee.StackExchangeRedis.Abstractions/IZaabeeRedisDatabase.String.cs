namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisDatabase
{
    bool Add<T>(string key, T? entity, TimeSpan? expiry = null);

    void AddRange<T>(
        IDictionary<string, T?> entities,
        TimeSpan? expiry = null,
        bool isBatch = false
    );

    T? Get<T>(string key);

    List<T> Get<T>(IEnumerable<string> keys, bool isBatch = false);

    bool Add(string key, long value, TimeSpan? expiry = null);

    bool Add(string key, double value, TimeSpan? expiry = null);

    double Increment(string key, double value);

    long Increment(string key, long value);
}
