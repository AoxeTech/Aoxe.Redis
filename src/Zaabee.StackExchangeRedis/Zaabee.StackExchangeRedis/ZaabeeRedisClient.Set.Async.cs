namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient
{
    public async ValueTask<bool> SetAddAsync<T>(string key, T? value) =>
        await db.SetAddAsync(key, ToRedisValue(value));

    public async ValueTask<long> SetAddRangeAsync<T>(string key, IEnumerable<T> values) =>
        await db.SetAddAsync(
            key,
            values.Select(value => (RedisValue)ToRedisValue(value)).ToArray()
        );

    public async ValueTask<List<T?>> SetCombineUnionAsync<T>(string firstKey, string secondKey)
    {
        var values = await db.SetCombineAsync(SetOperation.Union, firstKey, secondKey);
        return values.Select(value => value.HasValue ? FromRedisValue<T>(value) : default).ToList();
    }

    public async ValueTask<List<T?>> SetCombineUnionAsync<T>(IEnumerable<string> keys)
    {
        var values = await db.SetCombineAsync(
            SetOperation.Union,
            keys.Select(key => (RedisKey)key).ToArray()
        );
        return values.Select(value => value.HasValue ? FromRedisValue<T>(value) : default).ToList();
    }

    public async ValueTask<List<T?>> SetCombineIntersectAsync<T>(string firstKey, string secondKey)
    {
        var values = await db.SetCombineAsync(SetOperation.Intersect, firstKey, secondKey);
        return values.Select(value => value.HasValue ? FromRedisValue<T>(value) : default).ToList();
    }

    public async ValueTask<List<T?>> SetCombineIntersectAsync<T>(IEnumerable<string> keys)
    {
        var values = await db.SetCombineAsync(
            SetOperation.Intersect,
            keys.Select(key => (RedisKey)key).ToArray()
        );
        return values.Select(value => value.HasValue ? FromRedisValue<T>(value) : default).ToList();
    }

    public async ValueTask<List<T?>> SetCombineDifferenceAsync<T>(string firstKey, string secondKey)
    {
        var values = await db.SetCombineAsync(SetOperation.Difference, firstKey, secondKey);
        return values.Select(value => value.HasValue ? FromRedisValue<T>(value) : default).ToList();
    }

    public async ValueTask<List<T?>> SetCombineDifferenceAsync<T>(IEnumerable<string> keys)
    {
        var values = await db.SetCombineAsync(
            SetOperation.Difference,
            keys.Select(key => (RedisKey)key).ToArray()
        );
        return values.Select(value => value.HasValue ? FromRedisValue<T>(value) : default).ToList();
    }

    public async ValueTask<long> SetCombineAndStoreUnionAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    ) => await db.SetCombineAndStoreAsync(SetOperation.Union, destination, firstKey, secondKey);

    public async ValueTask<long> SetCombineAndStoreUnionAsync<T>(
        string destination,
        IEnumerable<string> keys
    ) =>
        await db.SetCombineAndStoreAsync(
            SetOperation.Union,
            destination,
            keys.Select(key => (RedisKey)key).ToArray()
        );

    public async ValueTask<long> SetCombineAndStoreIntersectAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    ) => await db.SetCombineAndStoreAsync(SetOperation.Intersect, destination, firstKey, secondKey);

    public async ValueTask<long> SetCombineAndStoreIntersectAsync<T>(
        string destination,
        IEnumerable<string> keys
    ) =>
        await db.SetCombineAndStoreAsync(
            SetOperation.Intersect,
            destination,
            keys.Select(key => (RedisKey)key).ToArray()
        );

    public async ValueTask<long> SetCombineAndStoreDifferenceAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    ) =>
        await db.SetCombineAndStoreAsync(SetOperation.Difference, destination, firstKey, secondKey);

    public async ValueTask<long> SetCombineAndStoreDifferenceAsync<T>(
        string destination,
        IEnumerable<string> keys
    ) =>
        await db.SetCombineAndStoreAsync(
            SetOperation.Difference,
            destination,
            keys.Select(key => (RedisKey)key).ToArray()
        );

    public async ValueTask<bool> SetContainsAsync<T>(string key, T? value) =>
        await db.SetContainsAsync(key, ToRedisValue(value));

    public async ValueTask<long> SetLengthAsync<T>(string key) => await db.SetLengthAsync(key);

    public async ValueTask<List<T?>> SetMembersAsync<T>(string key)
    {
        var results = await db.SetMembersAsync(key);
        return results
            .Select(value => value.HasValue ? FromRedisValue<T>(value) : default)
            .ToList();
    }

    public async ValueTask<bool> SetMoveAsync<T>(string source, string destination, T? value) =>
        await db.SetMoveAsync(source, destination, ToRedisValue(value));

    public async ValueTask<T?> SetPopAsync<T>(string key)
    {
        var value = await db.SetPopAsync(key);
        return value.HasValue ? FromRedisValue<T>(value) : default;
    }

    public async ValueTask<List<T?>> SetPopAsync<T>(string key, long count)
    {
        var values = await db.SetPopAsync(key, count);
        return values.Select(value => value.HasValue ? FromRedisValue<T>(value) : default).ToList();
    }

    public async ValueTask<T?> SetRandomMemberAsync<T>(string key)
    {
        var value = await db.SetRandomMemberAsync(key);
        return value.HasValue ? FromRedisValue<T>(value) : default;
    }

    public async ValueTask<List<T?>> SetRandomMembersAsync<T>(string key, long count)
    {
        var values = await db.SetRandomMembersAsync(key, count);
        return values.Select(value => value.HasValue ? FromRedisValue<T>(value) : default).ToList();
    }

    public async ValueTask<bool> SetRemoveAsync<T>(string key, T? value) =>
        await db.SetRemoveAsync(key, (RedisValue)ToRedisValue(value));

    public async ValueTask<long> SetRemoveRangeAsync<T>(string key, IEnumerable<T> values) =>
        await db.SetRemoveAsync(
            key,
            values.Select(value => (RedisValue)ToRedisValue(value)).ToArray()
        );
}
