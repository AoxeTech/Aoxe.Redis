using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.Redis.Abstractions
{
    public interface IHashOperate
    {
        bool HashAdd<T>(string key, string entityKey, T entity);

        Task<bool> HashAddAsync<T>(string key, string entityKey, T entity);

        void HashAddRange<T>(string key, IList<Tuple<string, T>> entities);

        Task HashAddRangeAsync<T>(string key, IList<Tuple<string, T>> entities);

        bool HashDelete(string key, string entityKey);

        Task<bool> HashDeleteAsync(string key, string entityKey);

        long HashDelete(string key, IList<string> entityKeys);

        Task<long> HashDeleteAsync(string key, IList<string> entityKeys);

        T HashGet<T>(string key, string entityKey);

        Task<T> HashGetAsync<T>(string key, string entityKey);

        List<T> HashGet<T>(string key);

        Task<List<T>> HashGetAsync<T>(string key);

        List<T> HashGet<T>(string key, IList<string> entityKeys);

        Task<List<T>> HashGetAsync<T>(string key, IList<string> entityKeys);

        List<string> HashGetAllEntityKeys(string key);

        Task<List<string>> HashGetAllEntityKeysAsync(string key);

        long HashCount(string key);

        Task<long> HashCountAsync(string key);
    }
}