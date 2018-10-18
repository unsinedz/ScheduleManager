using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using static ScheduleManager.Api.Metadata.Constants;

namespace ScheduleManager.Api.Metadata.Attributes
{
    public class RelatedEntitySelectorAttribute : Attribute, IMetadataAware
    {
        private const string EditorTemplateHint = "RelatedEntity";

        public virtual string RouteName { get; }

        public virtual string RouteArea { get; set; }

        public virtual bool SelectMultiple { get; set; } = false;

        public virtual string EntityType { get; set; }

        public virtual bool Required { get; set; } = false;

        public virtual string ErrorMessage { get; set; }

        public RelatedEntitySelectorAttribute(string routeName)
        {
            if (string.IsNullOrWhiteSpace(routeName))
                throw new ArgumentException("Route name must be specified.", nameof(routeName));

            RouteName = routeName;
        }

        public void OnDisplayMetadataCreated(DisplayMetadata metadata)
        {
            metadata.TemplateHint = EditorTemplateHint;
            metadata.AdditionalValues.Add(Keys.RelatedEntityFetchRoute, this.RouteName);
            metadata.AdditionalValues.Add(Keys.RelatedEntitySelectMultiple, this.SelectMultiple);
            metadata.AdditionalValues.Add(Keys.RelatedEntityRequired, this.Required);
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                metadata.AdditionalValues.Add(Keys.RelatedEntityErrorMessage, this.ErrorMessage);

            if (!string.IsNullOrEmpty(this.RouteArea))
                metadata.AdditionalValues.Add(Keys.RelatedEntityFetchRouteArea, this.RouteArea);

            if (!string.IsNullOrEmpty(this.EntityType))
                metadata.AdditionalValues.Add(Keys.RelatedEntityType, this.EntityType);
        }
    }
}