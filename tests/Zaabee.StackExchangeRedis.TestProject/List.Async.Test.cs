namespace Zaabee.StackExchangeRedis.TestProject;

public partial class ListTest
{

    [Fact]
    public async Task ListAsync()
    {
        await _client.DeleteAsync("ListAsync");
        var testModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        Assert.Equal(testModels.Count, await _client.ListLeftPushRangeAsync("ListAsync", testModels));
        Assert.Equal(testModels.Count, await _client.ListLengthAsync("ListAsync"));
        for (var i = 0; i < testModels.Count; i++)
            Assert.Equal(
                testModels[i],
                await _client.ListGetByIndexAsync<TestModel>("ListAsync", testModels.Count - 1 - i)
            );

        var testLeftModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        Assert.Equal(
            testLeftModels.Count + testModels.Count,
            await _client.ListLeftPushRangeAsync("ListAsync", testLeftModels)
        );
        Assert.Equal(testLeftModels.Count + testModels.Count, _client.ListLength("ListAsync"));
        for (var i = 0; i < testLeftModels.Count; i++)
            Assert.Equal(
                testLeftModels[i],
                await _client.ListGetByIndexAsync<TestModel>("ListAsync", testLeftModels.Count - 1 - i)
            );

        var testRightModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        Assert.Equal(
            testLeftModels.Count + testModels.Count + testRightModels.Count,
            await _client.ListRightPushRangeAsync("ListAsync", testRightModels)
        );
        Assert.Equal(
            testLeftModels.Count + testModels.Count + testRightModels.Count,
            await _client.ListLengthAsync("ListAsync")
        );
        for (var i = 0; i < testRightModels.Count; i++)
            Assert.Equal(
                testRightModels[i],
                await _client.ListGetByIndexAsync<TestModel>(
                    "ListAsync",
                    testLeftModels.Count + testModels.Count + i
                )
            );

        await _client.DeleteAsync("ListAsync");
    }

    [Fact]
    public async Task ListPushPopAsync()
    {
        await _client.DeleteAsync("ListPushPopAsync");
        var testModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();

        await _client.ListLeftPushRangeAsync("ListPushPopAsync", testModels);
        foreach (var testModel in testModels)
            Assert.Equal(testModel, await _client.ListRightPopAsync<TestModel>("ListPushPopAsync"));

        await _client.ListRightPushRangeAsync("ListPushPopAsync", testModels);
        foreach (var testModel in testModels)
            Assert.Equal(testModel, await _client.ListLeftPopAsync<TestModel>("ListPushPopAsync"));

        await _client.DeleteAsync("ListPushPopAsync");
    }

    [Fact]
    public async Task ListOprByIndexAsync()
    {
        await _client.DeleteAsync("ListOprByIndexAsync");
        var testModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        for (var i = 0; i < 10; i++)
            await _client.ListLeftPushAsync("ListOprByIndexAsync", (TestModel)null);
        for (var i = 0; i < testModels.Count; i++)
            await _client.ListSetByIndexAsync("ListOprByIndexAsync", i, testModels[i]);
        for (var i = 0; i < testModels.Count; i++)
            Assert.Equal(testModels[i], await _client.ListGetByIndexAsync<TestModel>("ListOprByIndexAsync", i));
        await _client.DeleteAsync("ListOprByIndexAsync");
    }

    [Fact]
    public async Task ListInsertAsync()
    {
        await _client.DeleteAsync("ListInsertAsync");
        var testModel = TestModelFactory.CreateTestModel();
        var testBeforeModel = TestModelFactory.CreateTestModel();
        var testAfterModel = TestModelFactory.CreateTestModel();

        await _client.ListRightPushAsync("ListInsertAsync", testModel);
        await _client.ListInsertBeforeAsync("ListInsertAsync", testModel, testBeforeModel);
        await _client.ListInsertAfterAsync("ListInsertAsync", testModel, testAfterModel);

        Assert.Equal(testBeforeModel, await _client.ListLeftPopAsync<TestModel>("ListInsertAsync"));
        Assert.Equal(testAfterModel, await _client.ListRightPopAsync<TestModel>("ListInsertAsync"));
        Assert.Equal(1, await _client.ListRemoveAsync("ListInsertAsync", testModel));

        await _client.DeleteAsync("ListInsertAsync");
    }

    [Fact]
    public async Task ListRangeTrimAsync()
    {
        await _client.DeleteAsync("ListRangeTrimAsyncA");
        await _client.DeleteAsync("ListRangeTrimAsyncB");

        var testModelsA = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        var testModelsB = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();

        await _client.ListRightPushRangeAsync("ListRangeTrimAsyncA", testModelsA);
        await _client.ListLeftPushRangeAsync("ListRangeTrimAsyncB", testModelsB);

        Assert.Equal(testModelsA.Count, await _client.ListLengthAsync("ListRangeTrimAsyncA"));
        Assert.Equal(testModelsB.Count, await _client.ListLengthAsync("ListRangeTrimAsyncB"));

        var testModelsResultA = await _client.ListRangeAsync<TestModel>("ListRangeTrimAsyncA", 0, 9);
        for (var i = 0; i < testModelsA.Count; i++)
            Assert.Equal(testModelsA[i], testModelsResultA[i]);

        await _client.ListTrimAsync("ListRangeTrimAsyncA", 0, 9);
        foreach (var testModel in testModelsA)
            Assert.Equal(testModel, await _client.ListLeftPopAsync<TestModel>("ListRangeTrimAsyncA"));

        Assert.Equal(0, await _client.ListLengthAsync("ListRangeTrimAsyncA"));

        await _client.DeleteAsync("ListRangeTrimAsyncA");
        await _client.DeleteAsync("ListRangeTrimAsyncB");
    }

    [Fact]
    public async Task ListRightPopLeftPushAsync()
    {
        await _client.DeleteAsync("{ListRightPopLeftPushAsync}A");
        await _client.DeleteAsync("{ListRightPopLeftPushAsync}B");

        var testModelsA = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        var testModelsB = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();

        await _client.ListRightPushRangeAsync("{ListRightPopLeftPushAsync}A", testModelsA);
        await _client.ListRightPushRangeAsync("{ListRightPopLeftPushAsync}B", testModelsB);

        var testModel = _client.ListRightPopLeftPush<TestModel>("{ListRightPopLeftPushAsync}A", "{ListRightPopLeftPushAsync}B");
        Assert.Equal(testModelsA.Last(), testModel);
        Assert.Equal(testModelsA.Count - 1, await _client.ListLengthAsync("{ListRightPopLeftPushAsync}A"));
        Assert.Equal(testModelsB.Count + 1, await _client.ListLengthAsync("{ListRightPopLeftPushAsync}B"));

        var result = await _client.ListLeftPopAsync<TestModel>("{ListRightPopLeftPushAsync}B");
        Assert.Equal(testModel, result);
    }
}
