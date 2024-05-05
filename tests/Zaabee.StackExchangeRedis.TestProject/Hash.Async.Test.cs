namespace Zaabee.StackExchangeRedis.TestProject;

public partial class HashTest
{
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
