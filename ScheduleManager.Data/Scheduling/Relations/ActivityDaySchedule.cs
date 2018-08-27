using System;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data.Scheduling.Relations
{
    public class ActivityDaySchedule
    {
        public Guid ActivityId { get; set; }

        public Activity Activity { get; set; }
        
        public Guid DayScheduleId { get; set; }
        
        public DaySchedule DaySchedule { get; set; }
    }
}