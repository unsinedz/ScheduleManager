using System;
using System.Collections.Generic;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Scheduling
{
    public class DaySchedule : Entity
    {
        public virtual DateTime? DedicatedDate { get; set; }

        public virtual DayOfWeek DayOfWeek { get; set; }

        public virtual ActivityCollection Activities { get; set; }
    }
}