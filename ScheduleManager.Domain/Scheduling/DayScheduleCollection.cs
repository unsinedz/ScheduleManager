using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScheduleManager.Domain.Scheduling
{
    public class DayScheduleCollection : KeyedCollection<DaySchedule.Key, DaySchedule>, IList<DaySchedule>, IEnumerable<DaySchedule>, ICollection<DaySchedule>
    {
        public DayScheduleCollection() : base()
        {
        }
        
        public DayScheduleCollection(IList<DaySchedule> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            
            foreach (var item in list)
                this.Add(item);
        }

        protected override DaySchedule.Key GetKeyForItem(DaySchedule item)
        {
            return new DaySchedule.Key(item.DayOfWeek, item.DedicatedDate);
        }
    }
}