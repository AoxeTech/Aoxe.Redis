using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient
    {
        public bool Delete(string key)
        {
            return _db.KeyDelete(key);
        }

        public long DeleteAll(IEnumerable<string> keys)
        {
            return _db.KeyDelete(keys.Select(x => (RedisKey) x).ToArray());
        }

        public bool Exists(string key)
        {
            return _db.KeyExists(key);
        }

        public bool Expire(string key, TimeSpan? timeSpan)
        {
            return _db.KeyExpire(key, timeSpan);
        }
    }
}