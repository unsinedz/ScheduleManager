using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace ScheduleManager.Api.Metadata.Attributes.Validation
{
    public class RangeAttributeAdapter : AttributeAdapterBase<RangeAttribute>
    {
        public RangeAttributeAdapter(RangeAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var attributes = context.Attributes;
            MergeAttribute(attributes, "data-val", "true");
            MergeAttribute(attributes, "data-val-range", GetErrorMessage(context));
            if (Attribute.Minimum != null)
            {
                var minimum = Attribute.Minimum.ToString();
                MergeAttribute(attributes, "data-val-range-min", minimum);
                MergeAttribute(attributes, "min", minimum);
            }

            if (Attribute.Maximum != null)
            {
                var maximum = Attribute.Maximum.ToString();
                MergeAttribute(attributes, "data-val-range-max", maximum);
                MergeAttribute(attributes, "max", maximum);
            }
        }

        public override string GetErrorMessage(ModelValidationContextBase context)
        {
            var metadata = context.ModelMetadata;
            return GetErrorMessage(metadata, metadata.GetDisplayName(), Attribute.Minimum, Attribute.Maximum);
        }
    }
}