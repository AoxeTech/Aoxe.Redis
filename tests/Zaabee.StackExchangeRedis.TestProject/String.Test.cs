namespace Zaabee.StackExchangeRedis.TestProject;

public partial class StringTest
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
}
