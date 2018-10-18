using System;
using System.Linq.Expressions;

namespace ScheduleManager.Domain.Entities
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification<T> WithCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
            return this;
        }

        public Specification<T> WithPageSize(int pageSize)
        {
            this.PageSize = pageSize;
            return this;
        }

        public Specification<T> WithPageIndex(int pageIndex)
        {
            this.PageIndex = pageIndex;
            return this;
        }

        public Expression<Func<T, bool>> Criteria { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}