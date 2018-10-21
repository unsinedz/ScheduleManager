using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Controllers.Api;
using ScheduleManager.Api.Models.Faculties;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Api.Controllers
{
    [Authorize]
    public class LecturerController : ItemsController<Lecturer, LecturerViewModel>
    {
        private readonly IAsyncProvider<Department> _departmentProvider;

        public LecturerController(IAsyncProvider<Lecturer> lecturerProvider, IAsyncProvider<Department> departmentProvider)
            : base(lecturerProvider)
        {
            this._departmentProvider = departmentProvider;
        }

        protected override IEnumerable<Lecturer> OrderListItems(IEnumerable<Lecturer> items)
        {
            return items.OrderBy(x => x.Name);
        }

        protected override LecturerViewModel CreateEmptyModel() =>
            new LecturerViewModel(_departmentProvider);

        protected override string GetListTitle() => "Lecturers";
    }
}