using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ScheduleManager.Domain.Entities;
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

        protected virtual bool TryUpdateEntityCollection<T, TCollection>(TCollection entityCollection, TCollection modelCollection)
            where TCollection : ICollection<T>
        {
            if (entityCollection == null)
                throw new ArgumentNullException(nameof(entityCollection));

            if (modelCollection == null)
                throw new ArgumentNullException(nameof(modelCollection));

            if (object.ReferenceEquals(entityCollection, modelCollection))
                return false;

            if (entityCollection.IsNullOrEmpty() && modelCollection.IsNullOrEmpty())
                return false;

            if (modelCollection.Count == 0)
            {
                var updated = entityCollection.Count > 0;
                entityCollection.Clear();
                return updated;
            }

            var collectionsDiffer = !modelCollection.IsSimilarAs(entityCollection);
            if (collectionsDiffer)
            {
                entityCollection.Clear();
                entityCollection.AddRange(modelCollection);
            }

            return collectionsDiffer;
        }

        protected virtual async Task<IList<T>> PrepareModelCollection<T>(IList<T> modelCollection, IList<T> entityCollection, AsyncByIdLoaderDelegate<T, Guid> byIdLoader)
            where T : Entity
        {
            IList<T> preparedModelCollection = null;
            if (!modelCollection.IsNullOrEmpty())
            {
                // load tracked entities and mark untracked
                var entityIdsToLoad = new List<Guid>();
                for (var i = 0; i < modelCollection.Count; i++)
                {
                    var loadedEntity = entityCollection?.FirstOrDefault(x => x.Id == modelCollection[i].Id);
                    if (loadedEntity == null)
                        entityIdsToLoad.Add(modelCollection[i].Id);
                    else
                        modelCollection[i] = loadedEntity;
                }

                // load untracked entities
                var loadedEntities = await byIdLoader(entityIdsToLoad);
                var absentEntityIds = entityIdsToLoad.Except(loadedEntities.Select(x => x.Id)).ToList();
                preparedModelCollection = modelCollection.Where(x => !absentEntityIds.Contains(x.Id)).ToList();
                for (var i = 0; i < preparedModelCollection.Count; i++)
                {
                    var loadedEntity = loadedEntities.FirstOrDefault(x => x.Id == preparedModelCollection[i].Id);
                    if (loadedEntity != null)
                        preparedModelCollection[i] = loadedEntity;
                }
            }

            return preparedModelCollection ?? modelCollection ?? new List<T>();
        }
    }
}