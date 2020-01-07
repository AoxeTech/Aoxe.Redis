using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient
    {
        public async Task<bool> DeleteAsync(string key) => await _db.KeyDeleteAsync(key);

        public async Task<long> DeleteAllAsync(IEnumerable<string> keys, bool isBatch = false)
        {
            if (isBatch) await _db.KeyDeleteAsync(keys.Select(x => (RedisKey) x).ToArray());
            var result = 0;
            foreach (var key in keys)
                if (await DeleteAsync(key))
                    result++;

            return result;
        }

        public Task<bool> ExistsAsync(string key) => _db.KeyExistsAsync(key);

        public Task<bool> ExpireAsync(string key, TimeSpan? timeSpan) => _db.KeyExpireAsync(key, timeSpan);
    }
}