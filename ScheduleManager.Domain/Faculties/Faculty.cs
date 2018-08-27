using System.Collections.Generic;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Domain.Faculties
{
    public class Faculty : Entity
    {
        public virtual string Title { get; set; }

        public virtual IList<Department> Departments { get; set; }
    }
}