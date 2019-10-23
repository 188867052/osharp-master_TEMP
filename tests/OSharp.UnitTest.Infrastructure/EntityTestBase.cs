using System;
using System.Collections.Generic;
using OSharp.Extensions;

namespace OSharp.UnitTest.Infrastructure
{
    public abstract class EntityTestBase
    {
        protected readonly IEnumerable<TestEntity> Entities;

        protected EntityTestBase()
        {
            List<TestEntity> entities = new List<TestEntity>();
            DateTime dt = DateTime.Now;
            Random rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                entities.Add(new TestEntity()
                {
                    Id = i + 1,
                    Name = "Name" + (i + 1),
                    AddDate = rnd.NextDateTime(dt.AddDays(-7), dt.AddDays(7)),
                    IsDeleted = rnd.NextBoolean(),
                });
            }

            this.Entities = entities;
        }
    }
}