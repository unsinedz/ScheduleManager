using System.Collections.Generic;

namespace ScheduleManager.Api.Models
{
    public interface IPreviewableItemModel
    {
        IEnumerable<ItemFieldInfo> GetItemFields();
    }
}