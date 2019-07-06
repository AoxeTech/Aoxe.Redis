using System;
using ProtoBuf;

namespace UnitTest
{
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
}