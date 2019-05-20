using System.Collections.Generic;

namespace Zaabee.StackExchangeRedis.Abstractions
{
    public interface ISetSync
    {
        bool SetAdd<T>(string key, T value);

        long SetAddRange<T>(string key, IEnumerable<T> values);

        IList<T> SetCombineUnion<T>(string firstKey, string secondKey);

        IList<T> SetCombineUnion<T>(IEnumerable<string> keys);

        IList<T> SetCombineIntersect<T>(string firstKey, string secondKey);

        IList<T> SetCombineIntersect<T>(IEnumerable<string> keys);

        IList<T> SetCombineDifference<T>(string firstKey, string secondKey);

        IList<T> SetCombineDifference<T>(IEnumerable<string> keys);

        long SetCombineAndStoreUnion<T>(string destination, string firstKey, string secondKey);

        long SetCombineAndStoreUnion<T>(string destination, IEnumerable<string> keys);

        long SetCombineAndStoreIntersect<T>(string destination, string firstKey, string secondKey);

        long SetCombineAndStoreIntersect<T>(string destination, IEnumerable<string> keys);

        long SetCombineAndStoreDifference<T>(string destination, string firstKey, string secondKey);

        long SetCombineAndStoreDifference<T>(string destination, IEnumerable<string> keys);

        bool SetContains<T>(string key, T value);

        long SetLength<T>(string key);

        IList<T> SetMembers<T>(string key);

        bool SetMove<T>(string source, string destination, T value);

        T SetPop<T>(string key);

        IList<T> SetPop<T>(string key, long count);

        T SetRandomMember<T>(string key);

        IList<T> SetRandomMembers<T>(string key, long count);

        bool SetRemove<T>(string key, T value);

        long SetRemoveRange<T>(string key, IEnumerable<T> values);

        IList<T> SetScan<T>(string key, T pattern = default(T), int pageSize = 10,
            long cursor = 0, int pageOffset = 0);

    }
}