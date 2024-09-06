namespace Aoxe.StackExchangeRedis.Client;

public partial class AoxeRedisClient
{
    public bool SetAdd<T>(string key, T? value) => db.SetAdd(key, ToRedisValue(value));

    public long SetAddRange<T>(string key, ISet<T> values) =>
        db.SetAdd(key, values.Select(ToRedisValue).ToArray());

    public HashSet<T?> SetCombineUnion<T>(string firstKey, string secondKey) =>
        [
            .. db.SetCombine(SetOperation.Union, firstKey, secondKey)
                .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
        ];

    public HashSet<T?> SetCombineUnion<T>(ISet<string> keys) =>
        [
            .. db.SetCombine(SetOperation.Union, keys.Select(key => (RedisKey)key).ToArray())
                .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
        ];

    public HashSet<T?> SetCombineIntersect<T>(string firstKey, string secondKey) =>
        [
            .. db.SetCombine(SetOperation.Intersect, firstKey, secondKey)
                .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
        ];

    public HashSet<T?> SetCombineIntersect<T>(ISet<string> keys) =>
        [
            .. db.SetCombine(SetOperation.Intersect, keys.Select(key => (RedisKey)key).ToArray())
                .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
        ];

    public HashSet<T?> SetCombineDifference<T>(string firstKey, string secondKey) =>
        [
            .. db.SetCombine(SetOperation.Difference, firstKey, secondKey)
                .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
        ];

    public HashSet<T?> SetCombineDifference<T>(ISet<string> keys) =>
        [
            .. db.SetCombine(SetOperation.Difference, keys.Select(key => (RedisKey)key).ToArray())
                .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
        ];

    public long SetCombineAndStoreUnion(string destination, string firstKey, string secondKey) =>
        db.SetCombineAndStore(SetOperation.Union, destination, firstKey, secondKey);

    public long SetCombineAndStoreUnion(string destination, ISet<string> keys) =>
        db.SetCombineAndStore(
            SetOperation.Union,
            destination,
            keys.Select(key => (RedisKey)key).ToArray()
        );

    public long SetCombineAndStoreIntersect(
        string destination,
        string firstKey,
        string secondKey
    ) => db.SetCombineAndStore(SetOperation.Intersect, destination, firstKey, secondKey);

    public long SetCombineAndStoreIntersect(string destination, ISet<string> keys) =>
        db.SetCombineAndStore(
            SetOperation.Intersect,
            destination,
            keys.Select(key => (RedisKey)key).ToArray()
        );

    public long SetCombineAndStoreDifference(
        string destination,
        string firstKey,
        string secondKey
    ) => db.SetCombineAndStore(SetOperation.Difference, destination, firstKey, secondKey);

    public long SetCombineAndStoreDifference(string destination, ISet<string> keys) =>
        db.SetCombineAndStore(
            SetOperation.Difference,
            destination,
            keys.Select(key => (RedisKey)key).ToArray()
        );

    public bool SetContains<T>(string key, T? value) => db.SetContains(key, ToRedisValue(value));

    public long SetLength(string key) => db.SetLength(key);

    public HashSet<T?> SetMembers<T>(string key) =>
        [
            .. db.SetMembers(key)
                .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
        ];

    public bool SetMove<T>(string source, string destination, T? value) =>
        db.SetMove(source, destination, ToRedisValue(value));

    public T? SetPop<T>(string key)
    {
        var value = db.SetPop(key);
        return value.HasValue ? FromRedisValue<T>(value) : default;
    }

    public HashSet<T?> SetPop<T>(string key, long count) =>
        [
            .. db.SetPop(key, count)
                .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
        ];

    public T? SetRandomMember<T>(string key)
    {
        var value = db.SetRandomMember(key);
        return value.HasValue ? FromRedisValue<T>(value) : default;
    }

    public HashSet<T?> SetRandomMembers<T>(string key, long count) =>
        [
            .. db.SetRandomMembers(key, count)
                .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
        ];

    public bool SetRemove<T>(string key, T? value) => db.SetRemove(key, ToRedisValue(value));

    public long SetRemoveRange<T>(string key, ISet<T> values) =>
        db.SetRemove(key, values.Select(ToRedisValue).ToArray());

    public HashSet<T?> SetScan<T>(
        string key,
        T? pattern = default,
        int pageSize = 10,
        long cursor = 0,
        int pageOffset = 0
    ) =>
        [
            .. db.SetScan(key, ToRedisValue(pattern), pageSize, cursor, pageOffset)
                .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
        ];
}
