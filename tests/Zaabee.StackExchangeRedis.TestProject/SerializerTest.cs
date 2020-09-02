using Xunit;
using Zaabee.StackExchangeRedis.Serializer.Abstractions;

namespace Zaabee.StackExchangeRedis.TestProject
{
    public class SerializerTest
    {
        [Fact]
        public void BinaryTest() => SerializerUnitTest(new Binary.Serializer());

        [Fact]
        public void JilTest() => SerializerUnitTest(new Jil.Serializer());

        [Fact]
        public void MsgPackTest() => SerializerUnitTest(new MsgPack.Serializer());

        [Fact]
        public void NewtonsoftJsonTest() => SerializerUnitTest(new NewtonsoftJson.Serializer());

        [Fact]
        public void ProtobufTest() => SerializerUnitTest(new Protobuf.Serializer());

        [Fact]
        public void SystemTextJsonTest() => SerializerUnitTest(new SystemTextJson.Serializer());

        [Fact]
        public void Utf8JsonTest() => SerializerUnitTest(new Utf8Json.Serializer());

        [Fact]
        public void XmlJsonTest() => SerializerUnitTest(new Xml.Serializer());

#if NETCOREAPP2_1
        [Fact]
        public void ZeroFormatterTest()=>SerializerUnitTest(new ZeroFormatter.Serializer());
#endif
        
        private static void SerializerUnitTest(ISerializer serializer)
        {
            var model = TestModelFactory.CreateTestModel();
            var bytes = serializer.Serialize(model);
            var result = serializer.Deserialize<TestModel>(bytes);
            Assert.Equal(model, result);
        }
    }
}