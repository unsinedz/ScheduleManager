using System;
using Microsoft.Extensions.Localization;

namespace ScheduleManager.Localizations
{
    public static class StringLocalizerExtensions
    {
        public static string LocalizeEnumValue<TEnum>(this IStringLocalizer stringLocalizer, TEnum value)
            where TEnum : Enum
        {
            if (stringLocalizer == null)
                throw new ArgumentNullException(nameof(stringLocalizer));

            var stringifiedValue = value.ToString();
            var key = $"{typeof(TEnum).Name}_{stringifiedValue}";
            var localized = stringLocalizer[key];
            return localized.ResourceNotFound
                ? stringifiedValue
                : localized.Value;
        }
    }
}