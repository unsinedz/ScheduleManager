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
    [PageTitles(createPageTitleKey: "WeekSchedule_Create", editPageTitleKey: "WeekSchedule_Edit")]
    public class WeekScheduleViewModel : ItemViewModel<WeekSchedule>, IPreviewableItemModel
    {
        private readonly IAsyncProvider<DaySchedule> _dayScheduleProvider;

        [HiddenInput(DisplayValue = false)]
        public virtual Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "WeekSchedule_WeekNumber")]
        [Range(1, 53)]
        public virtual int? WeekNumber { get; set; }

        [Display(Name = "WeekSchedule_Days")]
        [RelatedApiEntitySelector("Api_DaySchedule_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.DaySchedule,
            SelectMultiple = true)]
        public virtual DayScheduleCollection Days { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public WeekScheduleViewModel(IAsyncProvider<DaySchedule> dayScheduleProvider)
        {
            this._dayScheduleProvider = dayScheduleProvider;
        }

        public virtual IEnumerable<ItemFieldInfo> GetItemFields()
        {
            yield return new ItemFieldInfo(nameof(Days), this.Days?.Count.ToString());
            if (WeekNumber.HasValue)
                yield return new ItemFieldInfo(nameof(WeekNumber), this.WeekNumber.ToString());
        }

        public override void Initialize(WeekSchedule entity)
        {
            this.Id = entity.Id;
            this.WeekNumber = entity.WeekNumber;
            this.Days = new DayScheduleCollection((IList<DaySchedule>)entity.Days ?? new List<DaySchedule>(0));
        }

        public override async Task<bool> TryUpdateEntityProperties(WeekSchedule entity)
        {
            var updated = false;
            if (!object.Equals(entity.WeekNumber, this.WeekNumber) && (updated = true))
                entity.WeekNumber = this.WeekNumber;

            updated |= await TryUpdateEntityDayScheduling(entity);
            return updated;
        }

        public virtual async Task<bool> TryUpdateEntityDayScheduling(WeekSchedule entity)
        {
            var entityCollection = (IList<DaySchedule>)entity.Days ?? new List<DaySchedule>();
            var modelCollection = this.Days;
            var preparedModelCollection = await PrepareModelCollection(modelCollection, entityCollection, ids =>
            {
                return _dayScheduleProvider.ListAsync(new Specification<DaySchedule>
                {
                    Criteria = x => ids.Contains(x.Id)
                });
            });
            if (TryUpdateEntityCollection<DaySchedule, IList<DaySchedule>>(entityCollection, preparedModelCollection))
            {
                if (entity.Days == null)
                    entity.Days = new DayScheduleCollection(entityCollection);

                return true;
            }

            return false;
        }
    }
}