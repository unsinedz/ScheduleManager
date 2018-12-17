using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Api.Controllers.Api.V1.Scheduling
{
    [Area("Api_V1")]
    public class ActivityController : ItemsController<Activity>
    {
        public ActivityController(IAsyncProvider<Activity> itemProvider) : base(itemProvider)
        {
        }
    }
}