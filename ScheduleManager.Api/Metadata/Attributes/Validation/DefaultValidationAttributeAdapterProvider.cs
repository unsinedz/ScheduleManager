using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace ScheduleManager.Api.Metadata.Attributes.Validation
{
    public class DefaultValidationAttributeAdapterProvider : ValidationAttributeAdapterProvider, IValidationAttributeAdapterProvider
    {
        IAttributeAdapter IValidationAttributeAdapterProvider.GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is SignedCompareAttribute signedCompareAttribute)
                return new SignedCompareAttributeAdapter(signedCompareAttribute, stringLocalizer);

            return base.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}