using System;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Data.Scheduling.Relations
{
    public class ActivityAttendee
    {
        public virtual Guid ActivityId { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual Guid AttendeeId { get; set; }

        public virtual Attendee Attendee { get; set; }
    }
}