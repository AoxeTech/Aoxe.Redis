namespace Aoxe.StackExchangeRedis.TestProject;

public partial class StringTest
{
    [Fact]
    public async Task Add_Get_Async_StringKey_Entity()
    {
        var testModel = TestModelFactory.CreateTestModel();
        Assert.True(await _client.AddAsync(testModel.Id.ToString(), testModel));
        var result = await _client.GetAsync<TestModel>(testModel.Id.ToString());
        Assert.Equal(testModel, result);
        Assert.True(await _client.DeleteAsync(result.Id.ToString()));
    }

    [Fact]
    public async Task Add_Get_Async_MultipleKeys()
    {
        var testModels = new List<TestModel>
        {
            TestModelFactory.CreateTestModel(),
            TestModelFactory.CreateTestModel(),
            TestModelFactory.CreateTestModel()
        };

        var keys = testModels.Select(m => $"{{Add_Get_Async_MultipleKeys}}{m.Id.ToString()}");

        foreach (var model in testModels)
        {
            Assert.True(
                await _client.AddAsync(
                    $"{{Add_Get_Async_MultipleKeys}}{model.Id.ToString()}",
                    model
                )
            );
        }

        var results = await _client.GetAsync<TestModel>(keys);

        foreach (var model in testModels)
        {
            Assert.Contains(
                results,
                r =>
                    $"{{Add_Get_Async_MultipleKeys}}{r.Id.ToString()}"
                    == $"{{Add_Get_Async_MultipleKeys}}{model.Id.ToString()}"
            );
            Assert.True(
                await _client.DeleteAsync($"{{Add_Get_Async_MultipleKeys}}{model.Id.ToString()}")
            );
        }
    }

    [Fact]
    public async Task Add_Get_Async_StringKey_LongValue()
    {
        string key = "Add_Get_Async_StringKey_LongValue";
        long value = 12345;
        Assert.True(await _client.AddAsync(key, value));
        var result = await _client.GetAsync<long>(key);
        Assert.Equal(value, result);
        Assert.True(await _client.DeleteAsync(key));
    }

    [Fact]
    public async Task Add_Get_Async_StringKey_DoubleValue()
    {
        string key = "Add_Get_Async_StringKey_DoubleValue";
        double value = 12345.67;
        Assert.True(await _client.AddAsync(key, value));
        var result = await _client.GetAsync<double>(key);
        Assert.Equal(value, result);
        Assert.True(await _client.DeleteAsync(key));
    }

    [Fact]
    public async Task Increment_Async_StringKey_DoubleValue()
    {
        string key = "Increment_Async_StringKey_DoubleValue";
        double value = 12345.67;
        double incrementValue = 10.33;
        await _client.AddAsync(key, value);
        var result = await _client.IncrementAsync(key, incrementValue);
        Assert.Equal(value + incrementValue, result);
        Assert.True(await _client.DeleteAsync(key));
    }

    [Fact]
    public async Task Increment_Async_StringKey_LongValue()
    {
        string key = "Increment_Async_StringKey_LongValue";
        long value = 12345;
        long incrementValue = 10;
        await _client.AddAsync(key, value);
        var result = await _client.IncrementAsync(key, incrementValue);
        Assert.Equal(value + incrementValue, result);
        Assert.True(await _client.DeleteAsync(key));
    }
}
