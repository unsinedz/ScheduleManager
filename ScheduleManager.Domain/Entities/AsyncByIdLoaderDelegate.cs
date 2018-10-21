using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleManager.Domain.Entities
{
    public delegate Task<IEnumerable<T>> AsyncByIdLoaderDelegate<T, TId>(IEnumerable<TId> ids);
}