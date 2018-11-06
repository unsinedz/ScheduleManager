using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Api.Controllers.Api.V1.Faculties
{
    [Area("Api_V1")]
    public class LecturerController : ItemsController<Lecturer>
    {
        public LecturerController(IAsyncProvider<Lecturer> itemProvider) : base(itemProvider)
        {
        }
    }
}