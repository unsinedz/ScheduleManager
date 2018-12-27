using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Scheduling;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Api.Controllers.Scheduling
{
    [Authorize]
    public class DayScheduleController : ItemsController<DaySchedule, DayScheduleViewModel>
    {
        private readonly IAsyncProvider<Activity> _activityProvider;

        public DayScheduleController(IAsyncProvider<DaySchedule> itemProvider, IAsyncProvider<Activity> activityProvider) : base(itemProvider)
        {
            this._activityProvider = activityProvider;
        }

        protected override DayScheduleViewModel CreateEmptyModel() => new DayScheduleViewModel(_activityProvider);

        protected override string GetListTitle() => "DayScheduling";
    }
}