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
    [PageTitles(createPageTitleKey: "Activity_Create", editPageTitleKey: "Activity_Edit")]
    public class ActivityViewModel : ItemViewModel<Activity>, IPreviewableItemModel
    {
        private readonly IAsyncProvider<TimePeriod> _timePeriodProvider;
        private readonly IAsyncProvider<Room> _roomProvider;
        private readonly IAsyncProvider<Subject> _subjectProvider;
        private readonly IAsyncProvider<Lecturer> _lecturerProvider;
        private readonly IAsyncProvider<Attendee> _attendeeProvider;

        [HiddenInput(DisplayValue = false)]
        public virtual Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "Faculty_Title")]

        [Required(ErrorMessage = "Errors_Required")]
        [StringLength(50, ErrorMessage = "Errors_StringLength")]
        public virtual string Title { get; set; }

        [Display(Name = "Activity_TimePeriod")]
        [RelatedApiEntitySelector("Api_TimePeriod_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.TimePeriod,
            Required = true, ErrorMessage = "Errors_Required")]
        public virtual TimePeriod TimePeriod { get; set; }

        [Display(Name = "Activity_Room")]
        [RelatedApiEntitySelector("Api_Room_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.Room,
            Required = true, ErrorMessage = "Errors_Required")]
        public virtual Room Room { get; set; }

        [Display(Name = "Activity_Subject")]
        [RelatedApiEntitySelector("Api_Subject_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.Subject,
            Required = true, ErrorMessage = "Errors_Required")]
        public virtual Subject Subject { get; set; }

        [Display(Name = "Activity_Lecturer")]
        [RelatedApiEntitySelector("Api_Lecturer_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.Lecturer,
            Required = true, ErrorMessage = "Errors_Required")]
        public virtual Lecturer Lecturer { get; set; }

        [Display(Name = "Activity_Attendees")]
        [RelatedApiEntitySelector("Api_Attendee_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.Attendee,
            SelectMultiple = true)]
        public virtual IList<Attendee> Attendees { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public ActivityViewModel(IAsyncProvider<TimePeriod> timePeriodProvider, IAsyncProvider<Room> roomProvider,
            IAsyncProvider<Subject> subjectProvider, IAsyncProvider<Lecturer> lecturerProvider, IAsyncProvider<Attendee> attendeeProvider)
        {
            this._timePeriodProvider = timePeriodProvider;
            this._roomProvider = roomProvider;
            this._subjectProvider = subjectProvider;
            this._lecturerProvider = lecturerProvider;
            this._attendeeProvider = attendeeProvider;
        }

        public virtual IEnumerable<ItemFieldInfo> GetItemFields()
        {
            yield return new ItemFieldInfo(nameof(Title), this.Title);
        }

        public override void Initialize(Activity entity)
        {
            this.Id = entity.Id;
            this.TimePeriod = entity.TimePeriod;
            this.Title = entity.Title;
            this.Room = entity.Room;
            this.Subject = entity.Subject;
            this.Lecturer = entity.Lecturer;
            this.Attendees = new List<Attendee>(entity.Attendees ?? Enumerable.Empty<Attendee>());
        }

        public override async Task<bool> TryUpdateEntityProperties(Activity entity)
        {
            var updated = false;
            if (!string.Equals(entity.Title, this.Title) && (updated = true))
                entity.Title = this.Title;

            if (!object.Equals(entity.TimePeriod?.Id, this.TimePeriod?.Id) && (updated = true))
            {
                entity.TimePeriod = this.TimePeriod?.Id == null
                    ? null
                    : await _timePeriodProvider.GetByIdAsync(this.TimePeriod.Id);
            }

            if (!object.Equals(entity.Room?.Id, this.Room?.Id) && (updated = true))
            {
                entity.Room = this.Room?.Id == null
                    ? null
                    : await _roomProvider.GetByIdAsync(this.Room.Id);
            }

            if (!object.Equals(entity.Subject?.Id, this.Subject?.Id) && (updated = true))
            {
                entity.Subject = this.Subject?.Id == null
                    ? null
                    : await _subjectProvider.GetByIdAsync(this.Subject.Id);
            }

            if (!object.Equals(entity.Lecturer?.Id, this.Lecturer?.Id) && (updated = true))
            {
                entity.Lecturer = this.Lecturer?.Id == null
                    ? null
                    : await _lecturerProvider.GetByIdAsync(this.Lecturer.Id);
            }

            updated |= await TryUpdateEntityAttendees(entity);
            return updated;
        }

        public virtual async Task<bool> TryUpdateEntityAttendees(Activity entity)
        {
            var entityCollection = entity.Attendees ?? new List<Attendee>();
            var modelCollection = this.Attendees;
            var preparedModelCollection = await PrepareModelCollection(modelCollection, entityCollection, ids =>
            {
                return _attendeeProvider.ListAsync(new Specification<Attendee>
                {
                    Criteria = x => ids.Contains(x.Id)
                });
            });
            if (TryUpdateEntityCollection<Attendee, IList<Attendee>>(entityCollection, preparedModelCollection))
            {
                if (entity.Attendees == null)
                    entity.Attendees = entityCollection;

                return true;
            }

            return false;
        }
    }
}