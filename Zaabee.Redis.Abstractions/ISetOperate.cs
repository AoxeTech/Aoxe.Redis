using System.Collections.Generic;

namespace Zaabee.Redis.Abstractions
{
    public interface ISetOperate
    {
        bool SetAdd<T>(string key, T value);

        long SetAdd<T>(string key, List<T> values);

        List<T> SetCombineUnion<T>(string firstKey, string secondKey);

        List<T> SetCombineUnion<T>(string[] keys);

        List<T> SetCombineIntersect<T>(string firstKey, string secondKey);

        List<T> SetCombineIntersect<T>(string[] keys);

        List<T> SetCombineDifference<T>(string firstKey, string secondKey);

        List<T> SetCombineDifference<T>(string[] keys);

        long SetCombineAndStoreUnion<T>(string destination, string firstKey, string secondKey);

        long SetCombineAndStoreUnion<T>(string destination, string[] keys);

        long SetCombineAndStoreIntersect<T>(string destination, string firstKey, string secondKey);

        long SetCombineAndStoreIntersect<T>(string destination, string[] keys);

        long SetCombineAndStoreDifference<T>(string destination, string firstKey, string secondKey);

        long SetCombineAndStoreDifference<T>(string destination, string[] keys);

        bool SetContains<T>(string key, T value);

        long SetLength<T>(string key);

        List<T> SetMembers<T>(string key);

        bool SetMove<T>(string source, string destination, T value);

        T SetPop<T>(string key);

        List<T> SetPop<T>(string key, long count);

        T SetRandomMember<T>(string key);

        List<T> SetRandomMembers<T>(string key, long count);

        bool SetRemove<T>(string key, T value);

        long SetRemove<T>(string key, List<T> values);

        IEnumerable<T> SetScan<T>(string key, T pattern = default(T), int pageSize = 10,
            long cursor = 0, int pageOffset = 0);

    }
}