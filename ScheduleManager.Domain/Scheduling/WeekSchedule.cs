using System;
using System.Collections.Generic;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Scheduling
{
    public class WeekSchedule : Entity
    {
        public virtual int? WeekNumber { get; set; }

        public virtual DayScheduleCollection Days { get; set; }
    }
}