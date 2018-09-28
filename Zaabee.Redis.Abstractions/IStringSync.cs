using System;
using System.Collections.Generic;

namespace Zaabee.Redis.Abstractions
{
    public interface IStringSync
    {
        bool Add<T>(string key, T entity, TimeSpan? expiry = null);

        void AddRange<T>(IEnumerable<Tuple<string, T>> entities, TimeSpan? expiry = null);

        T Get<T>(string key);

        IList<T> Get<T>(IEnumerable<string> keys);
    }
}