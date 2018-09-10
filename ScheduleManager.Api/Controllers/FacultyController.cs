using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Faculties;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Api.Controllers
{
    [Authorize]
    public class FacultyController : ItemsController<Faculty, FacultyViewModel>
    {
        public FacultyController(IAsyncProvider<Faculty> itemProvider) : base(itemProvider)
        {
        }
    }
}