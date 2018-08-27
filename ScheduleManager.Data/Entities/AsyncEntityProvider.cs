using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Data.Entities
{
    public class AsyncEntityProvider<TEntity, TContext> : IAsyncProvider<TEntity>
        where TEntity : Entity
        where TContext : DbContext
    {
        private readonly TContext _context;

        public AsyncEntityProvider(TContext context)
        {
            this._context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<TEntity> GetByIdAsync(Guid id)
        {
            return _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> ListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> specification)
        {
            return await _context.Set<TEntity>()
                .Where(specification.Criteria)
                .ToListAsync();
        }

        public Task RemoveAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}