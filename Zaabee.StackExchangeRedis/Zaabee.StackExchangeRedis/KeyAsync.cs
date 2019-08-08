using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient
    {
        public async Task<bool> DeleteAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        public async Task<long> DeleteAllAsync(IEnumerable<string> keys)
        {
            return await _db.KeyDeleteAsync(keys.Select(x => (RedisKey) x).ToArray());
        }

        public Task<bool> ExistsAsync(string key)
        {
            return _db.KeyExistsAsync(key);
        }

        public Task<bool> ExpireAsync(string key, TimeSpan? timeSpan)
        {
            return _db.KeyExpireAsync(key, timeSpan);
        }
    }
}