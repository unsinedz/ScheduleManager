using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace ScheduleManager.Api.Metadata.Attributes.Validation
{
    public class SignedCompareAttributeAdapter : AttributeAdapterBase<SignedCompareAttribute>
    {
        private readonly IStringLocalizer _stringLocalizer;

        public SignedCompareAttributeAdapter(SignedCompareAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-signedcompare", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-signedcompare-to", Attribute.OtherProperty);
            MergeAttribute(context.Attributes, "data-val-signedcompare-sign", Attribute.ExpectedComparisonSign.ToString());
        }

        public override string GetErrorMessage(ModelValidationContextBase context)
        {
            return GetErrorMessage(context.ModelMetadata, context.ModelMetadata.GetDisplayName(), Attribute.OtherPropertyDisplayName ?? GetDisplayNameForProperty(context.ModelMetadata.ContainerType, Attribute.OtherProperty));
        }

        private string GetDisplayNameForProperty(Type containerType, string propertyName)
        {
            var property = containerType.GetProperties().FirstOrDefault(x => Equals(x.Name, propertyName));
            if (property == null)
                throw new ArgumentException($"Property {propertyName} can not be found for the type {containerType}.");

            var attributes = property.GetCustomAttributes(true);
            var display = attributes.OfType<DisplayAttribute>().FirstOrDefault();
            if (display != null)
                return LocalizeOrDefault(display.GetName());

            var displayName = attributes.OfType<DisplayNameAttribute>().FirstOrDefault();
            if (displayName != null)
                return LocalizeOrDefault(displayName.DisplayName);

            return propertyName;
        }

        private string LocalizeOrDefault(string value)
        {
            var localized = _stringLocalizer.GetString(value);
            if (localized.ResourceNotFound)
                return value;

            return localized.Value;
        }
    }
}