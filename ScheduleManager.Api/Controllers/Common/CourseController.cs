using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Common;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Api.Controllers
{
    [Authorize]
    public class CourseController : ItemsController<Course, CourseViewModel>
    {
        public CourseController(IAsyncProvider<Course> itemProvider) : base(itemProvider)
        {
        }

        protected override IEnumerable<Course> OrderListItems(IEnumerable<Course> items)
        {
            return items.OrderBy(x => x.Title);
        }

        protected override CourseViewModel CreateEmptyModel() =>
            new CourseViewModel();

        protected override string GetListTitle() => "Courses";
    }
}