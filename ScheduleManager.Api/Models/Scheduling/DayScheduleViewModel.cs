using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.Metadata;
using ScheduleManager.Api.Metadata.Attributes;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Scheduling;

namespace ScheduleManager.Api.Models.Scheduling
{
    [PageTitles(createPageTitleKey: "DaySchedule_Create", editPageTitleKey: "DaySchedule_Edit")]
    public class DayScheduleViewModel : ItemViewModel<DaySchedule>, IPreviewableItemModel
    {
        private readonly IAsyncProvider<Activity> _activityProvider;

        [HiddenInput(DisplayValue = false)]
        public virtual Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "DaySchedule_DedicatedDate")]
        [DataType(DataType.Date)]
        public virtual DateTime? DedicatedDate { get; set; }

        [Display(Name = "DaySchedule_DayOfWeek")]
        public virtual DayOfWeek DayOfWeek { get; set; }

        [Display(Name = "DaySchedule_Activities")]
        [RelatedApiEntitySelector("Api_Activity_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.Activity,
            SelectMultiple = true)]
        public virtual ActivityCollection Activities { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public DayScheduleViewModel(IAsyncProvider<Activity> activityProvider)
        {
            this._activityProvider = activityProvider;
        }

        public virtual IEnumerable<ItemFieldInfo> GetItemFields()
        {
            yield return new ItemFieldInfo(nameof(DayOfWeek), this.DayOfWeek.ToString());
            yield return new ItemFieldInfo(nameof(Activities), this.Activities?.Count.ToString());
            yield return new ItemFieldInfo(nameof(DedicatedDate), this.DedicatedDate?.ToString("d"));
        }

        public override void Initialize(DaySchedule entity)
        {
            this.Id = entity.Id;
            this.DedicatedDate = entity.DedicatedDate;
            this.DayOfWeek = entity.DayOfWeek;
            this.Activities = new ActivityCollection((IList<Activity>)entity.Activities ?? new List<Activity>(0));
        }

        public override async Task<bool> TryUpdateEntityProperties(DaySchedule entity)
        {
            var updated = false;
            if (!object.Equals(entity.DedicatedDate, this.DedicatedDate) && (updated = true))
                entity.DedicatedDate = this.DedicatedDate;

            if (entity.DayOfWeek != this.DayOfWeek && (updated = true))
                entity.DayOfWeek = this.DayOfWeek;

            updated |= await TryUpdateEntityActivities(entity);
            return updated;
        }

        public virtual async Task<bool> TryUpdateEntityActivities(DaySchedule entity)
        {
            var entityCollection = (IList<Activity>)entity.Activities ?? new List<Activity>();
            var modelCollection = this.Activities;
            var preparedModelCollection = await PrepareModelCollection(modelCollection, entityCollection, ids =>
            {
                return _activityProvider.ListAsync(new Specification<Activity>
                {
                    Criteria = x => ids.Contains(x.Id)
                });
            });
            if (TryUpdateEntityCollection<Activity, IList<Activity>>(entityCollection, preparedModelCollection))
            {
                if (entity.Activities == null)
                    entity.Activities = new ActivityCollection(entityCollection);

                return true;
            }

            return false;
        }
    }
}