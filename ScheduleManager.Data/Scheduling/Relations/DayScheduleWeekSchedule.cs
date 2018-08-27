using System;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data.Scheduling.Relations
{
    public class DayScheduleWeekSchedule
    {
        public Guid DayScheduleId { get; set; }

        public DaySchedule DaySchedule { get; set; }

        public Guid WeekScheduleId { get; set; }

        public WeekSchedule WeekSchedule { get; set; }
    }
}