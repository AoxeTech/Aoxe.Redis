using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface IHashAsync
    {
        Task<bool> HashAddAsync<T>(string key, string entityKey, T entity);

        Task HashAddRangeAsync<T>(string key, Dictionary<string, T> entities);

        Task<bool> HashDeleteAsync(string key, string entityKey);

        Task<long> HashDeleteRangeAsync(string key, IEnumerable<string> entityKeys);

        Task<T> HashGetAsync<T>(string key, string entityKey);

        Task<IList<T>> HashGetAsync<T>(string key);

        Task<IList<T>> HashGetRangeAsync<T>(string key, IEnumerable<string> entityKeys);

        Task<IList<string>> HashGetAllEntityKeysAsync(string key);

        Task<long> HashCountAsync(string key);
    }
}