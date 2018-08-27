using ScheduleManager.Data.Entities;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Data.Scheduling
{
    public class ScheduleProvider<T> : AsyncEntityProvider<T, ScheduleContext>
        where T : Entity
    {
        public ScheduleProvider(ScheduleContext context) : base(context)
        {
        }
    }
}