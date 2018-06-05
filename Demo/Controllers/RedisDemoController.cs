using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Zaabee.Redis.Abstractions;

namespace Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RedisDemoController : ControllerBase
    {
        private readonly IZaabeeRedisClient _redisHandler;

        public RedisDemoController(IZaabeeRedisClient handler)
        {
            _redisHandler = handler;
        }

        [HttpGet]
        [HttpPost]
        public Guid Add()
        {
            var testModel = new TestModel
            {
                Id = Guid.NewGuid(),
                Name = "apple",
                Age = 18,
                CreateTime = DateTimeOffset.Now
            };
            _redisHandler.Add(testModel.Id.ToString(), testModel);
            return testModel.Id;
        }

        [HttpGet]
        [HttpPost]
        public List<string> AddRange(int quantity)
        {
            var testModles = new List<Tuple<string, TestModel>>();
            for (var i = 0; i < quantity; i++)
            {
                var id = Guid.NewGuid();
                testModles.Add(new Tuple<string, TestModel>(id.ToString(),
                    new TestModel
                    {
                        Id = id,
                        Name = "apple",
                        Age = 18,
                        CreateTime = DateTimeOffset.Now
                    }));
            }

            _redisHandler.AddRange(testModles);

            return testModles.Select(p => p.Item1).ToList();
        }

        [HttpGet]
        [HttpPost]
        public void Delete(string key)
        {
            _redisHandler.Delete(key);
        }

        [HttpGet]
        [HttpPost]
        public void DeleteAll([FromBody]IList<string> keys)
        {
            _redisHandler.DeleteAll(keys);
        }

        [HttpGet]
        [HttpPost]
        public TestModel Get(string key)
        {
            return _redisHandler.Get<TestModel>(key);
        }

        [HttpGet]
        [HttpPost]
        public Dictionary<string, TestModel> GetAll([FromBody]IList<string> keys)
        {
            return _redisHandler.Get<TestModel>(keys);
        }
    }

    [ProtoContract]
    public class TestModel
    {
        [ProtoMember(1)]
        public Guid Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public int Age { get; set; }

        [ProtoMember(4)]
        public DateTimeOffset CreateTime { get; set; }
    }
}