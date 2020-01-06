using Xunit;

namespace Zaabee.StackExchangeRedis.TestProject
{
    public class SerializerTest
    {
        [Fact]
        public void BinaryTest()
        {
            var serializer = new Binary.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void JilTest()
        {
            var serializer = new Jil.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void MsgPackTest()
        {
            var serializer = new MsgPack.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void NewtonsoftJsonTest()
        {
            var serializer = new NewtonsoftJson.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void ProtobufTest()
        {
            var serializer = new Protobuf.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

        [Fact]
        public void Utf8JsonTest()
        {
            var serializer = new Utf8Json.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }

#if NETCOREAPP2_1
        [Fact]
        public void ZeroFormatterTest()
        {
            var serializer = new ZeroFormatter.Serializer();
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }
#endif
    }
}