using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.Redis.Abstractions
{
    public interface IKeyOperate
    {
        bool Delete(string key);

        Task<bool> DeleteAsync(string key);

        long DeleteAll(IList<string> keys);

        Task<long> DeleteAllAsync(IList<string> keys);

        bool Exists(string key);

        Task<bool> ExistsAsync(string key);

        bool Expire(string key, TimeSpan? timeSpan);

        Task<bool> ExpireAsync(string key, TimeSpan? timeSpan);
    }
}