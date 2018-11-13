using System;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data.Scheduling.Relations
{
    public class DayScheduleWeekSchedule
    {
        public virtual Guid DayScheduleId { get; set; }

        public virtual DaySchedule DaySchedule { get; set; }

        public virtual Guid WeekScheduleId { get; set; }

        public virtual WeekSchedule WeekSchedule { get; set; }
    }
}