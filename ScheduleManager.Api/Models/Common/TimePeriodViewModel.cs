using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.Metadata.Attributes;
using ScheduleManager.Api.Metadata.Attributes.Validation;
using ScheduleManager.Domain.Common;

namespace ScheduleManager.Api.Models.Common
{
    [PageTitles(createPageTitleKey: "TimePeriod_Create", editPageTitleKey: "TimePeriod_Edit")]
    public class TimePeriodViewModel : ItemViewModel<TimePeriod>, IPreviewableItemModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "TimePeriod_StartTime")]
        [Required(ErrorMessage = "Errors_Required")]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Display(Name = "TimePeriod_EndTime")]
        [Required(ErrorMessage = "Errors_Required")]
        [DataType(DataType.Time)]
        [SignedCompare(nameof(StartTime), 1, ErrorMessage = "Errors_PropertyMustBeGreaterThanProperty")]
        public DateTime EndTime { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public override void Initialize(TimePeriod entity)
        {
            this.Id = entity.Id;
            this.StartTime = entity.Start;
            this.EndTime = entity.End;
        }

        public override async Task<bool> TryUpdateEntityProperties(TimePeriod entity)
        {
            var updated = false;
            if (!string.Equals(entity.Start, this.StartTime) && (updated = true))
                entity.Start = this.StartTime;

            if (!string.Equals(entity.End, this.EndTime) && (updated = true))
                entity.End = this.EndTime;

            await Task.CompletedTask;
            return updated;
        }

        public IEnumerable<ItemFieldInfo> GetItemFields()
        {
            yield return new ItemFieldInfo(nameof(StartTime), StartTime.ToShortTimeString());
            yield return new ItemFieldInfo(nameof(EndTime), EndTime.ToShortTimeString());
        }
    }
}