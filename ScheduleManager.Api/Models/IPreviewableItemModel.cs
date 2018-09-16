using System.Collections.Generic;

namespace ScheduleManager.Api.Models
{
    public interface IPreviewableItemModel
    {
        string Id { get; }

        IEnumerable<ItemFieldInfo> GetItemFields();

        bool Editable { get; }

        bool Removable { get; }
    }
}