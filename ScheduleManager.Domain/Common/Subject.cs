using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Common
{
    public class Subject : Entity
    {
        public virtual string Title { get; set; }
    }
}