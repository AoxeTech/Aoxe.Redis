namespace Zaabee.StackExchangeRedis.TestProject;

public partial class StringTest
{
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