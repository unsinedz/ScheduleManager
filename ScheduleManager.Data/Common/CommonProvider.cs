using ScheduleManager.Data.Entities;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Data.Common
{
    public class CommonProvider<T> : AsyncEntityProvider<T, CommonContext>
        where T : Entity
    {
        public CommonProvider(CommonContext context) : base(context)
        {
        }
    }
}