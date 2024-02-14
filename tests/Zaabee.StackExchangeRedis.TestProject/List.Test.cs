namespace Zaabee.StackExchangeRedis.TestProject;

public partial class ListTest
{
    private readonly IZaabeeRedisClient _client = ZaabeeRedisClientFactory.GetClient();

    [Fact]
    public void ListSync()
    {
        _client.Delete("ListSync");
        var testModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        Assert.Equal(testModels.Count, _client.ListLeftPushRange("ListSync", testModels));
        Assert.Equal(testModels.Count, _client.ListLength("ListSync"));
        for (var i = 0; i < testModels.Count; i++)
            Assert.Equal(
                testModels[i],
                _client.ListGetByIndex<TestModel>("ListSync", testModels.Count - 1 - i)
            );

        var testLeftModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        Assert.Equal(
            testLeftModels.Count + testModels.Count,
            _client.ListLeftPushRange("ListSync", testLeftModels)
        );
        Assert.Equal(testLeftModels.Count + testModels.Count, _client.ListLength("ListSync"));
        for (var i = 0; i < testLeftModels.Count; i++)
            Assert.Equal(
                testLeftModels[i],
                _client.ListGetByIndex<TestModel>("ListSync", testLeftModels.Count - 1 - i)
            );

        var testRightModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        Assert.Equal(
            testLeftModels.Count + testModels.Count + testRightModels.Count,
            _client.ListRightPushRange("ListSync", testRightModels)
        );
        Assert.Equal(
            testLeftModels.Count + testModels.Count + testRightModels.Count,
            _client.ListLength("ListSync")
        );
        for (var i = 0; i < testRightModels.Count; i++)
            Assert.Equal(
                testRightModels[i],
                _client.ListGetByIndex<TestModel>(
                    "ListSync",
                    testLeftModels.Count + testModels.Count + i
                )
            );

        _client.Delete("ListSync");
    }

    [Fact]
    public void ListPushPopSync()
    {
        _client.Delete("ListPushPopSync");
        var testModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();

        _client.ListLeftPushRange("ListPushPopSync", testModels);
        testModels.ForEach(
            testModel => Assert.Equal(testModel, _client.ListRightPop<TestModel>("ListPushPopSync"))
        );

        _client.ListRightPushRange("ListPushPopSync", testModels);
        testModels.ForEach(
            testModel => Assert.Equal(testModel, _client.ListLeftPop<TestModel>("ListPushPopSync"))
        );

        _client.Delete("ListPushPopSync");
    }

    [Fact]
    public void ListOprByIndexSync()
    {
        _client.Delete("ListOprByIndexSync");
        var testModels = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        for (var i = 0; i < 10; i++)
            _client.ListLeftPush("ListOprByIndexSync", (TestModel)null);
        for (var i = 0; i < testModels.Count; i++)
            _client.ListSetByIndex("ListOprByIndexSync", i, testModels[i]);
        for (var i = 0; i < testModels.Count; i++)
            Assert.Equal(testModels[i], _client.ListGetByIndex<TestModel>("ListOprByIndexSync", i));
        _client.Delete("ListOprByIndexSync");
    }

    [Fact]
    public void ListInsertSync()
    {
        _client.Delete("ListInsertSync");
        var testModel = TestModelFactory.CreateTestModel();
        var testBeforeModel = TestModelFactory.CreateTestModel();
        var testAfterModel = TestModelFactory.CreateTestModel();

        _client.ListRightPush("ListInsertSync", testModel);
        _client.ListInsertBefore("ListInsertSync", testModel, testBeforeModel);
        _client.ListInsertAfter("ListInsertSync", testModel, testAfterModel);

        Assert.Equal(testBeforeModel, _client.ListLeftPop<TestModel>("ListInsertSync"));
        Assert.Equal(testAfterModel, _client.ListRightPop<TestModel>("ListInsertSync"));
        Assert.Equal(1, _client.ListRemove("ListInsertSync", testModel));

        _client.Delete("ListInsertSync");
    }

    [Fact]
    public void ListRangeTrimSync()
    {
        _client.Delete("ListRangeTrimSyncA");
        _client.Delete("ListRangeTrimSyncB");
        
        var testModelsA = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();
        var testModelsB = Enumerable
            .Range(0, 10)
            .Select(p => TestModelFactory.CreateTestModel())
            .ToList();

        _client.ListRightPushRange("ListRangeTrimSyncA", testModelsA);
        _client.ListLeftPushRange("ListRangeTrimSyncB", testModelsB);

        Assert.Equal(testModelsA.Count, _client.ListLength("ListRangeTrimSyncA"));
        Assert.Equal(testModelsB.Count, _client.ListLength("ListRangeTrimSyncB"));

        var testModelsResultA = _client.ListRange<TestModel>("ListRangeTrimSyncA", 0, 9);
        for (var i = 0; i < testModelsA.Count; i++)
            Assert.Equal(testModelsA[i], testModelsResultA[i]);

        _client.ListTrim("ListRangeTrimSyncA", 0, 9);
        foreach (var testModel in testModelsA)
            Assert.Equal(testModel, _client.ListLeftPop<TestModel>("ListRangeTrimSyncA"));

        Assert.Equal(0, _client.ListLength("ListRangeTrimSyncA"));

        _client.Delete("ListRangeTrimSyncA");
        _client.Delete("ListRangeTrimSyncB");
    }
}
