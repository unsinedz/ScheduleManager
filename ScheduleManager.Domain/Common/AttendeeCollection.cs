using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScheduleManager.Domain.Common
{
    public class AttendeeCollection : Collection<Attendee>, ICollection<Attendee>, IList<Attendee>, IEnumerable<Attendee>
    {
    }
}