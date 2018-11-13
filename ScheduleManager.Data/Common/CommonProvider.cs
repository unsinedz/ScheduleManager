using ScheduleManager.Data.Entities;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Data.Common
{
    public class CommonProvider<T> : AsyncEntityProvider<T, ApplicationContext>
        where T : Entity
    {
        public CommonProvider(ApplicationContext context) : base(context)
        {
        }
    }
}