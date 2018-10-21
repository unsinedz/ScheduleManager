using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Editors;
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

        protected override IEnumerable<Department> OrderListItems(IEnumerable<Department> items)
        {
            return items.OrderBy(x => x.Title);
        }

        protected override DepartmentViewModel CreateEmptyModel() =>
            new DepartmentViewModel(_facultyProvider, _lecturerProvider);

        protected override string GetListTitle() => "Departments";
    }
}