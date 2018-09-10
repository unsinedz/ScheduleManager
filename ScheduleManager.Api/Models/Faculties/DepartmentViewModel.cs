using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Faculties;

namespace ScheduleManager.Api.Models.Faculties
{
    public class DepartmentViewModel : ItemViewModel<Department>, IPreviewableItemModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

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

        public override bool TryUpdateEntity(Department entity)
        {
            bool updated = false;
            if (!entity.Title.Equals(this.Title) && (updated = true))
                entity.Title = this.Title;

            return updated;
        }

        protected virtual bool TryUpdate
    }
}