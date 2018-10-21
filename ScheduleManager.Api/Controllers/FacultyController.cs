using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Faculties;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Api.Controllers
{
    [Authorize]
    public class FacultyController : ItemsController<Faculty, FacultyViewModel>
    {
        private readonly IAsyncProvider<Department> _departmentProvider;

        public FacultyController(IAsyncProvider<Faculty> itemProvider, IAsyncProvider<Department> departmentProvider) : base(itemProvider)
        {
            this._departmentProvider = departmentProvider;
        }

        protected override FacultyViewModel CreateEmptyModel() =>
            new FacultyViewModel(_departmentProvider);

        protected override string GetListTitle() => "Faculties";
    }
}