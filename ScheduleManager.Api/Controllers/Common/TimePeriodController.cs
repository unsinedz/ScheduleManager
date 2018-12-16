using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Common;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Api.Controllers
{
    [Authorize]
    public class TimePeriodController : ItemsController<TimePeriod, TimePeriodViewModel>
    {
        public TimePeriodController(IAsyncProvider<TimePeriod> itemProvider) : base(itemProvider)
        {
        }

        protected override IEnumerable<TimePeriod> OrderListItems(IEnumerable<TimePeriod> items)
        {
            return items.OrderBy(x => x.Start).ThenBy(x => x.End);
        }

        protected override string GetListTitle() => "TimePeriods";
    }
}