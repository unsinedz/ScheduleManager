using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScheduleManager.Domain.Scheduling
{
    public class DayScheduleCollection : KeyedCollection<DayOfWeek, DaySchedule>, IList<DaySchedule>, IEnumerable<DaySchedule>, ICollection<DaySchedule>
    {
        protected override DayOfWeek GetKeyForItem(DaySchedule item)
        {
            return item.DayOfWeek;
        }
    }
}