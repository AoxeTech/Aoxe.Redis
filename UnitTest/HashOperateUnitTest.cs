using System;
using System.Linq;
using Xunit;
using Zaabee.Redis;
using Zaabee.Redis.Abstractions;
using Zaabee.Redis.Protobuf;

namespace UnitTest
{
    public class HashOperateUnitTest
    {
        private readonly IZaabeeRedisClient _client =
            new ZaabeeRedisClient(new RedisConfig("192.168.78.152:6379,abortConnect=false,syncTimeout=3000"),
                new Serializer());

        [Fact]
        public void HashSync()
        {
            var testModel = TestModelFactory.CreateTestModel();
            Assert.True(_client.HashAdd("HashTest", testModel.Id.ToString(), testModel));
            var result = _client.HashGet<TestModel>("HashTest", testModel.Id.ToString());
            Assert.Equal(testModel, result);
            Assert.True(_client.HashDelete("HashTest", result.Id.ToString()));
        }

        [Fact]
        public void HashAsync()
        {
            var testModel = TestModelFactory.CreateTestModel();
            Assert.True(_client.HashAddAsync("HashAsyncTest", testModel.Id.ToString(), testModel).Result);
            var result = _client.HashGetAsync<TestModel>("HashAsyncTest", testModel.Id.ToString()).Result;
            Assert.Equal(testModel, result);
            Assert.True(_client.HashDelete("HashAsyncTest", result.Id.ToString()));
        }

        [Fact]
        public void HashBatchSync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            _client.HashAddRange("HashBatchTest",
                testModels.Select(testModel => new Tuple<string, TestModel>(testModel.Id.ToString(), testModel))
                    .ToList());
            var results =
                _client.HashGet<TestModel>("HashBatchTest", testModels.Select(model => model.Id.ToString()).ToList());
            Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
            Assert.Equal(results.Count,
                _client.HashDelete("HashBatchTest", results.Select(testModel => testModel.Id.ToString()).ToList()));
        }

        [Fact]
        public void HashBatchAsync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            _client.HashAddRangeAsync("HashBatchAsyncTest",
                testModels.Select(testModel => new Tuple<string, TestModel>(testModel.Id.ToString(), testModel))
                    .ToList()).Wait();
            var results =
                _client.HashGetAsync<TestModel>("HashBatchAsyncTest", testModels.Select(model => model.Id.ToString()).ToList()).Result;
            Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
            Assert.Equal(results.Count,
                _client.HashDeleteAsync("HashBatchAsyncTest", results.Select(testModel => testModel.Id.ToString()).ToList()).Result);
        }

        [Fact]
        public void HashAllSync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            _client.HashAddRange("HashAllTest",
                testModels.Select(testModel => new Tuple<string, TestModel>(testModel.Id.ToString(), testModel))
                    .ToList());
            var results = _client.HashGet<TestModel>("HashAllTest");
            Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
            var keys = _client.HashGetAllEntityKeys("HashAllTest");
            Assert.True(keys.All(key => testModels.Any(model => model.Id.ToString() == key)));
            Assert.Equal(results.Count,_client.HashCount("HashAllTest"));
            Assert.True(_client.Delete("HashAllTest"));
        }

        [Fact]
        public void HashAllAsync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            _client.HashAddRangeAsync("HashAllAsyncTest",
                testModels.Select(testModel => new Tuple<string, TestModel>(testModel.Id.ToString(), testModel))
                    .ToList()).Wait();
            var results = _client.HashGetAsync<TestModel>("HashAllAsyncTest").Result;
            Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
            var keys = _client.HashGetAllEntityKeysAsync("HashAllAsyncTest").Result;
            Assert.True(keys.All(key => testModels.Any(model => model.Id.ToString() == key)));
            Assert.Equal(results.Count,_client.HashCountAsync("HashAllAsyncTest").Result);
            Assert.True(_client.DeleteAsync("HashAllAsyncTest").Result);
        }
    }
}