using System;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Common
{
    public class TimePeriod : Entity
    {
        public virtual DateTime Start { get; set; }

        public virtual DateTime End { get; set; }
    }
}