using ScheduleManager.Data.Entities;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Data.Faculties
{
    public class FacultyProvider<T> : AsyncEntityProvider<T, ApplicationContext>
        where T : Entity
    {
        public FacultyProvider(ApplicationContext context) : base(context)
        {
        }
    }
}