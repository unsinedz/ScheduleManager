using System.Collections.Generic;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Faculties
{
    public class Department : Entity
    {
        public virtual string Title { get; set; }

        public virtual IList<Lecturer> Lecturers { get; set; }

        public virtual Faculty Faculty { get; set; }
    }
}