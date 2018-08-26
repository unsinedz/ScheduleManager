using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ScheduleManager.Data.Generators
{
    public class GuidGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;

        protected override object NextValue(EntityEntry entry)
        {
            return Guid.NewGuid();
        }
    }
}