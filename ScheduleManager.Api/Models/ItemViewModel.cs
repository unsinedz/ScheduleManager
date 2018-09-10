using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ScheduleManager.Api.Models
{
    public abstract class ItemViewModel<TItem>
    {
        [ScaffoldColumn(false)]
        public virtual bool PreviewableInList => this is IPreviewableItemModel;

        [ScaffoldColumn(false)]
        public abstract bool Editable { get; }

        [ScaffoldColumn(false)]
        public abstract bool Removable { get; }

        public abstract void Initialize(TItem entity);

        public abstract bool TryUpdateEntity(TItem entity);
    }
}