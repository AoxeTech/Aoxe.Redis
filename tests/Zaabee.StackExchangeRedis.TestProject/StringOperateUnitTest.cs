namespace Zaabee.StackExchangeRedis.TestProject;

public class StringOperateUnitTest
{
    private readonly IZaabeeRedisClient _client = ZaabeeRedisClientFactory.GetClient();

    [Fact]
    public void StringSync()
    {
        var testModel = TestModelFactory.CreateTestModel();
        Assert.True(_client.Add(testModel.Id.ToString(), testModel));
        var result = _client.Get<TestModel>(testModel.Id.ToString());
        Assert.Equal(testModel, result);
        Assert.True(_client.Delete(result.Id.ToString()));
    }

    [Fact]
    public void StringBatchSync()
    {
        var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
        _client.AddRange(testModels.ToDictionary(k => k.Id.ToString(), v => v));
        var results = _client.Get<TestModel>(testModels.Select(model => model.Id.ToString()).ToList());
        Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
        Assert.Equal(results.Count, _client.DeleteAll(results.Select(result => result.Id.ToString()).ToList()));
    }

    [Fact]
    public async void StringAsync()
    {
        var testModel = TestModelFactory.CreateTestModel();
        Assert.True(await _client.AddAsync(testModel.Id.ToString(), testModel));
        var result = await _client.GetAsync<TestModel>(testModel.Id.ToString());
        Assert.Equal(testModel, result);
        Assert.True(await _client.DeleteAsync(result.Id.ToString()));
    }

    [Fact]
    public async void StringBatchAsync()
    {
        var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
        await _client.AddRangeAsync(testModels.ToDictionary(k => k.Id.ToString(), v => v));
        var results = await _client.GetAsync<TestModel>(testModels.Select(model => model.Id.ToString()).ToList());
        Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
        Assert.Equal(results.Count,
            await _client.DeleteAllAsync(results.Select(result => result.Id.ToString()).ToList()));
    }
}