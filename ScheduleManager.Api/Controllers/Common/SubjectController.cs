using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Common;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Api.Controllers
{
    [Authorize]
    public class SubjectController : ItemsController<Subject, SubjectViewModel>
    {
        public SubjectController(IAsyncProvider<Subject> itemProvider) : base(itemProvider)
        {
        }

        protected override IEnumerable<Subject> OrderListItems(IEnumerable<Subject> items)
        {
            return items.OrderBy(x => x.Title);
        }

        protected override SubjectViewModel CreateEmptyModel() => new SubjectViewModel();

        protected override string GetListTitle() => "Subjects";
    }
}