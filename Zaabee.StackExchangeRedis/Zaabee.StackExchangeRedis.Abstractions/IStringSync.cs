using System;
using System.Collections.Generic;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface IStringSync
    {
        bool Add<T>(string key, T entity, TimeSpan? expiry = null);
        
        void AddRange<T>(IEnumerable<Tuple<string, T>> entities, TimeSpan? expiry = null);
        
        T Get<T>(string key);
        
        IList<T> Get<T>(IEnumerable<string> keys);

        bool Add(string key, long value, TimeSpan? expiry = null);
        
        bool Add(string key, double value, TimeSpan? expiry = null);
        
        double Increment(string key, double value);
        
        long Increment(string key, long value);
    }
}