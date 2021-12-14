namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public async Task<bool> SetAddAsync<T>(string key, T? value) =>
        await _db.SetAddAsync(key, _serializer.ToBytes(value));

    public async Task<long> SetAddRangeAsync<T>(string key, IEnumerable<T> values) => await _db.SetAddAsync(key,
        values.Select(value => (RedisValue) _serializer.ToBytes(value)).ToArray());

    public async Task<IList<T>> SetCombineUnionAsync<T>(string firstKey, string secondKey)
    {
        var values = await _db.SetCombineAsync(SetOperation.Union, firstKey, secondKey);
        return values.Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default).ToList();
    }

    public async Task<IList<T>> SetCombineUnionAsync<T>(IEnumerable<string> keys)
    {
        var values = await _db.SetCombineAsync(SetOperation.Union, keys.Select(key => (RedisKey) key).ToArray());
        return values.Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default).ToList();
    }

    public async Task<IList<T>> SetCombineIntersectAsync<T>(string firstKey, string secondKey)
    {
        var values = await _db.SetCombineAsync(SetOperation.Intersect, firstKey, secondKey);
        return values.Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default).ToList();
    }

    public async Task<IList<T>> SetCombineIntersectAsync<T>(IEnumerable<string> keys)
    {
        var values =
            await _db.SetCombineAsync(SetOperation.Intersect, keys.Select(key => (RedisKey) key).ToArray());
        return values.Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default).ToList();
    }

    public async Task<IList<T>> SetCombineDifferenceAsync<T>(string firstKey, string secondKey)
    {
        var values = await _db.SetCombineAsync(SetOperation.Difference, firstKey, secondKey);
        return values.Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default).ToList();
    }

    public async Task<IList<T>> SetCombineDifferenceAsync<T>(IEnumerable<string> keys)
    {
        var values =
            await _db.SetCombineAsync(SetOperation.Difference, keys.Select(key => (RedisKey) key).ToArray());
        return values.Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default).ToList();
    }

    public async Task<long>
        SetCombineAndStoreUnionAsync<T>(string destination, string firstKey, string secondKey) =>
        await _db.SetCombineAndStoreAsync(SetOperation.Union, destination, firstKey, secondKey);

    public async Task<long> SetCombineAndStoreUnionAsync<T>(string destination, IEnumerable<string> keys) =>
        await _db.SetCombineAndStoreAsync(SetOperation.Union, destination,
            keys.Select(key => (RedisKey) key).ToArray());

    public async Task<long> SetCombineAndStoreIntersectAsync<T>(string destination, string firstKey,
        string secondKey) =>
        await _db.SetCombineAndStoreAsync(SetOperation.Intersect, destination, firstKey, secondKey);

    public async Task<long> SetCombineAndStoreIntersectAsync<T>(string destination, IEnumerable<string> keys) =>
        await _db.SetCombineAndStoreAsync(SetOperation.Intersect, destination,
            keys.Select(key => (RedisKey) key).ToArray());

    public async Task<long> SetCombineAndStoreDifferenceAsync<T>(string destination, string firstKey,
        string secondKey) =>
        await _db.SetCombineAndStoreAsync(SetOperation.Difference, destination, firstKey, secondKey);

    public async Task<long> SetCombineAndStoreDifferenceAsync<T>(string destination, IEnumerable<string> keys) =>
        await _db.SetCombineAndStoreAsync(SetOperation.Difference, destination,
            keys.Select(key => (RedisKey) key).ToArray());

    public async Task<bool> SetContainsAsync<T>(string key, T? value) =>
        await _db.SetContainsAsync(key, _serializer.ToBytes(value));

    public async Task<long> SetLengthAsync<T>(string key) => await _db.SetLengthAsync(key);

    public async Task<IList<T>> SetMembersAsync<T>(string key)
    {
        var results = await _db.SetMembersAsync(key);
        return results.Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default)
            .ToList();
    }

    public async Task<bool> SetMoveAsync<T>(string source, string destination, T? value) =>
        await _db.SetMoveAsync(source, destination, _serializer.ToBytes(value));

    public async Task<T?> SetPopAsync<T>(string key)
    {
        var value = await _db.SetPopAsync(key);
        return value.HasValue ? _serializer.FromBytes<T>(value) : default;
    }

    public async Task<IList<T>> SetPopAsync<T>(string key, long count)
    {
        var values = await _db.SetPopAsync(key, count);
        return values.Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default).ToList();
    }

    public async Task<T?> SetRandomMemberAsync<T>(string key)
    {
        var value = await _db.SetRandomMemberAsync(key);
        return value.HasValue ? _serializer.FromBytes<T>(value) : default;
    }

    public async Task<IList<T>> SetRandomMembersAsync<T>(string key, long count)
    {
        var values = await _db.SetRandomMembersAsync(key, count);
        return values.Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default).ToList();
    }

    public async Task<bool> SetRemoveAsync<T>(string key, T? value) =>
        await _db.SetRemoveAsync(key, (RedisValue) _serializer.ToBytes(value));

    public async Task<long> SetRemoveRangeAsync<T>(string key, IEnumerable<T> values) =>
        await _db.SetRemoveAsync(key, values.Select(value => (RedisValue) _serializer.ToBytes(value)).ToArray());

    public async Task<IList<T>> SetScanAsync<T>(string key, T? pattern = default, int pageSize = 10,
        long cursor = 0, int pageOffset = 0)
    {
        var values = await Task.Factory.StartNew(() =>
            _db.SetScan(key, _serializer.ToBytes(pattern), pageSize, cursor, pageOffset));
        return values.Select(value => value.HasValue ? _serializer.FromBytes<T>(value) : default).ToList();
    }
}