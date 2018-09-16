using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ScheduleManager.Domain.Extensions;

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

        public abstract Task<bool> TryUpdateEntityProperties(TItem entity);

        protected virtual bool TryUpdateEntityCollection<T, TCollection>(ref TCollection entityCollection, ref TCollection modelCollection, Func<TCollection> defaultInstatiator)
            where TCollection : ICollection<T>
        {
            if (defaultInstatiator == null)
                throw new ArgumentNullException(nameof(defaultInstatiator));

            if (object.ReferenceEquals(entityCollection, modelCollection))
                return false;

            if (entityCollection.IsNullOrEmpty() && modelCollection.IsNullOrEmpty())
                return false;

            if (entityCollection == null)
                entityCollection = defaultInstatiator();

            if (modelCollection == null)
                modelCollection = defaultInstatiator();

            var collectionsDiffer = !modelCollection.IsSimilarAs(entityCollection);
            if (collectionsDiffer)
            {
                entityCollection.Clear();
                entityCollection.AddRange(modelCollection);
            }

            return collectionsDiffer;
        }
    }
}