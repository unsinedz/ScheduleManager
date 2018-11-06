using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.Metadata;
using ScheduleManager.Api.Metadata.Attributes;
using ScheduleManager.Domain.Common;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Api.Models.Common
{
    [PageTitles(createPageTitleKey: "Attendee_Create", editPageTitleKey: "Attendee_Edit")]
    public class AttendeeViewModel : ItemViewModel<Attendee>, IPreviewableItemModel
    {
        private readonly IAsyncProvider<Course> _courseProvider;

        private readonly IAsyncProvider<Faculty> _facultyProvider;

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "Attendee_Type")]
        [Required(ErrorMessage = "Errors_Required")]
        [UIHint("Enum")]
        public AttendeeType? AttendeeType { get; set; }

        [Display(Name = "Attendee_Name")]
        [Required(ErrorMessage = "Errors_Required")]
        public string Name { get; set; }

        [Display(Name = "Attendee_Course")]
        [RelatedApiEntitySelector("Api_Course_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.Course,
            Required = true, ErrorMessage = "Errors_Required")]
        public Course Course { get; set; }

        [Display(Name = "Attendee_Faculty")]
        [RelatedApiEntitySelector("Api_Faculty_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.Faculty,
            Required = true, ErrorMessage = "Errors_Required")]
        public Faculty Faculty { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public AttendeeViewModel(IAsyncProvider<Course> courseProvider, IAsyncProvider<Faculty> facultyProvider)
        {
            this._courseProvider = courseProvider;
            this._facultyProvider = facultyProvider;
        }

        public override void Initialize(Attendee entity)
        {
            this.Id = entity.Id;
            this.AttendeeType = entity.AttendeeType;
            this.Name = entity.Name;
            this.Course = entity.Course;
            this.Faculty = entity.Faculty;
        }

        public override async Task<bool> TryUpdateEntityProperties(Attendee entity)
        {
            var updated = false;
            if (entity.AttendeeType != this.AttendeeType.Value && (updated = true))
                entity.AttendeeType = this.AttendeeType.Value;

            if (!string.Equals(entity.Name, this.Name) && (updated = true))
                entity.Name = this.Name;

            if (!object.Equals(entity.Course?.Id, this.Course?.Id) && (updated = true))
            {
                entity.Course = this.Course?.Id == null
                    ? null
                    : await _courseProvider.GetByIdAsync(this.Course.Id);
            }

            if (!object.Equals(entity.Faculty?.Id, this.Faculty?.Id) && (updated = true))
            {
                entity.Faculty = this.Faculty?.Id == null
                    ? null
                    : await _facultyProvider.GetByIdAsync(this.Faculty.Id);
            }

            return updated;
        }

        public IEnumerable<ItemFieldInfo> GetItemFields()
        {
            yield return new ItemFieldInfo(nameof(Name), Name);
            yield return new ItemFieldInfo(nameof(AttendeeType), AttendeeType.ToString());
            yield return new ItemFieldInfo(nameof(Course), Course?.Title);
        }
    }
}