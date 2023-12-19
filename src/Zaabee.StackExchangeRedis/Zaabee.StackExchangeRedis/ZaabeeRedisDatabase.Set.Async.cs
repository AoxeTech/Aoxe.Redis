namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public async ValueTask<bool> SetAddAsync<T>(string key, T? value) =>
        await _db.SetAddAsync(key, _serializer.ToBytes(value));

    public async ValueTask<long> SetAddRangeAsync<T>(string key, IEnumerable<T> values) =>
        await _db.SetAddAsync(
            key,
            values.Select(value => (RedisValue)_serializer.ToBytes(value)).ToArray()
        );

    public async ValueTask<List<T>> SetCombineUnionAsync<T>(string firstKey, string secondKey)
    {
        var values = await _db.SetCombineAsync(SetOperation.Union, firstKey, secondKey);
        return values
            .Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }

    public async ValueTask<List<T>> SetCombineUnionAsync<T>(IEnumerable<string> keys)
    {
        var values = await _db.SetCombineAsync(
            SetOperation.Union,
            keys.Select(key => (RedisKey)key).ToArray()
        );
        return values
            .Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }

    public async ValueTask<List<T>> SetCombineIntersectAsync<T>(string firstKey, string secondKey)
    {
        var values = await _db.SetCombineAsync(SetOperation.Intersect, firstKey, secondKey);
        return values
            .Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }

    public async ValueTask<List<T>> SetCombineIntersectAsync<T>(IEnumerable<string> keys)
    {
        var values = await _db.SetCombineAsync(
            SetOperation.Intersect,
            keys.Select(key => (RedisKey)key).ToArray()
        );
        return values
            .Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }

    public async ValueTask<List<T>> SetCombineDifferenceAsync<T>(string firstKey, string secondKey)
    {
        var values = await _db.SetCombineAsync(SetOperation.Difference, firstKey, secondKey);
        return values
            .Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }

    public async ValueTask<List<T>> SetCombineDifferenceAsync<T>(IEnumerable<string> keys)
    {
        var values = await _db.SetCombineAsync(
            SetOperation.Difference,
            keys.Select(key => (RedisKey)key).ToArray()
        );
        return values
            .Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }

    public async ValueTask<long> SetCombineAndStoreUnionAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    ) => await _db.SetCombineAndStoreAsync(SetOperation.Union, destination, firstKey, secondKey);

    public async ValueTask<long> SetCombineAndStoreUnionAsync<T>(
        string destination,
        IEnumerable<string> keys
    ) =>
        await _db.SetCombineAndStoreAsync(
            SetOperation.Union,
            destination,
            keys.Select(key => (RedisKey)key).ToArray()
        );

    public async ValueTask<long> SetCombineAndStoreIntersectAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    ) =>
        await _db.SetCombineAndStoreAsync(SetOperation.Intersect, destination, firstKey, secondKey);

    public async ValueTask<long> SetCombineAndStoreIntersectAsync<T>(
        string destination,
        IEnumerable<string> keys
    ) =>
        await _db.SetCombineAndStoreAsync(
            SetOperation.Intersect,
            destination,
            keys.Select(key => (RedisKey)key).ToArray()
        );

    public async ValueTask<long> SetCombineAndStoreDifferenceAsync<T>(
        string destination,
        string firstKey,
        string secondKey
    ) =>
        await _db.SetCombineAndStoreAsync(
            SetOperation.Difference,
            destination,
            firstKey,
            secondKey
        );

    public async ValueTask<long> SetCombineAndStoreDifferenceAsync<T>(
        string destination,
        IEnumerable<string> keys
    ) =>
        await _db.SetCombineAndStoreAsync(
            SetOperation.Difference,
            destination,
            keys.Select(key => (RedisKey)key).ToArray()
        );

    public async ValueTask<bool> SetContainsAsync<T>(string key, T? value) =>
        await _db.SetContainsAsync(key, _serializer.ToBytes(value));

    public async ValueTask<long> SetLengthAsync<T>(string key) => await _db.SetLengthAsync(key);

    public async ValueTask<List<T>> SetMembersAsync<T>(string key)
    {
        var results = await _db.SetMembersAsync(key);
        return results
            .Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }

    public async ValueTask<bool> SetMoveAsync<T>(string source, string destination, T? value) =>
        await _db.SetMoveAsync(source, destination, _serializer.ToBytes(value));

    public async ValueTask<T?> SetPopAsync<T>(string key)
    {
        var value = await _db.SetPopAsync(key);
        return value.HasValue ? _serializer.FromBytes<T>(value) : default;
    }

    public async ValueTask<List<T>> SetPopAsync<T>(string key, long count)
    {
        var values = await _db.SetPopAsync(key, count);
        return values
            .Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }

    public async ValueTask<T?> SetRandomMemberAsync<T>(string key)
    {
        var value = await _db.SetRandomMemberAsync(key);
        return value.HasValue ? _serializer.FromBytes<T>(value) : default;
    }

    public async ValueTask<List<T>> SetRandomMembersAsync<T>(string key, long count)
    {
        var values = await _db.SetRandomMembersAsync(key, count);
        return values
            .Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }

    public async ValueTask<bool> SetRemoveAsync<T>(string key, T? value) =>
        await _db.SetRemoveAsync(key, (RedisValue)_serializer.ToBytes(value));

    public async ValueTask<long> SetRemoveRangeAsync<T>(string key, IEnumerable<T> values) =>
        await _db.SetRemoveAsync(
            key,
            values.Select(value => (RedisValue)_serializer.ToBytes(value)).ToArray()
        );

    public async ValueTask<List<T>> SetScanAsync<T>(
        string key,
        T? pattern = default,
        int pageSize = 10,
        long cursor = 0,
        int pageOffset = 0
    )
    {
        var values = await ValueTask
            .Factory
            .StartNew(
                () => _db.SetScan(key, _serializer.ToBytes(pattern), pageSize, cursor, pageOffset)
            );
        return values
            .Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }
}
