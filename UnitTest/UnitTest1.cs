using System;
using System.Linq;
using Xunit;
using Zaabee.Redis;
using Zaabee.Redis.Abstractions;
using Zaabee.Redis.Protobuf;

namespace UnitTest
{
    public class UnitTest1
    {
        private readonly IZaabeeRedisClient _client =
            new ZaabeeRedisClient(new RedisConfig("192.168.78.152:6379,abortConnect=false,syncTimeout=3000"),
                new Serializer());

        #region string

        [Fact]
        public void StringSync()
        {
            var testModel = CreateTestModel();
            Assert.True(_client.Add(testModel.Id.ToString(), testModel));
            var result = _client.Get<TestModel>(testModel.Id.ToString());
            Assert.Equal(testModel, result);
            Assert.True(_client.Delete(result.Id.ToString()));
        }

        [Fact]
        public void StringBatchSync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => CreateTestModel()).ToList();
            _client.AddRange(testModels.Select(model => new Tuple<string, TestModel>(model.Id.ToString(), model))
                .ToList());
            var results = _client.Get<TestModel>(testModels.Select(model => model.Id.ToString()).ToList());
            Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
            Assert.Equal(results.Count, _client.DeleteAll(results.Select(result => result.Id.ToString()).ToList()));
        }

        [Fact]
        public void StringAsync()
        {
            var testModel = CreateTestModel();
            Assert.True(_client.AddAsync(testModel.Id.ToString(), testModel).Result);
            var result = _client.GetAsync<TestModel>(testModel.Id.ToString()).Result;
            Assert.Equal(testModel, result);
            Assert.True(_client.DeleteAsync(result.Id.ToString()).Result);
        }

        [Fact]
        public void StringBatchAsync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => CreateTestModel()).ToList();
            _client.AddRangeAsync(testModels.Select(model => new Tuple<string, TestModel>(model.Id.ToString(), model))
                .ToList()).Wait();
            var results = _client.GetAsync<TestModel>(testModels.Select(model => model.Id.ToString()).ToList()).Result;
            Assert.True(results.All(result => testModels.Any(model => model.Equals(result))));
            Assert.Equal(results.Count,
                _client.DeleteAllAsync(results.Select(result => result.Id.ToString()).ToList()).Result);
        }

        #endregion

        #region set



        #endregion

        #region list



        #endregion

        #region hash

        [Fact]
        public void HashSync()
        {
            var testModel = CreateTestModel();
            Assert.True(_client.HashAdd("HashTest", testModel.Id.ToString(), testModel));
            var result = _client.HashGet<TestModel>("HashTest", testModel.Id.ToString());
            Assert.Equal(testModel, result);
            Assert.True(_client.HashDelete("HashTest", result.Id.ToString()));
        }

        [Fact]
        public void HashAsync()
        {
            var testModel = CreateTestModel();
            Assert.True(_client.HashAddAsync("HashAsyncTest", testModel.Id.ToString(), testModel).Result);
            var result = _client.HashGetAsync<TestModel>("HashAsyncTest", testModel.Id.ToString()).Result;
            Assert.Equal(testModel, result);
            Assert.True(_client.HashDelete("HashAsyncTest", result.Id.ToString()));
        }

        [Fact]
        public void HashBatchSync()
        {
            var testModels = Enumerable.Range(0, 10).Select(p => CreateTestModel()).ToList();
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
            var testModels = Enumerable.Range(0, 10).Select(p => CreateTestModel()).ToList();
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
            var testModels = Enumerable.Range(0, 10).Select(p => CreateTestModel()).ToList();
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
            var testModels = Enumerable.Range(0, 10).Select(p => CreateTestModel()).ToList();
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

        #endregion

        #region zset



        #endregion

        private static TestModel CreateTestModel()
        {
            return new TestModel
            {
                Id = Guid.NewGuid(),
                Name = "Apple",
                Age = new Random().Next(),
                CreateTime = DateTime.Now
            };
        }
    }
}