using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.Redis.Abstractions
{
    public interface IZaabeeRedisClient
    {
        #region key

        bool Delete(string key);

        long DeleteAll(IList<string> keys);

        Task<bool> DeleteAsync(string key);

        Task<long> DeleteAllAsync(IList<string> keys);

        bool Exists(string key);

        Task<bool> ExistsAsync(string key);

        bool Expire(string key, TimeSpan? timeSpan);

        Task<bool> ExpireAsync(string key, TimeSpan? timeSpan);

        #endregion

        #region string

        bool Add<T>(string key, T entity, TimeSpan? expiry = null);

        void AddRange<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null);

        T Get<T>(string key);

        List<T> Get<T>(IList<string> keys);

        Task<bool> AddAsync<T>(string key, T entity, TimeSpan? expiry = null);

        Task AddRangeAsync<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null);

        Task<T> GetAsync<T>(string key);

        Task<List<T>> GetAsync<T>(IList<string> keys);

        #endregion

        #region set



        #endregion

        #region list



        #endregion

        #region hash

        bool HashAdd<T>(string key, string entityKey, T entity);

        void HashAddRange<T>(string key, IList<Tuple<string, T>> entities);

        bool HashDelete(string key, string entityKey);

        long HashDelete(string key, IList<string> entityKeys);

        T HashGet<T>(string key, string entityKey);

        List<T> HashGet<T>(string key);

        List<T> HashGet<T>(string key, IList<string> entityKeys);

        List<string> HashGetAllEntityKeys(string key);

        long HashCount(string key);

        Task<bool> HashAddAsync<T>(string key, string entityKey, T entity);

        Task HashAddRangeAsync<T>(string key, IList<Tuple<string, T>> entities);

        Task<bool> HashDeleteAsync(string key, string entityKey);

        Task<long> HashDeleteAsync(string key, IList<string> entityKeys);

        Task<T> HashGetAsync<T>(string key, string entityKey);

        Task<List<T>> HashGetAsync<T>(string key);

        Task<List<T>> HashGetAsync<T>(string key, IList<string> entityKeys);

        Task<List<string>> HashGetAllEntityKeysAsync(string key);

        Task<long> HashCountAsync(string key);

        #endregion

        #region zset



        #endregion
    }
}