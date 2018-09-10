using System;
using System.Collections.Generic;

namespace Zaabee.Redis.Abstractions
{
    public interface IZaabeeRedisClient
    {
        #region string

        void Add<T>(string key, T entity, TimeSpan? expiry = null);

        void AddAsync<T>(string key, T entity, TimeSpan? expiry = null);

        void AddRange<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null);

        void AddRangeAsync<T>(IList<Tuple<string, T>> entities, TimeSpan? expiry = null);

        void Delete(string key);

        void DeleteAsync(string key);

        void DeleteAll(IList<string> keys);

        void DeleteAllAsync(IList<string> keys);

        T Get<T>(string key);

        Dictionary<string, T> Get<T>(IList<string> keys);

        #endregion

        #region set



        #endregion

        #region list



        #endregion

        #region hash



        #endregion

        #region zset



        #endregion
    }
}