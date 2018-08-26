using System;
using System.Collections.Generic;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Scheduling
{
    public class Schedule : Entity
    {
        public virtual DateTime StartDate { get; set; }

        public virtual IDictionary<int, WeekSchedule> Weeks { get; set; }
    }
}