using System;
using System.Linq;
using Xunit;
using Zaabee.Redis;
using Zaabee.Redis.Abstractions;
using Zaabee.Redis.Protobuf;

namespace UnitTest
{
    public class StringOperateUnitTest
    {
        private readonly IZaabeeRedisClient _client =
            new ZaabeeRedisClient(new RedisConfig("192.168.78.152:6379,abortConnect=false,syncTimeout=3000"),
                new Serializer());

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
        public void StringBatchSync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            _client.AddRange(testModels.Select(model => new Tuple<string, TestModel>(model.Id.ToString(), model))
                .ToList());
            var results = _client.Get<TestModel>(testModels.Select(model => model.Id.ToString()).ToList());
            Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
            Assert.Equal(results.Count, _client.DeleteAll(results.Select(result => result.Id.ToString()).ToList()));
        }

        [Fact]
        public void StringAsync()
        {
            var testModel = TestModelFactory.CreateTestModel();
            Assert.True(_client.AddAsync(testModel.Id.ToString(), testModel).Result);
            var result = _client.GetAsync<TestModel>(testModel.Id.ToString()).Result;
            Assert.Equal(testModel, result);
            Assert.True(_client.DeleteAsync(result.Id.ToString()).Result);
        }

        [Fact]
        public void StringBatchAsync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => TestModelFactory.CreateTestModel()).ToList();
            _client.AddRangeAsync(testModels.Select(model => new Tuple<string, TestModel>(model.Id.ToString(), model))
                .ToList()).Wait();
            var results = _client.GetAsync<TestModel>(testModels.Select(model => model.Id.ToString()).ToList()).Result;
            Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
            Assert.Equal(results.Count,
                _client.DeleteAllAsync(results.Select(result => result.Id.ToString()).ToList()).Result);
        }
    }
}