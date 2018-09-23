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

namespace ScheduleManager.Api.Models.Faculties
{
    [PageTitles(createPageTitleKey: "Department_Create", editPageTitleKey: "Department_Edit")]
    public class DepartmentViewModel : ItemViewModel<Department>, IPreviewableItemModel
    {
        private readonly IAsyncProvider<Faculty> _facultyProvider;
        private readonly IAsyncProvider<Lecturer> _lecturerProvider;

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "Department_Title")]
        [Required(ErrorMessage = "Errors_Required")]
        [StringLength(50, ErrorMessage = "Errors_StringLength")]
        public virtual string Title { get; set; }

        [UIHint("DragNDrop")]
        [ScaffoldColumn(false)]
        public virtual IList<Lecturer> Lecturers { get; set; }

        [ScaffoldColumn(false)]
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
            this.Lecturers = entity.Lecturers;
        }

        public override async Task<bool> TryUpdateEntityProperties(Department entity)
        {
            bool updated = false;
            if (!string.Equals(entity.Title, this.Title) && (updated = true))
                entity.Title = this.Title;

            var entityHasFaculty = entity.Faculty != null;
            var modelHasFaculty = this.Faculty != null;
            var facultyIdsDifferent = entityHasFaculty && modelHasFaculty && !entity.Faculty.Id.Equals(this.Faculty.Id);
            if ((facultyIdsDifferent || (!entityHasFaculty && modelHasFaculty)) && (updated = true))
                entity.Faculty = await _facultyProvider.GetByIdAsync(this.Faculty.Id);
            else if (!modelHasFaculty && (updated = true))
                entity.Faculty = null;

            updated |= await TryUpdateLecturers(entity);
            return updated;
        }

        protected virtual async Task<bool> TryUpdateLecturers(Department entity)
        {
            var entityCollection = entity.Lecturers;
            var modelCollection = this.Lecturers;
            if (TryUpdateEntityCollection<Lecturer, IList<Lecturer>>(ref entityCollection, ref modelCollection, () => new List<Lecturer>())
                && !(entityCollection.Count == 0 && entity.Lecturers == null))
            {
                var preparedCollection = await Task.WhenAll(entityCollection.Select(x => _lecturerProvider.GetByIdAsync(x.Id)));
                if (entity.Lecturers == null)
                    entity.Lecturers = preparedCollection;
                else
                {
                    entity.Lecturers.Clear();
                    entity.Lecturers.AddRange(preparedCollection);
                }
                
                return true;
            }

            return false;
        }
    }
}