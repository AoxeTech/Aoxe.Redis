using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.Redis.Abstractions
{
    public interface IZaabeeRedisClient
    {
        #region key

        bool Delete(string key);

        Task<bool> DeleteAsync(string key);

        long DeleteAll(IList<string> keys);

        Task<long> DeleteAllAsync(IList<string> keys);

        #endregion

        #region string

        bool Add<T>(string key, T entity, TimeSpan? expiry = null);

        Task<bool> AddAsync<T>(string key, T entity, TimeSpan? expiry = null);

        void AddRange<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null);

        Task AddRangeAsync<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null);

        T Get<T>(string key);

        Task<T> GetAsync<T>(string key);

        List<T> Get<T>(IList<string> keys);

        Task<List<T>> GetAsync<T>(IList<string> keys);

        #endregion

        #region set



        #endregion

        #region list



        #endregion

        #region hash

        bool HashAdd<T>(string key, string entityKey, T entity);
        void HashAddRange<T>(string key, Dictionary<string, T> entities);

        bool HashDelete(string key, string entityKey);
        long HashDeleteAll(string key, IList<string> entityKeys);

        T HashGet<T>(string key, string entityKey);
        List<T> HashGetAll<T>(string key);
        List<T> HashGet<T>(string key, IList<string> entityKeys);
        List<string> HashGetAllEntityKeys(string key);
        List<T> HashGetAllEntities<T>(string key);
        long HashCount(string key);

        #endregion

        #region zset



        #endregion
    }
}