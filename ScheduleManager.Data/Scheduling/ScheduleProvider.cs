using ScheduleManager.Data.Entities;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Data.Scheduling
{
    public class ScheduleProvider<T> : AsyncEntityProvider<T, ApplicationContext>
        where T : Entity
    {
        public ScheduleProvider(ApplicationContext context) : base(context)
        {
        }
    }
}