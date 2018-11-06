using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Api.Controllers.Api.V1.Faculties
{
    [Area("Api_V1")]
    public class FacultyController : ItemsController<Faculty>
    {
        public FacultyController(IAsyncProvider<Faculty> itemProvider) : base(itemProvider)
        {
        }
    }
}