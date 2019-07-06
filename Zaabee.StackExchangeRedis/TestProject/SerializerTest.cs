using Xunit;

namespace UnitTest
{
    public class SerializerTest
    {
        [Fact]
        public void BinaryTest()
        {
            var serializer = new Zaabee.StackExchangeRedis.Binary.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void JilTest()
        {
            var serializer = new Zaabee.StackExchangeRedis.Jil.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void MsgPackTest()
        {
            var serializer = new Zaabee.StackExchangeRedis.MsgPack.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void NewtonsoftJsonTest()
        {
            var serializer = new Zaabee.StackExchangeRedis.NewtonsoftJson.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void ProtobufTest()
        {
            var serializer = new Zaabee.StackExchangeRedis.Protobuf.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void Utf8JsonTest()
        {
            var serializer = new Zaabee.StackExchangeRedis.Utf8Json.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void ZeroFormatterTest()
        {
            var serializer = new Zaabee.StackExchangeRedis.ZeroFormatter.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }
    }
}