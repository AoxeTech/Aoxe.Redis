namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient
{
    public bool SetAdd<T>(string key, T? value) =>
        db.SetAdd(key, (RedisValue)serializer.ToBytes(value));

    public long SetAddRange<T>(string key, IEnumerable<T> values) =>
        db.SetAdd(key, values.Select(value => (RedisValue)serializer.ToBytes(value)).ToArray());

    public List<T?> SetCombineUnion<T>(string firstKey, string secondKey) =>
        db.SetCombine(SetOperation.Union, firstKey, secondKey)
            .Select(value => value.HasValue ? serializer.FromBytes<T>(value) : default)
            .ToList();

    public List<T?> SetCombineUnion<T>(IEnumerable<string> keys) =>
        db.SetCombine(SetOperation.Union, keys.Select(key => (RedisKey)key).ToArray())
            .Select(value => value.HasValue ? serializer.FromBytes<T>(value) : default)
            .ToList();

    public List<T?> SetCombineIntersect<T>(string firstKey, string secondKey) =>
        db.SetCombine(SetOperation.Intersect, firstKey, secondKey)
            .Select(value => value.HasValue ? serializer.FromBytes<T>(value) : default)
            .ToList();

    public List<T?> SetCombineIntersect<T>(IEnumerable<string> keys) =>
        db.SetCombine(SetOperation.Intersect, keys.Select(key => (RedisKey)key).ToArray())
            .Select(value => value.HasValue ? serializer.FromBytes<T>(value) : default)
            .ToList();

    public List<T?> SetCombineDifference<T>(string firstKey, string secondKey) =>
        db.SetCombine(SetOperation.Difference, firstKey, secondKey)
            .Select(value => value.HasValue ? serializer.FromBytes<T>(value) : default)
            .ToList();

    public List<T?> SetCombineDifference<T>(IEnumerable<string> keys) =>
        db.SetCombine(SetOperation.Difference, keys.Select(key => (RedisKey)key).ToArray())
            .Select(value => value.HasValue ? serializer.FromBytes<T>(value) : default)
            .ToList();

    public long SetCombineAndStoreUnion(string destination, string firstKey, string secondKey) =>
        db.SetCombineAndStore(SetOperation.Union, destination, firstKey, secondKey);

    public long SetCombineAndStoreUnion(string destination, IEnumerable<string> keys) =>
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

    public long SetCombineAndStoreIntersect(string destination, IEnumerable<string> keys) =>
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

    public long SetCombineAndStoreDifference(string destination, IEnumerable<string> keys) =>
        db.SetCombineAndStore(
            SetOperation.Difference,
            destination,
            keys.Select(key => (RedisKey)key).ToArray()
        );

    public bool SetContains<T>(string key, T? value) =>
        db.SetContains(key, serializer.ToBytes(value));

    public long SetLength(string key) => db.SetLength(key);

    public List<T?> SetMembers<T>(string key) =>
        db.SetMembers(key)
            .Select(value => value.HasValue ? serializer.FromBytes<T>(value) : default)
            .ToList();

    public bool SetMove<T>(string source, string destination, T? value) =>
        db.SetMove(source, destination, serializer.ToBytes(value));

    public T? SetPop<T>(string key)
    {
        var value = db.SetPop(key);
        return value.HasValue ? serializer.FromBytes<T>(value) : default;
    }

    public List<T?> SetPop<T>(string key, long count) =>
        db.SetPop(key, count)
            .Select(value => value.HasValue ? serializer.FromBytes<T>(value) : default)
            .ToList();

    public T? SetRandomMember<T>(string key)
    {
        var value = db.SetRandomMember(key);
        return value.HasValue ? serializer.FromBytes<T>(value) : default;
    }

    public List<T?> SetRandomMembers<T>(string key, long count) =>
        db.SetRandomMembers(key, count)
            .Select(value => value.HasValue ? serializer.FromBytes<T>(value) : default)
            .ToList();

    public bool SetRemove<T>(string key, T? value) => db.SetRemove(key, serializer.ToBytes(value));

    public long SetRemoveRange<T>(string key, IEnumerable<T> values) =>
        db.SetRemove(key, values.Select(value => (RedisValue)serializer.ToBytes(value)).ToArray());

    public List<T?> SetScan<T>(
        string key,
        T? pattern = default,
        int pageSize = 10,
        long cursor = 0,
        int pageOffset = 0
    ) =>
        db.SetScan(key, serializer.ToBytes(pattern), pageSize, cursor, pageOffset)
            .Select(value => value.HasValue ? serializer.FromBytes<T>(value) : default)
            .ToList();
}
