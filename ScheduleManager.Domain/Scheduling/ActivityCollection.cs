using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScheduleManager.Domain.Scheduling
{
    public class ActivityCollection : Collection<Activity>, IList<Activity>, IEnumerable<Activity>, ICollection<Activity>
    {
        
    }
}