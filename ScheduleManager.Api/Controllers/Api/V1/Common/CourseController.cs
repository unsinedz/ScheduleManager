using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Api.Controllers.Api.V1.Common
{
    [Area("Api_V1")]
    public class CourseController : ItemsController<Course>
    {
        public CourseController(IAsyncProvider<Course> itemProvider) : base(itemProvider)
        {
        }
    }
}