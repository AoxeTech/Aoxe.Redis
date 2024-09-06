namespace Aoxe.StackExchangeRedis.Abstractions;

public partial interface IAoxeRedisClient
{
    bool SetAdd<T>(string key, T? value);

    long SetAddRange<T>(string key, ISet<T> values);

    HashSet<T?> SetCombineUnion<T>(string firstKey, string secondKey);

    HashSet<T?> SetCombineUnion<T>(ISet<string> keys);

    HashSet<T?> SetCombineIntersect<T>(string firstKey, string secondKey);

    HashSet<T?> SetCombineIntersect<T>(ISet<string> keys);

    HashSet<T?> SetCombineDifference<T>(string firstKey, string secondKey);

    HashSet<T?> SetCombineDifference<T>(ISet<string> keys);

    long SetCombineAndStoreUnion(string destination, string firstKey, string secondKey);

    long SetCombineAndStoreUnion(string destination, ISet<string> keys);

    long SetCombineAndStoreIntersect(string destination, string firstKey, string secondKey);

    long SetCombineAndStoreIntersect(string destination, ISet<string> keys);

    long SetCombineAndStoreDifference(string destination, string firstKey, string secondKey);

    long SetCombineAndStoreDifference(string destination, ISet<string> keys);

    bool SetContains<T>(string key, T? value);

    long SetLength(string key);

    HashSet<T?> SetMembers<T>(string key);

    bool SetMove<T>(string source, string destination, T? value);

    T? SetPop<T>(string key);

    HashSet<T?> SetPop<T>(string key, long count);

    T? SetRandomMember<T>(string key);

    HashSet<T?> SetRandomMembers<T>(string key, long count);

    bool SetRemove<T>(string key, T? value);

    long SetRemoveRange<T>(string key, ISet<T> values);

    HashSet<T?> SetScan<T>(
        string key,
        T? pattern = default,
        int pageSize = 10,
        long cursor = 0,
        int pageOffset = 0
    );
}
