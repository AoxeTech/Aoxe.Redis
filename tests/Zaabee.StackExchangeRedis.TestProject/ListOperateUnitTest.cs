using System;
using System.Linq;
using Xunit;
using Zaabee.StackExchangeRedis.Abstractions;

namespace Zaabee.StackExchangeRedis.TestProject
{
    public class ListOperateUnitTest
    {
        private readonly IZaabeeRedisClient _client =
            new ZaabeeRedisClient("192.168.78.140:6379,abortConnect=false,syncTimeout=3000", TimeSpan.FromMinutes(10),
                new Protobuf.Serializer());

        [Fact]
        public void ListSync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            Assert.Equal(testModels.Count, _client.ListLeftPushRange("ListSync", testModels));
            Assert.Equal(testModels.Count, _client.ListLength("ListSync"));
            for (var i = 0; i < testModels.Count; i++)
                Assert.Equal(testModels[i],
                    _client.ListGetByIndex<TestModel>("ListSync", testModels.Count - 1 - i));

            var testLeftModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            Assert.Equal(testLeftModels.Count + testModels.Count, _client.ListLeftPushRange("ListSync", testLeftModels));
            Assert.Equal(testLeftModels.Count + testModels.Count, _client.ListLength("ListSync"));
            for (var i = 0; i < testLeftModels.Count; i++)
                Assert.Equal(testLeftModels[i],
                    _client.ListGetByIndex<TestModel>("ListSync", testLeftModels.Count - 1 - i));

            var testRightModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            Assert.Equal(testLeftModels.Count + testModels.Count + testRightModels.Count,
                _client.ListRightPushRange("ListSync", testRightModels));
            Assert.Equal(testLeftModels.Count + testModels.Count + testRightModels.Count,
                _client.ListLength("ListSync"));
            for (var i = 0; i < testRightModels.Count; i++)
                Assert.Equal(testRightModels[i],
                    _client.ListGetByIndex<TestModel>("ListSync",
                        testLeftModels.Count + testModels.Count + i));

            _client.Delete("ListSync");
        }

        [Fact]
        public void ListPushPopSync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();

            _client.ListLeftPushRange("ListPushPopSync", testModels);
            testModels.ForEach(testModel =>
                Assert.Equal(testModel, _client.ListRightPop<TestModel>("ListPushPopSync")));

            _client.ListRightPushRange("ListPushPopSync", testModels);
            testModels.ForEach(testModel =>
                Assert.Equal(testModel, _client.ListLeftPop<TestModel>("ListPushPopSync")));

            _client.Delete("ListPushPopSync");
        }

        [Fact]
        public void ListOprByIndexSync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            for (var i = 0; i < 10; i++)
                _client.ListLeftPush("ListOprByIndexSync", (TestModel) null);
            for (var i = 0; i < testModels.Count; i++)
                _client.ListSetByIndex("ListOprByIndexSync", i, testModels[i]);
            for (var i = 0; i < testModels.Count; i++)
                Assert.Equal(testModels[i], _client.ListGetByIndex<TestModel>("ListOprByIndexSync", i));
            _client.Delete("ListOprByIndexSync");
        }

        [Fact]
        public void ListInsertSync()
        {
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
            var testModelsA = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            var testModelsB = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();

            _client.ListRightPushRange("ListRangeTrimSyncA", testModelsA);
            _client.ListRightPushRange("ListRangeTrimSyncB", testModelsB);
            _client.ListRightPopLeftPush<TestModel>("ListRangeTrimSyncA", "ListRangeTrimSyncB");

            Assert.Equal(testModelsA.Count - 1, _client.ListLength("ListRangeTrimSyncA"));
            Assert.Equal(testModelsB.Count + 1, _client.ListLength("ListRangeTrimSyncB"));

            var testModelsResultB = _client.ListRange<TestModel>("ListRangeTrimSyncB", 1, 10);
            for (var i = 0; i < testModelsA.Count; i++)
                Assert.Equal(testModelsB[i], testModelsResultB[i]);

            _client.ListTrim("ListRangeTrimSyncA", 0, 9);
            for (var i = 0; i < testModelsA.Count - 1; i++)
                Assert.Equal(testModelsA[i], _client.ListLeftPop<TestModel>("ListRangeTrimSyncA"));

            Assert.Equal(0, _client.ListLength("ListRangeTrimSyncA"));

            _client.Delete("ListRangeTrimSyncA");
            _client.Delete("ListRangeTrimSyncB");
        }
    }
}