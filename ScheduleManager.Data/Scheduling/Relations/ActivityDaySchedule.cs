using System;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data.Scheduling.Relations
{
    public class ActivityDaySchedule
    {
        public virtual Guid ActivityId { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual Guid DayScheduleId { get; set; }

        public virtual DaySchedule DaySchedule { get; set; }
    }
}