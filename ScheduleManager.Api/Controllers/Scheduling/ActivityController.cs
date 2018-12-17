using Microsoft.AspNetCore.Authorization;
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

        public ActivityController(IAsyncProvider<Activity> itemProvider, IAsyncProvider<TimePeriod> timePeriodProvider,
            IAsyncProvider<Room> roomProvider, IAsyncProvider<Subject> subjectProvider, IAsyncProvider<Lecturer> lecturerProvider,
            IAsyncProvider<Attendee> attendeeProvider) : base(itemProvider)
        {
            this._timePeriodProvider = timePeriodProvider;
            this._roomProvider = roomProvider;
            this._subjectProvider = subjectProvider;
            this._lecturerProvider = lecturerProvider;
            this._attendeeProvider = attendeeProvider;
        }

        protected override ActivityViewModel CreateEmptyModel()
            => new ActivityViewModel(_timePeriodProvider, _roomProvider, _subjectProvider, _lecturerProvider, _attendeeProvider);

        protected override string GetListTitle() => "Activities";
    }
}