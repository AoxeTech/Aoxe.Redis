using System;

namespace UnitTest
{
    public static class TestModelFactory
    {
        public static TestModel CreateTestModel()
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