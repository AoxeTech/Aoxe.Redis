namespace Zaabee.StackExchangeRedis.TestProject;

public class HashOperateTest
{
    private readonly IZaabeeRedisClient _client = ZaabeeRedisClientFactory.GetClient();

    [Fact]
    public void HashSync()
    {
        _client.Delete("HashTest");
        var testModel = TestModelFactory.CreateTestModel();
        Assert.True(_client.HashAdd("HashTest", testModel.Id.ToString(), testModel));
        var result = _client.HashGet<TestModel>("HashTest", testModel.Id.ToString());
        Assert.Equal(testModel, result);
        Assert.True(_client.HashDelete("HashTest", result.Id.ToString()));
        _client.Delete("HashTest");
    }

    [Fact]
    public async void HashAsync()
    {
        await _client.DeleteAsync("HashAsyncTest");
        var testModel = TestModelFactory.CreateTestModel();
        Assert.True(
            await _client.HashAddAsync("HashAsyncTest", testModel.Id.ToString(), testModel)
        );
        var result = await _client.HashGetAsync<TestModel>(
            "HashAsyncTest",
            testModel.Id.ToString()
        );
        Assert.Equal(testModel, result);
        Assert.True(await _client.HashDeleteAsync("HashAsyncTest", result.Id.ToString()));
        await _client.DeleteAsync("HashAsyncTest");
    }

    [Fact]
    public void HashBatchSync()
    {
        _client.Delete("HashBatchTest");
        var testModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        _client.HashAddRange(
            "HashBatchTest",
            testModels.ToDictionary(k => k.Id.ToString(), v => v)
        );
        var results = _client.HashGetRange<TestModel>(
            "HashBatchTest",
            testModels.Select(model => model.Id.ToString()).ToList()
        );
        Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
        Assert.Equal(
            results.Count,
            _client.HashDeleteRange(
                "HashBatchTest",
                results.Select(testModel => testModel.Id.ToString()).ToList()
            )
        );
        _client.Delete("HashBatchTest");
    }

    [Fact]
    public async void HashBatchAsync()
    {
        await _client.DeleteAsync("HashBatchTest");
        var testModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        await _client.HashAddRangeAsync(
            "HashBatchAsyncTest",
            testModels.ToDictionary(k => k.Id.ToString(), v => v)
        );
        var results = await _client.HashGetRangeAsync<TestModel>(
            "HashBatchAsyncTest",
            testModels.Select(model => model.Id.ToString()).ToList()
        );
        Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
        Assert.Equal(
            results.Count,
            await _client.HashDeleteRangeAsync(
                "HashBatchAsyncTest",
                results.Select(testModel => testModel.Id.ToString()).ToList()
            )
        );
        await _client.DeleteAsync("HashBatchTest");
    }

    [Fact]
    public void HashAllSync()
    {
        _client.Delete("HashAllTest");
        var testModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        _client.HashAddRange("HashAllTest", testModels.ToDictionary(k => k.Id.ToString(), v => v));
        var results = _client.HashGet<TestModel>("HashAllTest");
        Assert.True(results.All(kv => testModels.Any(model => model.Equals(kv.Value))));
        var keys = _client.HashGetAllEntityKeys("HashAllTest");
        Assert.True(keys.All(key => testModels.Any(model => model.Id.ToString() == key)));
        Assert.Equal(results.Count, _client.HashCount("HashAllTest"));
        Assert.True(_client.Delete("HashAllTest"));
        _client.Delete("HashAllTest");
    }

    [Fact]
    public async void HashAllAsync()
    {
        await _client.DeleteAsync("HashAllAsyncTest");
        var testModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        await _client.HashAddRangeAsync(
            "HashAllAsyncTest",
            testModels.ToDictionary(k => k.Id.ToString(), v => v)
        );
        var results = await _client.HashGetAsync<TestModel>("HashAllAsyncTest");
        Assert.True(results.All(kv => testModels.Any(model => model.Equals(kv.Value))));
        var keys = await _client.HashGetAllEntityKeysAsync("HashAllAsyncTest");
        Assert.True(keys.All(key => testModels.Any(model => model.Id.ToString() == key)));
        Assert.Equal(results.Count, await _client.HashCountAsync("HashAllAsyncTest"));
        Assert.True(await _client.DeleteAsync("HashAllAsyncTest"));
        await _client.DeleteAsync("HashAllAsyncTest");
    }
}
