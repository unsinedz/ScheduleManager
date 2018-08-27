using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleManager.Domain.Entities
{
    public interface IAsyncProvider<T>
    {
        Task<T> CreateAsync(T entity);

        Task<T> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> ListAsync();

        Task<IEnumerable<T>> ListAsync(ISpecification<T> specification);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);
    }
}