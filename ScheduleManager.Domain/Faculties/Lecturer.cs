using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Faculties
{
    public class Lecturer : Entity
    {
        public virtual string Name { get; set; }

        public virtual Department Department { get; set; }
    }
}