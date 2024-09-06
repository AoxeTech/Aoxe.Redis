namespace Aoxe.StackExchangeRedis.Abstractions;

public partial interface IAoxeRedisClient
{
    ValueTask<bool> SetAddAsync<T>(string key, T? value);

    ValueTask<long> SetAddRangeAsync<T>(string key, ISet<T> values);

    ValueTask<HashSet<T?>> SetCombineUnionAsync<T>(string firstKey, string secondKey);

    ValueTask<HashSet<T?>> SetCombineUnionAsync<T>(ISet<string> keys);

    ValueTask<HashSet<T?>> SetCombineIntersectAsync<T>(string firstKey, string secondKey);

    ValueTask<HashSet<T?>> SetCombineIntersectAsync<T>(ISet<string> keys);

    ValueTask<HashSet<T?>> SetCombineDifferenceAsync<T>(string firstKey, string secondKey);

    ValueTask<HashSet<T?>> SetCombineDifferenceAsync<T>(ISet<string> keys);

    ValueTask<long> SetCombineAndStoreUnionAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    );

    ValueTask<long> SetCombineAndStoreUnionAsync<T>(string destination, ISet<string> keys);

    ValueTask<long> SetCombineAndStoreIntersectAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    );

    ValueTask<long> SetCombineAndStoreIntersectAsync<T>(string destination, ISet<string> keys);

    ValueTask<long> SetCombineAndStoreDifferenceAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    );

    ValueTask<long> SetCombineAndStoreDifferenceAsync<T>(string destination, ISet<string> keys);

    ValueTask<bool> SetContainsAsync<T>(string key, T? value);

    ValueTask<long> SetLengthAsync<T>(string key);

    ValueTask<HashSet<T?>> SetMembersAsync<T>(string key);

    ValueTask<bool> SetMoveAsync<T>(string source, string destination, T? value);

    ValueTask<T?> SetPopAsync<T>(string key);

    ValueTask<HashSet<T?>> SetPopAsync<T>(string key, long count);

    ValueTask<T?> SetRandomMemberAsync<T>(string key);

    ValueTask<HashSet<T?>> SetRandomMembersAsync<T>(string key, long count);

    ValueTask<bool> SetRemoveAsync<T>(string key, T? value);

    ValueTask<long> SetRemoveRangeAsync<T>(string key, ISet<T> values);
}
