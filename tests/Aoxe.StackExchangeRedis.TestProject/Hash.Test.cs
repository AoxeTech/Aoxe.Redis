namespace Aoxe.StackExchangeRedis.TestProject;

public partial class HashTest
{
    private readonly IAoxeRedisClient _client = AoxeRedisClientFactory.GetClient();

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
}
