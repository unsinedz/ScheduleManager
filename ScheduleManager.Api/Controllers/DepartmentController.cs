using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Faculties;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Api.Controllers
{
    [Authorize]
    public class DepartmentController : ItemsController<Department, DepartmentViewModel>
    {
        public DepartmentController(IAsyncProvider<Department> itemProvider) : base(itemProvider)
        {
        }
    }
}