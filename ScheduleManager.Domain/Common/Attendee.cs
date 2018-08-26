using System;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Domain.Common
{
    public class Attendee : Entity
    {
        public virtual AttendeeType AttendeeType { get; set; }

        public virtual string Name { get; set; }

        public virtual Course Course { get; set; }

        public virtual Faculty Faculty { get; set; }
    }
}