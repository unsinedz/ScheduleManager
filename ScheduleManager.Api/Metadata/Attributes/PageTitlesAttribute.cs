using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Keys = ScheduleManager.Api.Metadata.Constants.Keys;

namespace ScheduleManager.Api.Metadata.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PageTitlesAttribute : Attribute, IMetadataAware
    {        
        public string CreatePageTitleKey { get; }
        
        public string EditPageTitleKey { get; }

        public PageTitlesAttribute(string createPageTitleKey = "Create", string editPageTitleKey = "Edit")
        {
            this.CreatePageTitleKey = createPageTitleKey;
            this.EditPageTitleKey = editPageTitleKey;
        }

        public void OnDisplayMetadataCreated(DisplayMetadata metadata)
        {
            if (!metadata.AdditionalValues.ContainsKey(Keys.CreatePageTitle))
                metadata.AdditionalValues.Add(Keys.CreatePageTitle, this.CreatePageTitleKey);

            if (!metadata.AdditionalValues.ContainsKey(Keys.EditPageTitle))
                metadata.AdditionalValues.Add(Keys.EditPageTitle, this.EditPageTitleKey);
        }
    }
}