using System;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data.Scheduling.Relations
{
    public class WeekScheduleSchedule
    {
        public virtual Guid WeekScheduleId { get; set; }

        public virtual WeekSchedule WeekSchedule { get; set; }

        public virtual Guid ScheduleGroupId { get; set; }

        public virtual ScheduleGroup ScheduleGroup { get; set; }
    }
}