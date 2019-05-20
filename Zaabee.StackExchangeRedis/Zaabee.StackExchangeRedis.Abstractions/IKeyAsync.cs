using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface IKeyAsync
    {
        Task<bool> DeleteAsync(string key);

        Task<long> DeleteAllAsync(IEnumerable<string> keys);

        Task<bool> ExistsAsync(string key);

        Task<bool> ExpireAsync(string key, TimeSpan? timeSpan);
    }
}