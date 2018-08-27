using ScheduleManager.Data.Entities;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Data.Faculties
{
    public class FacultyProvider<T> : AsyncEntityProvider<T, FacultyContext>
        where T : Entity
    {
        public FacultyProvider(FacultyContext context) : base(context)
        {
        }
    }
}