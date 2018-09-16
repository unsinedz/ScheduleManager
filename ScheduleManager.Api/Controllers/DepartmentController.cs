using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Faculties;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Api.Controllers
{
    [Authorize]
    public class DepartmentController : ItemsController<Department, DepartmentViewModel>
    {
        private readonly IAsyncProvider<Faculty> _facultyProvider;
        private readonly IAsyncProvider<Lecturer> _lecturerProvider;

        public DepartmentController(IAsyncProvider<Department> itemProvider, IAsyncProvider<Faculty> facultyProvider,
            IAsyncProvider<Lecturer> lecturerProvider) : base(itemProvider)
        {
            this._facultyProvider = facultyProvider;
            this._lecturerProvider = lecturerProvider;
        }

        protected override DepartmentViewModel CreateEmptyModel()
        {
            return new DepartmentViewModel(_facultyProvider, _lecturerProvider);
        }

        protected override string GetListTitle() => "Departments";
    }
}