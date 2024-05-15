namespace Zaabee.StackExchangeRedis.TestProject;

[ProtoContract]
[Serializable]
public class TestModel
{
    [ProtoMember(1)]
    public virtual Guid Id { get; set; }

    [ProtoMember(2)]
    public virtual string Name { get; set; }

    [ProtoMember(3)]
    public virtual int Age { get; set; }

    [ProtoMember(4)]
    public virtual DateTime CreateTime { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is not TestModel o)
            return false;
        return Id == o.Id && Name == o.Name && Age == o.Age && CreateTime == o.CreateTime;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Id.GetHashCode();
            hashCode = (hashCode * 397) ^ (Name is not null ? Name.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ Age;
            hashCode = (hashCode * 397) ^ CreateTime.GetHashCode();
            return hashCode;
        }
    }
}
