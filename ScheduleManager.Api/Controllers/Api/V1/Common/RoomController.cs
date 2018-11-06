using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Api.Controllers.Api.V1.Common
{
    [Area("Api_V1")]
    public class RoomController : ItemsController<Room>
    {
        public RoomController(IAsyncProvider<Room> itemProvider) : base(itemProvider)
        {
        }
    }
}