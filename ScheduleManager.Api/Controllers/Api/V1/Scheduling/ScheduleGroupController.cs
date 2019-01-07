using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Api.Controllers.Api.V1.Scheduling
{
    [Area("Api_V1")]
    public class ScheduleGroupController : ItemsController<ScheduleGroup>
    {
        public ScheduleGroupController(IAsyncProvider<ScheduleGroup> itemProvider) : base(itemProvider)
        {
        }
    }
}