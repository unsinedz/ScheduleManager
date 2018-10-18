using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ScheduleManager.Domain.Entities
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; set; }

        int PageSize { get; set; }

        int PageIndex { get; set; }
    }
}