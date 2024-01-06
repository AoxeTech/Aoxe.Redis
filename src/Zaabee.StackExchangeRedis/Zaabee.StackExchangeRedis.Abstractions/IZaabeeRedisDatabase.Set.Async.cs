namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisDatabase
{
    ValueTask<bool> SetAddAsync<T>(string key, T? value);

    ValueTask<long> SetAddRangeAsync<T>(string key, IEnumerable<T> values);

    ValueTask<List<T?>> SetCombineUnionAsync<T>(string firstKey, string secondKey);

    ValueTask<List<T?>> SetCombineUnionAsync<T>(IEnumerable<string> keys);

    ValueTask<List<T?>> SetCombineIntersectAsync<T>(string firstKey, string secondKey);

    ValueTask<List<T?>> SetCombineIntersectAsync<T>(IEnumerable<string> keys);

    ValueTask<List<T?>> SetCombineDifferenceAsync<T>(string firstKey, string secondKey);

    ValueTask<List<T?>> SetCombineDifferenceAsync<T>(IEnumerable<string> keys);

    ValueTask<long> SetCombineAndStoreUnionAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    );

    ValueTask<long> SetCombineAndStoreUnionAsync<T>(string destination, IEnumerable<string> keys);

    ValueTask<long> SetCombineAndStoreIntersectAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    );

    ValueTask<long> SetCombineAndStoreIntersectAsync<T>(
        string destination,
        IEnumerable<string> keys
    );

    ValueTask<long> SetCombineAndStoreDifferenceAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    );

    ValueTask<long> SetCombineAndStoreDifferenceAsync<T>(
        string destination,
        IEnumerable<string> keys
    );

    ValueTask<bool> SetContainsAsync<T>(string key, T? value);

    ValueTask<long> SetLengthAsync<T>(string key);

    ValueTask<List<T?>> SetMembersAsync<T>(string key);

    ValueTask<bool> SetMoveAsync<T>(string source, string destination, T? value);

    ValueTask<T?> SetPopAsync<T>(string key);

    ValueTask<List<T?>> SetPopAsync<T>(string key, long count);

    ValueTask<T?> SetRandomMemberAsync<T>(string key);

    ValueTask<List<T?>> SetRandomMembersAsync<T>(string key, long count);

    ValueTask<bool> SetRemoveAsync<T>(string key, T? value);

    ValueTask<long> SetRemoveRangeAsync<T>(string key, IEnumerable<T> values);
}
