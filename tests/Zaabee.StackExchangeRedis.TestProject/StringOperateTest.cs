namespace Zaabee.StackExchangeRedis.TestProject;

public class StringOperateTest
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
    public async void StringAsync()
    {
        var testModel = TestModelFactory.CreateTestModel();
        Assert.True(await _client.AddAsync(testModel.Id.ToString(), testModel));
        var result = await _client.GetAsync<TestModel>(testModel.Id.ToString());
        Assert.Equal(testModel, result);
        Assert.True(await _client.DeleteAsync(result.Id.ToString()));
    }
}
