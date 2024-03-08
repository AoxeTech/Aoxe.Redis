namespace Zaabee.StackExchangeRedis.TestProject;

public partial class SetTest
{
    private readonly IZaabeeRedisClient _client = ZaabeeRedisClientFactory.GetClient();

    [Fact]
    public void SetAddTest()
    {
        var key = "SetAddTest";
        _client.Delete(key);
        var testModel = TestModelFactory.CreateTestModel();
        Assert.True(_client.SetAdd(key, testModel));
        Assert.True(_client.SetContains(key, testModel));
        Assert.True(_client.Delete(key));
    }

    [Fact]
    public void SetAddRangeTest()
    {
        var key = "SetAddRangeTest";
        _client.Delete(key);
        var testModels = new List<TestModel>
        {
            TestModelFactory.CreateTestModel(),
            TestModelFactory.CreateTestModel(),
            TestModelFactory.CreateTestModel()
        };
        Assert.Equal(testModels.Count, _client.SetAddRange(key, testModels));
        foreach (var model in testModels)
        {
            Assert.True(_client.SetContains(key, model));
        }
        Assert.True(_client.Delete(key));
    }

    [Fact]
    public void SetCombineUnionTest()
    {
        var firstKey = "{SetCombineUnionTest}1";
        var secondKey = "{SetCombineUnionTest}2";
        _client.Delete(firstKey);
        _client.Delete(secondKey);
        var testModel1 = TestModelFactory.CreateTestModel();
        var testModel2 = TestModelFactory.CreateTestModel();
        _client.SetAdd(firstKey, testModel1);
        _client.SetAdd(secondKey, testModel2);
        var result = _client.SetCombineUnion<TestModel>(firstKey, secondKey);
        Assert.Contains(result, r => r.Equals(testModel1));
        Assert.Contains(result, r => r.Equals(testModel2));
        Assert.True(_client.Delete(firstKey));
        Assert.True(_client.Delete(secondKey));
    }
}
