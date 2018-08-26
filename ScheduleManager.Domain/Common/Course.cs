using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Common
{
    public class Course : Entity
    {
        public virtual string Title { get; set; }
    }
}