using System;
using System.Collections.Generic;

namespace ScheduleManager.Domain.Entities
{
    public interface IProvider<T> where T : Entity
    {
        T Create(T entity);

        T GetById(Guid id);

        IEnumerable<T> List();

        IEnumerable<T> List(ISpecification<T> specification);

        void Update(T entity);

        void Remove(T entity);
    }
}