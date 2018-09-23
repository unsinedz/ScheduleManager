using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Extensions;
using System;
using ScheduleManager.Domain.Entities;

namespace ScheduleManager.Api.Models.Faculties
{
    public class FacultyViewModel : ItemViewModel<Faculty>, IPreviewableItemModel
    {
        private readonly IAsyncProvider<Department> _departmentProvider;

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "Faculty_Title")]
        [Required(ErrorMessage = "Errors_Required")]
        public virtual string Title { get; set; }

        [UIHint("DragNDrop")]
        [ScaffoldColumn(false)]
        public virtual IList<Department> Departments { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public FacultyViewModel(IAsyncProvider<Department> departmentProvider)
        {
            this._departmentProvider = departmentProvider;
        }

        public IEnumerable<ItemFieldInfo> GetItemFields()
        {
            yield return new ItemFieldInfo(nameof(Title), this.Title);
        }

        public override void Initialize(Faculty entity)
        {
            this.Id = entity.Id;
            this.Title = entity.Title;
            this.Departments = new List<Department>(entity.Departments ?? Enumerable.Empty<Department>());
        }

        public override async Task<bool> TryUpdateEntityProperties(Faculty entity)
        {
            bool updated = false;
            if (!string.Equals(entity.Title, this.Title) && (updated = true))
                entity.Title = this.Title;

            updated |= await TryUpdateDepartments(entity);
            return updated;
        }

        protected virtual async Task<bool> TryUpdateDepartments(Faculty entity)
        {
            var entityCollection = entity.Departments;
            var modelCollection = this.Departments;
            if (TryUpdateEntityCollection<Department, IList<Department>>(ref entityCollection, ref modelCollection, () => new List<Department>())
                && !(entityCollection.Count == 0 && entity.Departments == null))
            {
                var preparedCollection = await Task.WhenAll(entityCollection.Select(x => _departmentProvider.GetByIdAsync(x.Id)));
                if (entity.Departments == null)
                    entity.Departments = preparedCollection;
                else
                {
                    entity.Departments.Clear();
                    entity.Departments.AddRange(preparedCollection);
                }

                return true;
            }

            return false;
        }
    }
}