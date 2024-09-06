namespace Aoxe.StackExchangeRedis.Abstractions;

public partial interface IAoxeRedisClient
{
    ValueTask<bool> HashAddAsync<T>(string key, string entityKey, T? entity);

    ValueTask HashAddRangeAsync<T>(string key, IDictionary<string, T?> entities);

    ValueTask<bool> HashDeleteAsync(string key, string entityKey);

    ValueTask<long> HashDeleteRangeAsync(string key, IEnumerable<string> entityKeys);

    ValueTask<T?> HashGetAsync<T>(string key, string entityKey);

    ValueTask<Dictionary<string, T?>> HashGetAsync<T>(string key);

    ValueTask<List<T?>> HashGetRangeAsync<T>(string key, IEnumerable<string> entityKeys);

    ValueTask<List<string>> HashGetAllEntityKeysAsync(string key);

    ValueTask<long> HashCountAsync(string key);
}
