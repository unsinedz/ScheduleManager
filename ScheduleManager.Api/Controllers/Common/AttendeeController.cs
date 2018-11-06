using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Common;
using ScheduleManager.Api.Models.Editors;
using ScheduleManager.Api.Models.Faculties;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Api.Controllers
{
    [Authorize]
    public class AttendeeController : ItemsController<Attendee, AttendeeViewModel>
    {
        private readonly IAsyncProvider<Course> _courseProvider;

        private readonly IAsyncProvider<Faculty> _facultyProvider;

        public AttendeeController(IAsyncProvider<Attendee> itemProvider, IAsyncProvider<Course> courseProvider,
            IAsyncProvider<Faculty> facultyProvider) : base(itemProvider)
        {
            this._courseProvider = courseProvider;
            this._facultyProvider = facultyProvider;
        }

        protected override IEnumerable<Attendee> OrderListItems(IEnumerable<Attendee> items)
        {
            return items.OrderBy(x => x.Name)
                .ThenBy(x => x.AttendeeType);
        }

        protected override AttendeeViewModel CreateEmptyModel() =>
            new AttendeeViewModel(_courseProvider, _facultyProvider);

        protected override string GetListTitle() => "Attendees";
    }
}