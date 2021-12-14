namespace Zaabee.StackExchangeRedis;

public class ZaabeeRedisClient : IZaabeeRedisClient
{
    private IConnectionMultiplexer _conn = null!;
    private IZaabeeRedisDatabase _database = null!;
    private TimeSpan _defaultExpiry;
    private IBytesSerializer _serializer = null!;

    public ZaabeeRedisClient(ZaabeeStackExchangeRedisOptions options) =>
        Init(options.Options, options.DefaultExpiry, options.Serializer);

    private void Init(ConfigurationOptions options, TimeSpan defaultExpiry, IBytesSerializer serializer)
    {
        _conn = ConnectionMultiplexer.Connect(options);
        _database = new ZaabeeRedisDatabase(_conn.GetDatabase(), serializer, defaultExpiry);
        _defaultExpiry = defaultExpiry;
        _serializer = serializer;
    }

    public EndPoint[] GetEndPoints(bool configuredOnly = false) => _conn.GetEndPoints(configuredOnly);

    public string GetStatus() => _conn.GetStatus();

    public int GetHashSlot(string key) => _conn.GetHashSlot(key);

    public string GetStormLog() => _conn.GetStormLog();

    public void ResetStormLog() => _conn.ResetStormLog();

    public IZaabeeRedisDatabase GetDatabase(int db = -1, object? asyncState = null) =>
        new ZaabeeRedisDatabase(_conn.GetDatabase(db, asyncState), _serializer, _defaultExpiry);

    public IZaabeeRedisServer GetServer(string host, int port, object? asyncState = null) =>
        new ZaabeeRedisServer(_conn.GetServer(host, port, asyncState));

    public IZaabeeRedisServer GetServer(string hostAndPort, object? asyncState = null) =>
        new ZaabeeRedisServer(_conn.GetServer(hostAndPort, asyncState));

    public IZaabeeRedisServer GetServer(IPAddress host, int port) =>
        new ZaabeeRedisServer(_conn.GetServer(host, port));

    public IZaabeeRedisServer GetServer(EndPoint endpoint, object? asyncState = null) =>
        new ZaabeeRedisServer(_conn.GetServer(endpoint, asyncState));

    public void Dispose() => _conn.Dispose();

    public bool Delete(string key) => _database.Delete(key);

    public long DeleteAll(IEnumerable<string> keys, bool isBatch = false) => _database.DeleteAll(keys, isBatch);

    public bool Exists(string key) => _database.Exists(key);

    public bool Expire(string key, TimeSpan? timeSpan) => _database.Expire(key, timeSpan);

    public bool Add<T>(string key, T? entity, TimeSpan? expiry = null) => _database.Add(key, entity, expiry);

    public void AddRange<T>(IDictionary<string, T?> entities, TimeSpan? expiry = null, bool isBatch = false) =>
        _database.AddRange(entities, expiry, isBatch);

    public T? Get<T>(string key) => _database.Get<T>(key);

    public IList<T> Get<T>(IEnumerable<string> keys, bool isBatch = false) => _database.Get<T>(keys, isBatch);

    public bool Add(string key, long value, TimeSpan? expiry = null) => _database.Add(key, value, expiry);

    public bool Add(string key, double value, TimeSpan? expiry = null) => _database.Add(key, value, expiry);

    public double Increment(string key, double value) => _database.Increment(key, value);

    public long Increment(string key, long value) => _database.Increment(key, value);

    public T? ListGetByIndex<T>(string key, long index) => _database.ListGetByIndex<T>(key, index);

    public long ListInsertAfter<T>(string key, T? pivot, T? value) => _database.ListInsertAfter(key, pivot, value);

    public long ListInsertBefore<T>(string key, T? pivot, T? value) => _database.ListInsertBefore(key, pivot, value);

    public T? ListLeftPop<T>(string key) => _database.ListLeftPop<T>(key);

    public long ListLeftPush<T>(string key, T? value) => _database.ListLeftPush(key, value);

    public long ListLeftPushRange<T>(string key, IEnumerable<T> values) => _database.ListLeftPushRange(key, values);

    public long ListLength(string key) => _database.ListLength(key);

    public IList<T> ListRange<T>(string key, long start = 0, long stop = -1) =>
        _database.ListRange<T>(key, start, stop);

    public long ListRemove<T>(string key, T? value, long count = 0) => _database.ListRemove(key, value, count);

    public T? ListRightPop<T>(string key) => _database.ListRightPop<T>(key);

    public T? ListRightPopLeftPush<T>(string source, string destination) =>
        _database.ListRightPopLeftPush<T>(source, destination);

    public long ListRightPush<T>(string key, T? value) => _database.ListRightPush(key, value);

    public long ListRightPushRange<T>(string key, IEnumerable<T> values) =>
        _database.ListRightPushRange(key, values);

    public void ListSetByIndex<T>(string key, long index, T? value) => _database.ListSetByIndex(key, index, value);

    public void ListTrim(string key, long start, long stop) => _database.ListTrim(key, start, stop);

    public bool HashAdd<T>(string key, string entityKey, T? entity) => _database.HashAdd(key, entityKey, entity);

    public void HashAddRange<T>(string key, IDictionary<string, T?> entities) =>
        _database.HashAddRange(key, entities);

    public bool HashDelete(string key, string entityKey) => _database.HashDelete(key, entityKey);

    public long HashDeleteRange(string key, IEnumerable<string> entityKeys) =>
        _database.HashDeleteRange(key, entityKeys);

    public T? HashGet<T>(string key, string entityKey) => _database.HashGet<T>(key, entityKey);

    public IList<T> HashGet<T>(string key) => _database.HashGet<T>(key);

    public IList<T> HashGetRange<T>(string key, IEnumerable<string> entityKeys) =>
        _database.HashGetRange<T>(key, entityKeys);

    public IList<string> HashGetAllEntityKeys(string key) => _database.HashGetAllEntityKeys(key);

    public long HashCount(string key) => _database.HashCount(key);

    public bool SetAdd<T>(string key, T? value) => _database.SetAdd(key, value);

    public long SetAddRange<T>(string key, IEnumerable<T> values) => _database.SetAddRange(key, values);

    public IList<T> SetCombineUnion<T>(string firstKey, string secondKey) =>
        _database.SetCombineUnion<T>(firstKey, secondKey);

    public IList<T> SetCombineUnion<T>(IEnumerable<string> keys) => _database.SetCombineUnion<T>(keys);

    public IList<T> SetCombineIntersect<T>(string firstKey, string secondKey) =>
        _database.SetCombineIntersect<T>(firstKey, secondKey);

    public IList<T> SetCombineIntersect<T>(IEnumerable<string> keys) => _database.SetCombineIntersect<T>(keys);

    public IList<T> SetCombineDifference<T>(string firstKey, string secondKey) =>
        _database.SetCombineDifference<T>(firstKey, secondKey);

    public IList<T> SetCombineDifference<T>(IEnumerable<string> keys) => _database.SetCombineDifference<T>(keys);

    public long SetCombineAndStoreUnion<T>(string destination, string firstKey, string secondKey) =>
        _database.SetCombineAndStoreUnion<T>(destination, firstKey, secondKey);

    public long SetCombineAndStoreUnion<T>(string destination, IEnumerable<string> keys) =>
        _database.SetCombineAndStoreUnion<T>(destination, keys);

    public long SetCombineAndStoreIntersect<T>(string destination, string firstKey, string secondKey) =>
        _database.SetCombineAndStoreIntersect<T>(destination, firstKey, secondKey);

    public long SetCombineAndStoreIntersect<T>(string destination, IEnumerable<string> keys) =>
        _database.SetCombineAndStoreIntersect<T>(destination, keys);

    public long SetCombineAndStoreDifference<T>(string destination, string firstKey, string secondKey) =>
        _database.SetCombineAndStoreDifference<T>(destination, firstKey, secondKey);

    public long SetCombineAndStoreDifference<T>(string destination, IEnumerable<string> keys) =>
        _database.SetCombineAndStoreDifference<T>(destination, keys);

    public bool SetContains<T>(string key, T? value) => _database.SetContains(key, value);

    public long SetLength<T>(string key) => _database.SetLength<T>(key);

    public IList<T> SetMembers<T>(string key) => _database.SetMembers<T>(key);

    public bool SetMove<T>(string source, string destination, T? value) =>
        _database.SetMove(source, destination, value);

    public T? SetPop<T>(string key) => _database.SetPop<T>(key);

    public IList<T> SetPop<T>(string key, long count) => _database.SetPop<T>(key, count);

    public T? SetRandomMember<T>(string key) => _database.SetRandomMember<T>(key);

    public IList<T> SetRandomMembers<T>(string key, long count) => _database.SetRandomMembers<T>(key, count);

    public bool SetRemove<T>(string key, T? value) => _database.SetRemove(key, value);

    public long SetRemoveRange<T>(string key, IEnumerable<T> values) => _database.SetRemoveRange(key, values);

    public IList<T> SetScan<T>(string key, T? pattern = default, int pageSize = 10, long cursor = 0,
        int pageOffset = 0) =>
        _database.SetScan(key, pattern, pageSize, cursor, pageOffset);

    public bool SortedSetAdd<T>(string key, T? member, double score) => _database.SortedSetAdd(key, member, score);

    public long SortedSetAdd<T>(string key, IDictionary<T, double> values) => _database.SortedSetAdd(key, values);

    public double SortedSetDecrement<T>(string key, T? member, double value) =>
        _database.SortedSetDecrement(key, member, value);

    public double SortedSetIncrement<T>(string key, T? member, double value) =>
        _database.SortedSetIncrement(key, member, value);

    public long SortedSetLength<T>(string key) => _database.SortedSetLength<T>(key);

    public long SortedSetLengthByValue(string key, int min, int max) =>
        _database.SortedSetLengthByValue(key, min, max);

    public long SortedSetLengthByValue(string key, long min, long max) =>
        _database.SortedSetLengthByValue(key, min, max);

    public long SortedSetLengthByValue(string key, float min, float max) =>
        _database.SortedSetLengthByValue(key, min, max);

    public long SortedSetLengthByValue(string key, double min, double max) =>
        _database.SortedSetLengthByValue(key, min, max);

    public long SortedSetLengthByValue(string key, string min, string max) =>
        _database.SortedSetLengthByValue(key, min, max);

    public IList<T> SortedSetRangeByScoreAscending<T>(string key, double start = 0, double stop = -1) =>
        _database.SortedSetRangeByScoreAscending<T>(key, start, stop);

    public IList<T> SortedSetRangeByScoreDescending<T>(string key, double start = 0, double stop = -1) =>
        _database.SortedSetRangeByScoreDescending<T>(key, start, stop);

    public IDictionary<T, double> SortedSetRangeByScoreWithScoresAscending<T>(string key, double start = 0,
        double stop = -1) =>
        _database.SortedSetRangeByScoreWithScoresAscending<T>(key, start, stop);

    public IDictionary<T, double> SortedSetRangeByScoreWithScoresDescending<T>(string key, double start = 0,
        double stop = -1) =>
        _database.SortedSetRangeByScoreWithScoresDescending<T>(key, start, stop);

    public IList<int> SortedSetRangeByValue(string key, int min, int max, long skip, long take = -1) =>
        _database.SortedSetRangeByValue(key, min, max, skip, take);

    public IList<long> SortedSetRangeByValue(string key, long min, long max, long skip, long take = -1) =>
        _database.SortedSetRangeByValue(key, min, max, skip, take);

    public IList<float> SortedSetRangeByValue(string key, float min, float max, long skip, long take = -1) =>
        _database.SortedSetRangeByValue(key, min, max, skip, take);

    public IList<double> SortedSetRangeByValue(string key, double min, double max, long skip, long take = -1) =>
        _database.SortedSetRangeByValue(key, min, max, skip, take);

    public IList<string> SortedSetRangeByValue(string key, string min, string max, long skip, long take = -1) =>
        _database.SortedSetRangeByValue(key, min, max, skip, take);

    public IList<int> SortedSetRangeByValueAscending(string key, int min = default, int max = default,
        long skip = 0, long take = -1) =>
        _database.SortedSetRangeByValueAscending(key, min, max, skip, take);

    public IList<long> SortedSetRangeByValueAscending(string key, long min = default, long max = default,
        long skip = 0, long take = -1) =>
        _database.SortedSetRangeByValueAscending(key, min, max, skip, take);

    public IList<float> SortedSetRangeByValueAscending(string key, float min = default, float max = default,
        long skip = 0, long take = -1) =>
        _database.SortedSetRangeByValueAscending(key, min, max, skip, take);

    public IList<double> SortedSetRangeByValueAscending(string key, double min = default, double max = default,
        long skip = 0, long take = -1) =>
        _database.SortedSetRangeByValueAscending(key, min, max, skip, take);

    public IList<string> SortedSetRangeByValueAscending(string key, string? min = default, string? max = default,
        long skip = 0, long take = -1) =>
        _database.SortedSetRangeByValueAscending(key, min, max, skip, take);

    public IList<int> SortedSetRangeByValueDescending(string key, int min = default, int max = default,
        long skip = 0, long take = -1) =>
        _database.SortedSetRangeByValueDescending(key, min, max, skip, take);

    public IList<long> SortedSetRangeByValueDescending(string key, long min = default, long max = default,
        long skip = 0, long take = -1) =>
        _database.SortedSetRangeByValueDescending(key, min, max, skip, take);

    public IList<float> SortedSetRangeByValueDescending(string key, float min = default, float max = default,
        long skip = 0, long take = -1) =>
        _database.SortedSetRangeByValueDescending(key, min, max, skip, take);

    public IList<double> SortedSetRangeByValueDescending(string key, double min = default, double max = default,
        long skip = 0, long take = -1) =>
        _database.SortedSetRangeByValueDescending(key, min, max, skip, take);

    public IList<string> SortedSetRangeByValueDescending(string key, string? min = default, string? max = default,
        long skip = 0, long take = -1) =>
        _database.SortedSetRangeByValueDescending(key, min, max, skip, take);

    public bool SortedSetRemove<T>(string key, T? member) => _database.SortedSetRemove(key, member);

    public long SortedSetRemoveRange<T>(string key, IEnumerable<T> members) =>
        _database.SortedSetRemoveRange(key, members);

    public long SortedSetRemoveRangeByScore<T>(string key, double start, double stop) =>
        _database.SortedSetRemoveRangeByScore<T>(key, start, stop);

    public long SortedSetRemoveRangeByValue(string key, int min, int max) =>
        _database.SortedSetRemoveRangeByValue(key, min, max);

    public long SortedSetRemoveRangeByValue(string key, long min, long max) =>
        _database.SortedSetRemoveRangeByValue(key, min, max);

    public long SortedSetRemoveRangeByValue(string key, float min, float max) =>
        _database.SortedSetRemoveRangeByValue(key, min, max);

    public long SortedSetRemoveRangeByValue(string key, double min, double max) =>
        _database.SortedSetRemoveRangeByValue(key, min, max);

    public long SortedSetRemoveRangeByValue(string key, string min, string max) =>
        _database.SortedSetRemoveRangeByValue(key, min, max);

    public IDictionary<T, double> SortedSetScan<T>(string key, T? pattern = default, int pageSize = 10,
        long cursor = 0, int pageOffset = 0) =>
        _database.SortedSetScan(key, pattern, pageSize, cursor, pageOffset);

    public double? SortedSetScore<T>(string key, T? member) => _database.SortedSetScore(key, member);

    public async Task<bool> DeleteAsync(string key) => await _database.DeleteAsync(key);

    public async Task<long> DeleteAllAsync(IEnumerable<string> keys, bool isBatch = false) =>
        await _database.DeleteAllAsync(keys, isBatch);

    public async Task<bool> ExistsAsync(string key) => await _database.ExistsAsync(key);

    public async Task<bool> ExpireAsync(string key, TimeSpan? timeSpan) =>
        await _database.ExpireAsync(key, timeSpan);

    public async Task<bool> AddAsync<T>(string key, T? entity, TimeSpan? expiry = null) =>
        await _database.AddAsync(key, entity, expiry);

    public async Task AddRangeAsync<T>(IDictionary<string, T?> entities, TimeSpan? expiry = null,
        bool isBatch = false) =>
        await _database.AddRangeAsync(entities, expiry, isBatch);

    public async Task<T?> GetAsync<T>(string key) => await _database.GetAsync<T>(key);

    public async Task<IList<T>> GetAsync<T>(IEnumerable<string> keys, bool isBatch = false) =>
        await _database.GetAsync<T>(keys, isBatch);

    public async Task<bool> AddAsync(string key, long value, TimeSpan? expiry = null) =>
        await _database.AddAsync(key, value, expiry);

    public async Task<bool> AddAsync(string key, double value, TimeSpan? expiry = null) =>
        await _database.AddAsync(key, value, expiry);

    public async Task<double> IncrementAsync(string key, double value) =>
        await _database.IncrementAsync(key, value);

    public async Task<long> IncrementAsync(string key, long value) => await _database.IncrementAsync(key, value);

    public async Task<T?> ListGetByIndexAsync<T>(string key, long index) =>
        await _database.ListGetByIndexAsync<T>(key, index);

    public async Task<long> ListInsertAfterAsync<T>(string key, T? pivot, T? value) =>
        await _database.ListInsertAfterAsync(key, pivot, value);

    public async Task<long> ListInsertBeforeAsync<T>(string key, T? pivot, T? value) =>
        await _database.ListInsertBeforeAsync(key, pivot, value);

    public async Task<T?> ListLeftPopAsync<T>(string key) => await _database.ListLeftPopAsync<T>(key);

    public async Task<long> ListLeftPushAsync<T>(string key, T? value) =>
        await _database.ListLeftPushAsync(key, value);

    public async Task<long> ListLeftPushRangeAsync<T>(string key, IEnumerable<T> values) =>
        await _database.ListLeftPushRangeAsync(key, values);

    public async Task<long> ListLengthAsync(string key) => await _database.ListLengthAsync(key);

    public async Task<IList<T>> ListRangeAsync<T>(string key, long start = 0, long stop = -1) =>
        await _database.ListRangeAsync<T>(key, start, stop);

    public async Task<long> ListRemoveAsync<T>(string key, T? value, long count = 0) =>
        await _database.ListRemoveAsync(key, value, count);

    public async Task<T?> ListRightPopAsync<T>(string key) => await _database.ListRightPopAsync<T>(key);

    public async Task<T?> ListRightPopLeftPushAsync<T>(string source, string destination) =>
        await _database.ListRightPopLeftPushAsync<T>(source, destination);

    public async Task<long> ListRightPushAsync<T>(string key, T? value) =>
        await _database.ListRightPushAsync(key, value);

    public async Task<long> ListRightPushRangeAsync<T>(string key, IEnumerable<T> values) =>
        await _database.ListRightPushRangeAsync(key, values);

    public async Task ListSetByIndexAsync<T>(string key, long index, T? value) =>
        await _database.ListSetByIndexAsync(key, index, value);

    public async Task ListTrimAsync(string key, long start, long stop) =>
        await _database.ListTrimAsync(key, start, stop);

    public async Task<bool> HashAddAsync<T>(string key, string entityKey, T? entity) =>
        await _database.HashAddAsync(key, entityKey, entity);

    public async Task HashAddRangeAsync<T>(string key, IDictionary<string, T?> entities) =>
        await _database.HashAddRangeAsync(key, entities);

    public async Task<bool> HashDeleteAsync(string key, string entityKey) =>
        await _database.HashDeleteAsync(key, entityKey);

    public async Task<long> HashDeleteRangeAsync(string key, IEnumerable<string> entityKeys) =>
        await _database.HashDeleteRangeAsync(key, entityKeys);

    public async Task<T?> HashGetAsync<T>(string key, string entityKey) =>
        await _database.HashGetAsync<T>(key, entityKey);

    public async Task<IList<T>> HashGetAsync<T>(string key) => await _database.HashGetAsync<T>(key);

    public async Task<IList<T>> HashGetRangeAsync<T>(string key, IEnumerable<string> entityKeys) =>
        await _database.HashGetRangeAsync<T>(key, entityKeys);

    public async Task<IList<string>> HashGetAllEntityKeysAsync(string key) =>
        await _database.HashGetAllEntityKeysAsync(key);

    public async Task<long> HashCountAsync(string key) => await _database.HashCountAsync(key);

    public async Task<bool> SetAddAsync<T>(string key, T? value) => await _database.SetAddAsync(key, value);

    public async Task<long> SetAddRangeAsync<T>(string key, IEnumerable<T> values) =>
        await _database.SetAddRangeAsync(key, values);

    public async Task<IList<T>> SetCombineUnionAsync<T>(string firstKey, string secondKey) =>
        await _database.SetCombineUnionAsync<T>(firstKey, secondKey);

    public async Task<IList<T>> SetCombineUnionAsync<T>(IEnumerable<string> keys) =>
        await _database.SetCombineUnionAsync<T>(keys);

    public async Task<IList<T>> SetCombineIntersectAsync<T>(string firstKey, string secondKey) =>
        await _database.SetCombineIntersectAsync<T>(firstKey, secondKey);

    public async Task<IList<T>> SetCombineIntersectAsync<T>(IEnumerable<string> keys) =>
        await _database.SetCombineIntersectAsync<T>(keys);

    public async Task<IList<T>> SetCombineDifferenceAsync<T>(string firstKey, string secondKey) =>
        await _database.SetCombineDifferenceAsync<T>(firstKey, secondKey);

    public async Task<IList<T>> SetCombineDifferenceAsync<T>(IEnumerable<string> keys) =>
        await _database.SetCombineDifferenceAsync<T>(keys);

    public async Task<long>
        SetCombineAndStoreUnionAsync<T>(string destination, string firstKey, string secondKey) =>
        await _database.SetCombineAndStoreUnionAsync<T>(destination, firstKey, secondKey);

    public async Task<long> SetCombineAndStoreUnionAsync<T>(string destination, IEnumerable<string> keys) =>
        await _database.SetCombineAndStoreUnionAsync<T>(destination, keys);

    public async Task<long> SetCombineAndStoreIntersectAsync<T>(string destination, string firstKey,
        string secondKey) =>
        await _database.SetCombineAndStoreIntersectAsync<T>(destination, firstKey, secondKey);


    public async Task<long> SetCombineAndStoreIntersectAsync<T>(string destination, IEnumerable<string> keys) =>
        await _database.SetCombineAndStoreIntersectAsync<T>(destination, keys);

    public async Task<long> SetCombineAndStoreDifferenceAsync<T>(string destination, string firstKey,
        string secondKey) =>
        await _database.SetCombineAndStoreDifferenceAsync<T>(destination, firstKey, secondKey);

    public async Task<long> SetCombineAndStoreDifferenceAsync<T>(string destination, IEnumerable<string> keys) =>
        await _database.SetCombineAndStoreDifferenceAsync<T>(destination, keys);

    public async Task<bool> SetContainsAsync<T>(string key, T? value) =>
        await _database.SetContainsAsync(key, value);

    public async Task<long> SetLengthAsync<T>(string key) => await _database.SetLengthAsync<T>(key);

    public async Task<IList<T>> SetMembersAsync<T>(string key) => await _database.SetMembersAsync<T>(key);

    public async Task<bool> SetMoveAsync<T>(string source, string destination, T? value) =>
        await _database.SetMoveAsync(source, destination, value);

    public async Task<T?> SetPopAsync<T>(string key) => await _database.SetPopAsync<T>(key);

    public async Task<IList<T>> SetPopAsync<T>(string key, long count) =>
        await _database.SetPopAsync<T>(key, count);

    public async Task<T?> SetRandomMemberAsync<T>(string key) => await _database.SetRandomMemberAsync<T>(key);

    public async Task<IList<T>> SetRandomMembersAsync<T>(string key, long count) =>
        await _database.SetRandomMembersAsync<T>(key, count);

    public async Task<bool> SetRemoveAsync<T>(string key, T? value) => await _database.SetRemoveAsync(key, value);

    public async Task<long> SetRemoveRangeAsync<T>(string key, IEnumerable<T> values) =>
        await _database.SetRemoveRangeAsync(key, values);

    public async Task<IList<T>> SetScanAsync<T>(string key, T? pattern = default, int pageSize = 10, long cursor = 0,
        int pageOffset = 0) =>
        await _database.SetScanAsync(key, pattern, pageSize, cursor, pageOffset);

    public async Task<bool> SortedSetAddAsync<T>(string key, T? member, double score) =>
        await _database.SortedSetAddAsync(key, member, score);

    public async Task<long> SortedSetAddAsync<T>(string key, IDictionary<T, double> values) =>
        await _database.SortedSetAddAsync(key, values);

    public async Task<double> SortedSetDecrementAsync<T>(string key, T? member, double value) =>
        await _database.SortedSetDecrementAsync(key, member, value);

    public async Task<double> SortedSetIncrementAsync<T>(string key, T? member, double value) =>
        await _database.SortedSetIncrementAsync(key, member, value);

    public async Task<long> SortedSetLengthAsync<T>(string key) => await _database.SortedSetLengthAsync<T>(key);

    public async Task<long> SortedSetLengthByValueAsync(string key, int min, int max) =>
        await _database.SortedSetLengthByValueAsync(key, min, max);

    public async Task<long> SortedSetLengthByValueAsync(string key, long min, long max) =>
        await _database.SortedSetLengthByValueAsync(key, min, max);

    public async Task<long> SortedSetLengthByValueAsync(string key, float min, float max) =>
        await _database.SortedSetLengthByValueAsync(key, min, max);

    public async Task<long> SortedSetLengthByValueAsync(string key, double min, double max) =>
        await _database.SortedSetLengthByValueAsync(key, min, max);

    public async Task<long> SortedSetLengthByValueAsync(string key, string min, string max) =>
        await _database.SortedSetLengthByValueAsync(key, min, max);

    public async Task<IList<T>>
        SortedSetRangeByScoreAscendingAsync<T>(string key, double start = 0, double stop = -1) =>
        await _database.SortedSetRangeByScoreAscendingAsync<T>(key, start, stop);

    public async Task<IList<T>>
        SortedSetRangeByScoreDescendingAsync<T>(string key, double start = 0, double stop = -1) =>
        await _database.SortedSetRangeByScoreDescendingAsync<T>(key, start, stop);

    public async Task<IDictionary<T, double>> SortedSetRangeByScoreWithScoresAscendingAsync<T>(string key,
        long start = 0, long stop = -1) =>
        await _database.SortedSetRangeByScoreWithScoresAscendingAsync<T>(key, start, stop);

    public async Task<IDictionary<T, double>> SortedSetRangeByScoreWithScoresDescendingAsync<T>(string key,
        double start = 0, double stop = -1) =>
        await _database.SortedSetRangeByScoreWithScoresDescendingAsync<T>(key, start, stop);

    public async Task<IList<int>> SortedSetRangeByValueAsync(string key, int min, int max, long skip,
        long take = -1) => await _database.SortedSetRangeByValueAsync(key, min, max, skip, take);

    public async Task<IList<long>> SortedSetRangeByValueAsync(string key, long min, long max, long skip,
        long take = -1) => await _database.SortedSetRangeByValueAsync(key, min, max, skip, take);

    public async Task<IList<float>> SortedSetRangeByValueAsync(string key, float min, float max, long skip,
        long take = -1) => await _database.SortedSetRangeByValueAsync(key, min, max, skip, take);

    public async Task<IList<double>> SortedSetRangeByValueAsync(string key, double min, double max, long skip,
        long take = -1) => await _database.SortedSetRangeByValueAsync(key, min, max, skip, take);

    public async Task<IList<string>> SortedSetRangeByValueAsync(string key, string min, string max, long skip,
        long take = -1) => await _database.SortedSetRangeByValueAsync(key, min, max, skip, take);

    public async Task<IList<int>> SortedSetRangeByValueAscendingAsync(string key, int min = default,
        int max = default, long skip = 0, long take = -1) =>
        await _database.SortedSetRangeByValueAscendingAsync(key, min, max, skip, take);

    public async Task<IList<long>> SortedSetRangeByValueAscendingAsync(string key, long min = default,
        long max = default, long skip = 0, long take = -1) =>
        await _database.SortedSetRangeByValueAscendingAsync(key, min, max, skip, take);

    public async Task<IList<float>> SortedSetRangeByValueAscendingAsync(string key, float min = default,
        float max = default, long skip = 0, long take = -1) =>
        await _database.SortedSetRangeByValueAscendingAsync(key, min, max, skip, take);

    public async Task<IList<double>> SortedSetRangeByValueAscendingAsync(string key, double min = default,
        double max = default, long skip = 0, long take = -1) =>
        await _database.SortedSetRangeByValueAscendingAsync(key, min, max, skip, take);

    public async Task<IList<string>> SortedSetRangeByValueAscendingAsync(string key, string? min = default,
        string? max = default, long skip = 0, long take = -1) =>
        await _database.SortedSetRangeByValueAscendingAsync(key, min, max, skip, take);

    public async Task<IList<int>> SortedSetRangeByValueDescendingAsync(string key, int min = default,
        int max = default, long skip = 0, long take = -1) =>
        await _database.SortedSetRangeByValueDescendingAsync(key, min, max, skip, take);

    public async Task<IList<long>> SortedSetRangeByValueDescendingAsync(string key, long min = default,
        long max = default, long skip = 0, long take = -1) =>
        await _database.SortedSetRangeByValueDescendingAsync(key, min, max, skip, take);

    public async Task<IList<float>> SortedSetRangeByValueDescendingAsync(string key, float min = default,
        float max = default, long skip = 0, long take = -1) =>
        await _database.SortedSetRangeByValueDescendingAsync(key, min, max, skip, take);

    public async Task<IList<double>> SortedSetRangeByValueDescendingAsync(string key, double min = default,
        double max = default, long skip = 0, long take = -1) =>
        await _database.SortedSetRangeByValueDescendingAsync(key, min, max, skip, take);

    public async Task<IList<string>> SortedSetRangeByValueDescendingAsync(string key, string? min = default,
        string? max = default, long skip = 0, long take = -1) =>
        await _database.SortedSetRangeByValueDescendingAsync(key, min, max, skip, take);

    public async Task<bool> SortedSetRemoveAsync<T>(string key, T? member) =>
        await _database.SortedSetRemoveAsync(key, member);

    public async Task<long> SortedSetRemoveRangeAsync<T>(string key, IEnumerable<T> members) =>
        await _database.SortedSetRemoveRangeAsync(key, members);

    public async Task<long> SortedSetRemoveRangeByScoreAsync<T>(string key, double start, double stop) =>
        await _database.SortedSetRemoveRangeByScoreAsync<T>(key, start, stop);

    public async Task<long> SortedSetRemoveRangeByValueAsync(string key, int min, int max) =>
        await _database.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async Task<long> SortedSetRemoveRangeByValueAsync(string key, long min, long max) =>
        await _database.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async Task<long> SortedSetRemoveRangeByValueAsync(string key, float min, float max) =>
        await _database.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async Task<long> SortedSetRemoveRangeByValueAsync(string key, double min, double max) =>
        await _database.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async Task<long> SortedSetRemoveRangeByValueAsync(string key, string min, string max) =>
        await _database.SortedSetRemoveRangeByValueAsync(key, min, max);

    public async Task<IDictionary<T, double>> SortedSetScanAsync<T>(string key, T? pattern = default,
        int pageSize = 10, long cursor = 0, int pageOffset = 0) =>
        await _database.SortedSetScanAsync(key, pattern, pageSize, cursor, pageOffset);

    public async Task<double?> SortedSetScoreAsync<T>(string key, T? member) =>
        await _database.SortedSetScoreAsync(key, member);
}