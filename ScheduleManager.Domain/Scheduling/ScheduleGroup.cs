using System;
using System.Collections.Generic;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Scheduling
{
    public class ScheduleGroup : Entity
    {
        public virtual DateTime StartDate { get; set; }

        public virtual WeekScheduleCollection Weeks { get; set; }
    }
}