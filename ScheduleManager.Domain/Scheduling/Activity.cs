using System.Collections.Generic;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Domain.Scheduling
{
    public class Activity : Entity
    {
        public virtual TimePeriod TimePeriod { get; set; }

        public virtual string Title { get; set; }

        public virtual Room Room { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        public virtual IList<Attendee> Attendees { get; set; }
    }
}