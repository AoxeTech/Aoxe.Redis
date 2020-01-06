using System;

namespace Zaabee.StackExchangeRedis.TestProject
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
                CreateTime = new DateTime(2000,1,1).ToUniversalTime()
            };
        }
    }
}