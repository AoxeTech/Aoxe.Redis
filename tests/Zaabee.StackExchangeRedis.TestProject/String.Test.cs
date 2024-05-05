namespace Zaabee.StackExchangeRedis.TestProject;

public partial class StringTest
{
    private readonly IZaabeeRedisClient _client = ZaabeeRedisClientFactory.GetClient();

    [Fact]
    public void Add_Get_StringKey_Entity()
    {
        var testModel = TestModelFactory.CreateTestModel();
        Assert.True(_client.Add(testModel.Id.ToString(), testModel));
        var result = _client.Get<TestModel>(testModel.Id.ToString());
        Assert.Equal(testModel, result);
        Assert.True(_client.Delete(result.Id.ToString()));
    }

    [Fact]
    public void Add_Get_StringKey_Int()
    {
        var id = Guid.NewGuid().ToString();
        Assert.True(_client.Add(id, 1));
        var result = _client.Get<int>(id);
        Assert.Equal(1, result);
        Assert.True(_client.Delete(id));
    }

    [Fact]
    public void Add_Get_MultipleKeys()
    {
        var testModels = new List<TestModel>
        {
            TestModelFactory.CreateTestModel(),
            TestModelFactory.CreateTestModel(),
            TestModelFactory.CreateTestModel()
        };

        var keys = testModels.Select(m => $"{{Add_Get_MultipleKeys}}{m.Id.ToString()}");

        foreach (var model in testModels)
        {
            Assert.True(_client.Add($"{{Add_Get_MultipleKeys}}{model.Id.ToString()}", model));
        }

        var results = _client.Get<TestModel>(keys);

        foreach (var model in testModels)
        {
            Assert.Contains(
                results,
                r =>
                    $"{{Add_Get_MultipleKeys}}{r.Id.ToString()}"
                    == $"{{Add_Get_MultipleKeys}}{model.Id.ToString()}"
            );
            Assert.True(_client.Delete($"{{Add_Get_MultipleKeys}}{model.Id.ToString()}"));
        }
    }

    [Fact]
    public void Add_Get_StringKey_LongValue()
    {
        string key = "Add_Get_StringKey_LongValue";
        long value = 12345;
        Assert.True(_client.Add(key, value));
        var result = _client.Get<long>(key);
        Assert.Equal(value, result);
        Assert.True(_client.Delete(key));
    }

    [Fact]
    public void Add_Get_StringKey_DoubleValue()
    {
        string key = "Add_Get_StringKey_DoubleValue";
        double value = 12345.67;
        Assert.True(_client.Add(key, value));
        var result = _client.Get<double>(key);
        Assert.Equal(value, result);
        Assert.True(_client.Delete(key));
    }

    [Fact]
    public void Increment_StringKey_DoubleValue()
    {
        string key = "Increment_StringKey_DoubleValue";
        double value = 12345.67;
        double incrementValue = 10.33;
        _client.Add(key, value);
        var result = _client.Increment(key, incrementValue);
        Assert.Equal(value + incrementValue, result);
        Assert.True(_client.Delete(key));
    }

    [Fact]
    public void Increment_StringKey_LongValue()
    {
        string key = "Increment_StringKey_LongValue";
        long value = 12345;
        long incrementValue = 10;
        _client.Add(key, value);
        var result = _client.Increment(key, incrementValue);
        Assert.Equal(value + incrementValue, result);
        Assert.True(_client.Delete(key));
    }
}
