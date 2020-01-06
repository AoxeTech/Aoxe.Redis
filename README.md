# Zaabee.Redis

[Redis](https://github.com/antirez/redis) is an in-memory database that persists on disk. The data model is key-value, but many different kind of values are supported: Strings, Lists, Sets, Sorted Sets, Hashes, HyperLogLogs, Bitmaps. <http://redis.io>

## Introduction

This redis client wrappers and serializers.

## QuickStart

### NuGet

```CLI
Install-Package Zaabee.StackExchangeRedis
Install-Package Zaabee.StackExchangeRedis.NewtonsoftJson
```

### Build Project

Create an asp.net core project and import references in startup.cs. Get [Zaabee.StackExchangeRedis](https://github.com/Mutuduxf/Zaabee.Redis/tree/master/src/Zaabee.StackExchangeRedis/Zaabee.StackExchangeRedis) and [Zaabee.StackExchangeRedis.NewtonsoftJson](https://github.com/Mutuduxf/Zaabee.Redis/tree/master/src/Zaabee.StackExchangeRedis/Zaabee.StackExchangeRedis.NewtonsoftJson) from Nuget,otherwise we have other serializers:

[Zaabee.StackExchangeRedis.Binary](https://github.com/Mutuduxf/Zaabee.Redis/tree/master/src/Zaabee.StackExchangeRedis/Zaabee.StackExchangeRedis.Binary)

[Zaabee.StackExchangeRedis.Jil](https://github.com/Mutuduxf/Zaabee.Redis/tree/master/src/Zaabee.StackExchangeRedis/Zaabee.StackExchangeRedis.Jil)

[Zaabee.StackExchangeRedis.MsgPack](https://github.com/Mutuduxf/Zaabee.Redis/tree/master/src/Zaabee.StackExchangeRedis/Zaabee.StackExchangeRedis.MsgPack)

[Zaabee.StackExchangeRedis.Protobuf](https://github.com/Mutuduxf/Zaabee.Redis/tree/master/src/Zaabee.StackExchangeRedis/Zaabee.StackExchangeRedis.Protobuf)

[Zaabee.StackExchangeRedis.Utf8Json](https://github.com/Mutuduxf/Zaabee.Redis/tree/master/src/Zaabee.StackExchangeRedis/Zaabee.StackExchangeRedis.Utf8Json)

[Zaabee.StackExchangeRedis.ZeroFormatter](https://github.com/Mutuduxf/Zaabee.Redis/tree/master/src/Zaabee.StackExchangeRedis/Zaabee.StackExchangeRedis.ZeroFormatter)

```CSharp
using Zaabee.StackExchangeRedis;
using Zaabee.StackExchangeRedis.Abstractions;
using Zaabee.StackExchangeRedis.ISerialize;
using Zaabee.StackExchangeRedis.NewtonsoftJson;
```

Register ZaabyRedisClient in Configuration like

```CSharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    services.AddSingleton<IZaabeeRedisClient>(p =>
        new ZaabeeRedisClient(new RedisConfig("192.168.78.152:6379,abortConnect=false,syncTimeout=3000"),
            new Serializer()));
}
```

Add a TestClass for the demo

```CSharp
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
    public DateTime CreateTime { get; set; }
}
```

Create a controller like this

```CSharp
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
            CreateTime = DateTime.Now
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
                    CreateTime = DateTime.Now
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
```

Now you can run a [Postman](https://www.getpostman.com/) and send some requests to try it.And the ZaabyRedisClient has async methods like AddAsync/AddRangeAsync/DeleteAsync/DeleteAllAsync,you can try it yourself.
