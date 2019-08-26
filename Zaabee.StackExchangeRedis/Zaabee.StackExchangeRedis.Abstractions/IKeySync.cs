using System;
using System.Collections.Generic;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface IKeySync
    {
        bool Delete(string key);

        long DeleteAll(IEnumerable<string> keys, bool isBatch = false);

        bool Exists(string key);

        bool Expire(string key, TimeSpan? timeSpan);
    }
}