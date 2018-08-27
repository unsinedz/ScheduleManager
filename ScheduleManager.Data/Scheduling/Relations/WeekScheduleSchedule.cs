using System;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data.Scheduling.Relations
{
    public class WeekScheduleSchedule
    {
        public Guid WeekScheduleId { get; set; }

        public WeekSchedule WeekSchedule { get; set; }

        public Guid ScheduleGroupId { get; set; }

        public ScheduleGroup ScheduleGroup { get; set; }
    }
}