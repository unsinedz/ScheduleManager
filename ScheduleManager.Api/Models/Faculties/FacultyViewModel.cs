using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Domain.Faculties;
using ScheduleManager.Domain.Extensions;
using System;
using ScheduleManager.Domain.Entities;
using ScheduleManager.Api.Metadata.Attributes;
using ScheduleManager.Api.Metadata;

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

        [RelatedApiEntitySelector("Api_Department_List", ApiVersion = "V1", EntityType = Constants.EntityType.Department,
            SelectMultiple = true)]
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
            var entityCollection = entity.Departments ?? new List<Department>();
            var modelCollection = this.Departments;
            var preparedModelCollection = await PrepareModelCollection(modelCollection, entityCollection, ids =>
            {
                return _departmentProvider.ListAsync(new Specification<Department>
                {
                    Criteria = x => ids.Contains(x.Id)
                });
            });
            if (TryUpdateEntityCollection<Department, IList<Department>>(entityCollection, preparedModelCollection))
            {
                if (entity.Departments == null)
                    entity.Departments = entityCollection;

                return true;
            }

            return false;
        }
    }
}