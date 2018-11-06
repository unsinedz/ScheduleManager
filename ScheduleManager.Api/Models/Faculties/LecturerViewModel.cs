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
    public class LecturerViewModel : ItemViewModel<Lecturer>, IPreviewableItemModel
    {
        private readonly IAsyncProvider<Department> _departmentProvider;

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "Lecturer_Name")]
        [Required(ErrorMessage = "Errors_Required")]
        public virtual string Name { get; set; }

        [RelatedApiEntitySelector("Api_Department_List", ApiVersion = Constants.ApiVersions.V1, EntityType = Constants.EntityType.Department,
            Required = true, ErrorMessage = "Errors_Required")]
        public virtual Department Department { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public LecturerViewModel(IAsyncProvider<Department> departmentProvider)
        {
            this._departmentProvider = departmentProvider;
        }

        public IEnumerable<ItemFieldInfo> GetItemFields()
        {
            yield return new ItemFieldInfo(nameof(Name), this.Name);
        }

        public override void Initialize(Lecturer entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Department = entity.Department;
        }

        public override async Task<bool> TryUpdateEntityProperties(Lecturer entity)
        {
            bool updated = false;
            if (!string.Equals(entity.Name, this.Name) && (updated = true))
                entity.Name = this.Name;

            if (!object.Equals(entity.Department?.Id, this.Department?.Id) && (updated = true))
            {
                this.Department = this.Department?.Id == null
                    ? null
                    : await _departmentProvider.GetByIdAsync(this.Department.Id);
            }

            return updated;
        }
    }
}