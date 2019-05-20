using System;

namespace UnitTest
{
    public class TestModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime CreateTime { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is TestModel o)) return false;
            return Id == o.Id && Name == o.Name && Age == o.Age && CreateTime == o.CreateTime;
        }

        protected bool Equals(TestModel other)
        {
            return Id.Equals(other.Id) && string.Equals(Name, other.Name) && Age == other.Age &&
                   CreateTime.Equals(other.CreateTime);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Age;
                hashCode = (hashCode * 397) ^ CreateTime.GetHashCode();
                return hashCode;
            }
        }
    }
}