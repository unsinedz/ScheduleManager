using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.Metadata.Attributes;
using ScheduleManager.Domain.Common;

namespace ScheduleManager.Api.Models.Common
{
    [PageTitles(createPageTitleKey: "Course_Create", editPageTitleKey: "Course_Edit")]
    public class CourseViewModel : ItemViewModel<Course>, IPreviewableItemModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "Course_Title")]
        [Required(ErrorMessage = "Errors_Required")]
        [StringLength(50, ErrorMessage = "Errors_StringLength")]
        public string Title { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public override void Initialize(Course entity)
        {
            this.Id = entity.Id;
            this.Title = entity.Title;
        }

        public override async Task<bool> TryUpdateEntityProperties(Course entity)
        {
            var updated = false;
            if (!string.Equals(entity.Title, this.Title) && (updated = true))
                entity.Title = this.Title;

            await Task.CompletedTask;
            return updated;
        }

        public IEnumerable<ItemFieldInfo> GetItemFields()
        {
            yield return new ItemFieldInfo(nameof(Title), Title);
        }
    }
}