using System;
using System.Collections.Generic;

namespace ScheduleManager.Api.Models.Editors
{
    public class PreviewableItemsViewModel
    {
        public string Title { get; set; }
        
        public Func<object, string> EditUrl { get; set; }

        public Func<object, string> DeleteUrl { get; set; }
        
        public IEnumerable<IPreviewableItemModel> PreviewableItems { get; set; }
    }
}