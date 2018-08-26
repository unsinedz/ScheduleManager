using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Common
{
    public class Room : Entity
    {
        public virtual string Title { get; set; }
    }
}