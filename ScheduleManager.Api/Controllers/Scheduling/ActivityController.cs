using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using ScheduleManager.Api.Models.Scheduling;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Api.Controllers.Scheduling
{
    [Authorize]
    public class ActivityController : ItemsController<Activity, ActivityViewModel>
    {
        private readonly IAsyncProvider<TimePeriod> _timePeriodProvider;
        private readonly IAsyncProvider<Room> _roomProvider;
        private readonly IAsyncProvider<Subject> _subjectProvider;
        private readonly IAsyncProvider<Lecturer> _lecturerProvider;
        private readonly IAsyncProvider<Attendee> _attendeeProvider;
        private readonly IAsyncProvider<DaySchedule> _dayScheduleProvider;
        private readonly IStringLocalizer _stringLocalizer;

        public ActivityController(IAsyncProvider<Activity> itemProvider, IAsyncProvider<TimePeriod> timePeriodProvider,
            IAsyncProvider<Room> roomProvider, IAsyncProvider<Subject> subjectProvider, IAsyncProvider<Lecturer> lecturerProvider,
            IAsyncProvider<Attendee> attendeeProvider, IAsyncProvider<DaySchedule> dayScheduleProvider, IStringLocalizer stringLocalizer) : base(itemProvider)
        {
            this._timePeriodProvider = timePeriodProvider;
            this._roomProvider = roomProvider;
            this._subjectProvider = subjectProvider;
            this._lecturerProvider = lecturerProvider;
            this._attendeeProvider = attendeeProvider;
            this._dayScheduleProvider = dayScheduleProvider;
            this._stringLocalizer = stringLocalizer;
        }

        protected override ActivityViewModel CreateEmptyModel()
            => new ActivityViewModel(_timePeriodProvider, _roomProvider, _subjectProvider, _lecturerProvider, _attendeeProvider, _dayScheduleProvider, _stringLocalizer);

        protected override string GetListTitle() => "Activities";
    }
}