using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ScheduleManager.Api.Metadata.Attributes.Validation
{
    public class SignedCompareAttribute : CompareAttribute
    {
        public byte ExpectedComparisonSign { get; private set; }

        public string OverridenOtherProperty { get; private set; }

        public SignedCompareAttribute(string otherProperty, byte expectedComparisonSign) : base(otherProperty)
        {
            this.ExpectedComparisonSign = expectedComparisonSign;
            this.OverridenOtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
            => string.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name, OtherPropertyDisplayName ?? OverridenOtherProperty);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            var otherPropertyInterfaces = otherPropertyInfo.PropertyType.GetInterfaces();
            if (!otherPropertyInterfaces.Contains(typeof(IComparable))
                && !otherPropertyInterfaces.Contains(typeof(IComparable<>)))
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (!MatchesExpectedSign(Comparer.Default.Compare(value, otherPropertyValue)))
            {
                if (OverridenOtherProperty == null)
                    OverridenOtherProperty = GetDisplayNameForProperty(validationContext.ObjectType, OtherProperty);

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        private bool MatchesExpectedSign(int comparisonResult)
        {
            if (comparisonResult == 0)
                return ExpectedComparisonSign == 0;

            return Math.Sign(comparisonResult) * Math.Sign(ExpectedComparisonSign) > 0;
        }

        private static string GetDisplayNameForProperty(Type containerType, string propertyName)
        {
            var property = containerType.GetProperties().FirstOrDefault(x => Equals(x.Name, propertyName));
            if (property == null)
                throw new ArgumentException($"Property {propertyName} can not be found for the type {containerType}.");

            var attributes = property.GetCustomAttributes(true);
            var display = attributes.OfType<DisplayAttribute>().FirstOrDefault();
            if (display != null)
                return display.GetName();

            var displayName = attributes.OfType<DisplayNameAttribute>().FirstOrDefault();
            if (displayName != null)
                return displayName.DisplayName;

            return propertyName;
        }

        private void MergeAttributes(IDictionary<string, string> attributes, string name, string value)
        {
            if (!attributes.ContainsKey(name))
                attributes.Add(name, value);
        }
    }
}