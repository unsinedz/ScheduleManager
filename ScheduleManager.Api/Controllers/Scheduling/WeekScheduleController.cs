using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.Models.Scheduling;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Api.Controllers.Scheduling
{
    [Authorize]
    public class WeekScheduleController : ItemsController<WeekSchedule, WeekScheduleViewModel>
    {
        private readonly IAsyncProvider<DaySchedule> _dayScheduleProvider;

        public WeekScheduleController(IAsyncProvider<WeekSchedule> itemProvider, IAsyncProvider<DaySchedule> dayScheduleProvider) : base(itemProvider)
        {
            this._dayScheduleProvider = dayScheduleProvider;
        }

        protected override WeekScheduleViewModel CreateEmptyModel() => new WeekScheduleViewModel(_dayScheduleProvider);

        protected override string GetListTitle() => "WeekScheduling";
    }
}