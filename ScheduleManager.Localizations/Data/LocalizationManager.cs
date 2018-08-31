using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using ScheduleManager.Domain.Extensions;

namespace ScheduleManager.Localizations.Data
{
    public class LocalizationManager<TResourceKey, TResourceValue> : IResourceReader
    {
        protected Dictionary<CultureInfo, ILocalizableResourceProvider<TResourceKey, TResourceValue>> Providers { get; set; }

        protected CultureInfo DefaultCulture { get; set; }

        public LocalizationManager(CultureInfo defaultCulture = null)
        {
            Providers = new Dictionary<CultureInfo, ILocalizableResourceProvider<TResourceKey, TResourceValue>>();
            this.DefaultCulture = defaultCulture ?? CultureInfo.InvariantCulture;
        }

        public virtual void AddResourceProvider(ILocalizableResourceProvider<TResourceKey, TResourceValue> provider)
        {
            if (provider == null)
                throw new System.ArgumentNullException(nameof(provider));

            var culture = provider.GetCulture();
            if (culture == null)
                throw new InvalidOperationException("Culture could not be retrieved from provider.");

            if (Providers.ContainsKey(culture))
                throw new InvalidOperationException("Provider with this culture was already added.");

            Providers.Add(culture, provider);
        }

        public void Close()
        {
        }

        public void Dispose()
        {
            this.Providers?.Each(x => x.Value.Dispose());
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return GetMatchingProvider(this.DefaultCulture).Values
                .ToDictionary(x => x.Key, x => x.Value)
                .GetEnumerator();
        }

        public virtual TResourceValue Localize(TResourceKey key, TResourceValue defaultValue = default(TResourceValue), CultureInfo culture = null)
        {
            var provider = GetMatchingProvider(culture ?? this.DefaultCulture);
            var result = provider.Get(key);
            if (IsValueAbsent(result))
                return defaultValue;

            return result;
        }

        protected virtual ILocalizableResourceProvider<TResourceKey, TResourceValue> GetMatchingProvider(CultureInfo culture)
        {
            CultureInfo specifiedNeutralCulture = null;
            CultureInfo defaultNeutralCulture = null;
            if (SupportsCulture(culture))
                return Providers[culture];

            if (!culture.IsNeutralCulture && SupportsCulture(specifiedNeutralCulture = MakeNeutral(culture)))
                return Providers[specifiedNeutralCulture];

            if (!culture.Equals(this.DefaultCulture))
            {
                if (!SupportsCulture(this.DefaultCulture))
                    return Providers[this.DefaultCulture];

                if (!this.DefaultCulture.IsNeutralCulture && SupportsCulture(defaultNeutralCulture = MakeNeutral(this.DefaultCulture)))
                    return Providers[this.DefaultCulture];
            }

            throw new ArgumentException($"Provider for the culture \"{culture.LCID}\" does not exist.", nameof(culture));
        }

        protected virtual bool SupportsCulture(CultureInfo culture)
        {
            if (culture == null)
                throw new ArgumentNullException(nameof(culture));

            return Providers.ContainsKey(culture);
        }

        protected virtual CultureInfo MakeNeutral(CultureInfo culture)
        {
            if (culture == null)
                throw new ArgumentNullException(nameof(culture));

            if (culture.IsNeutralCulture)
                return culture;

            return new CultureInfo(culture.TwoLetterISOLanguageName);
        }

        protected virtual bool IsValueAbsent(TResourceValue value)
        {
            return object.ReferenceEquals(value, null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}