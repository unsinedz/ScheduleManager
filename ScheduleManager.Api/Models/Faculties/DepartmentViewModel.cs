using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Data.Faculties;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Extensions;
using ScheduleManager.Api.Metadata.Attributes;
using ScheduleManager.Api.Metadata;

namespace ScheduleManager.Api.Models.Faculties
{
    [PageTitles(createPageTitleKey: "Department_Create", editPageTitleKey: "Department_Edit")]
    public class DepartmentViewModel : ItemViewModel<Department>, IPreviewableItemModel
    {
        private readonly IAsyncProvider<Faculty> _facultyProvider;
        private readonly IAsyncProvider<Lecturer> _lecturerProvider;

        [HiddenInput(DisplayValue = false)]
        public virtual Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "Department_Title")]
        [Required(ErrorMessage = "Errors_Required")]
        [StringLength(50, ErrorMessage = "Errors_StringLength")]
        public virtual string Title { get; set; }

        [RelatedApiEntitySelector("Api_Lecturer_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.Lecturer,
            SelectMultiple = true)]
        public virtual IList<Lecturer> Lecturers { get; set; }

        [RelatedApiEntitySelector("Api_Faculty_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.Faculty,
            Required = true, ErrorMessage = "Errors_Required")]
        public virtual Faculty Faculty { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public DepartmentViewModel(IAsyncProvider<Faculty> facultyProvider, IAsyncProvider<Lecturer> lecturerProvider)
        {
            this._facultyProvider = facultyProvider;
            this._lecturerProvider = lecturerProvider;
        }

        public IEnumerable<ItemFieldInfo> GetItemFields()
        {
            yield return new ItemFieldInfo(nameof(Title), Title);
        }

        public override void Initialize(Department entity)
        {
            this.Id = entity.Id;
            this.Title = entity.Title;
            this.Faculty = entity.Faculty;
            this.Lecturers = new List<Lecturer>(entity.Lecturers ?? Enumerable.Empty<Lecturer>());
        }

        public override async Task<bool> TryUpdateEntityProperties(Department entity)
        {
            bool updated = false;
            if (!string.Equals(entity.Title, this.Title) && (updated = true))
                entity.Title = this.Title;

            if (!object.Equals(entity.Faculty?.Id, this.Faculty?.Id) && (updated = true))
            {
                entity.Faculty = this.Faculty?.Id == null
                    ? null
                    : await _facultyProvider.GetByIdAsync(this.Faculty.Id);
            }

            updated |= await TryUpdateLecturers(entity);
            return updated;
        }

        protected virtual async Task<bool> TryUpdateLecturers(Department entity)
        {
            var entityCollection = entity.Lecturers ?? new List<Lecturer>();
            var modelCollection = this.Lecturers;
            var preparedModelCollection = await PrepareModelCollection(modelCollection, entityCollection, ids =>
            {
                return _lecturerProvider.ListAsync(new Specification<Lecturer>
                {
                    Criteria = x => ids.Contains(x.Id)
                });
            });
            if (TryUpdateEntityCollection<Lecturer, IList<Lecturer>>(entityCollection, preparedModelCollection))
            {
                if (entity.Lecturers == null)
                    entity.Lecturers = entityCollection;

                return true;
            }

            return false;
        }
    }
}