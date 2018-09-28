using System;
using System.Collections.Generic;

namespace Zaabee.Redis.Abstractions
{
    public interface IKeySync
    {
        bool Delete(string key);

        long DeleteAll(IEnumerable<string> keys);

        bool Exists(string key);

        bool Expire(string key, TimeSpan? timeSpan);
    }
}