using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleManager.Api.Metadata.Attributes;
using ScheduleManager.Domain.Common;

namespace ScheduleManager.Api.Models.Common
{
    [PageTitles(createPageTitleKey: "Room_Create", editPageTitleKey: "Room_Edit")]
    public class RoomViewModel : ItemViewModel<Room>, IPreviewableItemModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        string IPreviewableItemModel.Id => this.Id.ToString();

        [Display(Name = "Room_Title")]
        [Required(ErrorMessage = "Errors_Required")]
        public string Title { get; set; }

        public override bool Editable => true;

        public override bool Removable => true;

        public override void Initialize(Room entity)
        {
            this.Id = entity.Id;
            this.Title = entity.Title;
        }

        public override async Task<bool> TryUpdateEntityProperties(Room entity)
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