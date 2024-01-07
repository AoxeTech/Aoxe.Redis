namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisClient
{
    bool SetAdd<T>(string key, T? value);

    long SetAddRange<T>(string key, IEnumerable<T> values);

    List<T?> SetCombineUnion<T>(string firstKey, string secondKey);

    List<T?> SetCombineUnion<T>(IEnumerable<string> keys);

    List<T?> SetCombineIntersect<T>(string firstKey, string secondKey);

    List<T?> SetCombineIntersect<T>(IEnumerable<string> keys);

    List<T?> SetCombineDifference<T>(string firstKey, string secondKey);

    List<T?> SetCombineDifference<T>(IEnumerable<string> keys);

    long SetCombineAndStoreUnion(string destination, string firstKey, string secondKey);

    long SetCombineAndStoreUnion(string destination, IEnumerable<string> keys);

    long SetCombineAndStoreIntersect(string destination, string firstKey, string secondKey);

    long SetCombineAndStoreIntersect(string destination, IEnumerable<string> keys);

    long SetCombineAndStoreDifference(string destination, string firstKey, string secondKey);

    long SetCombineAndStoreDifference(string destination, IEnumerable<string> keys);

    bool SetContains<T>(string key, T? value);

    long SetLength(string key);

    List<T?> SetMembers<T>(string key);

    bool SetMove<T>(string source, string destination, T? value);

    T? SetPop<T>(string key);

    List<T?> SetPop<T>(string key, long count);

    T? SetRandomMember<T>(string key);

    List<T?> SetRandomMembers<T>(string key, long count);

    bool SetRemove<T>(string key, T? value);

    long SetRemoveRange<T>(string key, IEnumerable<T> values);

    List<T?> SetScan<T>(
        string key,
        T? pattern = default,
        int pageSize = 10,
        long cursor = 0,
        int pageOffset = 0
    );
}
