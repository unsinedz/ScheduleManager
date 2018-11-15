using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ScheduleManager.Api.Models.Common;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Api.Controllers
{
    [Authorize]
    public class RoomController : ItemsController<Room, RoomViewModel>
    {
        public RoomController(IAsyncProvider<Room> itemProvider) : base(itemProvider)
        {
        }

        protected override IEnumerable<Room> OrderListItems(IEnumerable<Room> items)
        {
            return items.OrderBy(x => x.Title);
        }

        protected override RoomViewModel CreateEmptyModel() => new RoomViewModel();

        protected override string GetListTitle() => "Rooms";
    }
}