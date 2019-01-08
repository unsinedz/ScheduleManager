using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScheduleManager.Domain.Scheduling
{
    public class WeekScheduleCollection : KeyedCollection<int, WeekSchedule>, IList<WeekSchedule>, IEnumerable<WeekSchedule>, ICollection<WeekSchedule>
    {
        protected override int GetKeyForItem(WeekSchedule item)
        {
            return item.WeekNumber.GetValueOrDefault();
        }
    }
}