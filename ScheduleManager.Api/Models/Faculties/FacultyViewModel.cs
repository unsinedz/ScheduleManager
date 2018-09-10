using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Extensions;
using System;

namespace ScheduleManager.Api.Models.Faculties
{
    public class FacultyViewModel : ItemViewModel<Faculty>, IPreviewableItemModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Display(Name = "Faculty_Title")]
        [Required(ErrorMessage = "Errors_Required")]
        public virtual string Title { get; set; }

        [UIHint("DragNDrop")]
        [ScaffoldColumn(false)]
        public virtual IList<Department> Departments { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public IEnumerable<ItemFieldInfo> GetItemFields()
        {
            yield return new ItemFieldInfo(nameof(Title), this.Title);
        }

        public override void Initialize(Faculty entity)
        {
            this.Id = entity.Id;
            this.Title = entity.Title;
            this.Departments = new List<Department>(entity.Departments);
        }

        public override bool TryUpdateEntity(Faculty entity)
        {
            bool updated = false;
            if (!entity.Title.Equals(this.Title) && (updated = true))
                entity.Title = this.Title;

            updated &= TryUpdateDepartments(entity);
            return updated;
        }

        protected virtual bool TryUpdateDepartments(Faculty entity)
        {
            if (object.ReferenceEquals(this.Departments, entity.Departments))
                return false;

            if (entity.Departments.IsNullOrEmpty() && this.Departments.IsNullOrEmpty())
                return false;

            if (entity.Departments == null)
                entity.Departments = new List<Department>();

            if (this.Departments == null)
                this.Departments = new List<Department>();

            var collectionsDiffer = !this.Departments.IsSimilarAs(entity.Departments);
            if (collectionsDiffer)
            {
                entity.Departments.Clear();
                foreach (var department in this.Departments)
                    entity.Departments.Add(department);
            }

            return collectionsDiffer;
        }
    }
}