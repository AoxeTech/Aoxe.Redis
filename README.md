# Zaabee.Redis

[Redis](https://github.com/antirez/redis) is an in-memory database that persists on disk. The data model is key-value, but many different kind of values are supported: Strings, Lists, Sets, Sorted Sets, Hashes, HyperLogLogs, Bitmaps. [http://redis.io](http://redis.io)

## Introduction

This redis client wrappers and serializers.

## QuickStart

### NuGet

```CLI
Install-Package Zaabee.StackExchangeRedis
Install-Package Zaabee.NewtonsoftJson
```

### Build Project

Create an asp.net core project and import references in startup.cs. Get [Zaabee.StackExchangeRedis](https://github.com/PicoHex/Zaabee.Redis/tree/master/src/Zaabee.StackExchangeRedis/Zaabee.StackExchangeRedis) and [Zaabee.NewtonsoftJson](https://github.com/PicoHex/Zaabee.Serializers/tree/master/src/Zaabee.NewtonsoftJson) from Nuget,otherwise we have other serializers:

[Zaabee.Binary](https://github.com/PicoHex/Zaabee.Serializers/tree/master/src/Zaabee.Binary)

[Zaabee.Jil](https://github.com/PicoHex/Zaabee.Serializers/tree/master/src/Zaabee.Jil)

[Zaabee.MsgPack](https://github.com/PicoHex/Zaabee.Serializers/tree/master/src/Zaabee.MsgPack)

[Zaabee.Protobuf](https://github.com/PicoHex/Zaabee.Serializers/tree/master/src/Zaabee.Protobuf)

[Zaabee.SystemTextJson](https://github.com/PicoHex/Zaabee.Serializers/tree/master/src/Zaabee.SystemTextJson)

[Zaabee.Utf8Json](https://github.com/PicoHex/Zaabee.Serializers/tree/master/src/Zaabee.Utf8Json)

[Zaabee.Xml](https://github.com/PicoHex/Zaabee.Serializers/tree/master/src/Zaabee.Xml)

[Zaabee.ZeroFormatter](https://github.com/PicoHex/Zaabee.Serializers/tree/master/src/Zaabee.ZeroFormatter)

```CSharp
using Zaabee.StackExchangeRedis;
using Zaabee.StackExchangeRedis.Abstractions;
using Zaabee.NewtonsoftJson;
```

Register ZaabyRedisClient in Configuration like

```CSharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddSingleton<IZaabeeRedisClient>(p =>
        new ZaabeeRedisClient(new ZaabeeStackExchangeRedisOptions
                {
                    ConnectionString = "192.168.78.140:6379,abortConnect=false,syncTimeout=3000"),
                    DefaultExpiry = TimeSpan.FromMinutes(10),
                    Serializer = new ZaabeeSerializer()
                });
}
```

Add a TestClass for the demo

```CSharp
public class TestModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

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
