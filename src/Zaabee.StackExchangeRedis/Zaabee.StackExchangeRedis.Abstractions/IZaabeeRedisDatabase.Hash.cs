namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisDatabase
{
    bool HashAdd<T>(string key, string entityKey, T? entity);

    void HashAddRange<T>(string key, IDictionary<string, T?> entities);

    bool HashDelete(string key, string entityKey);

    long HashDeleteRange(string key, IEnumerable<string> entityKeys);

    T? HashGet<T>(string key, string entityKey);

    IList<T> HashGet<T>(string key);

    IList<T> HashGetRange<T>(string key, IEnumerable<string> entityKeys);

    IList<string> HashGetAllEntityKeys(string key);

    long HashCount(string key);
}