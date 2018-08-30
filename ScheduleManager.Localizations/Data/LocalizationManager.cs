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
        private readonly Dictionary<CultureInfo, ILocalizableResourceProvider<TResourceKey, TResourceValue>> _providers;
        private readonly CultureInfo _defaultCulture;

        public LocalizationManager(CultureInfo defaultCulture = null)
        {
            _providers = new Dictionary<CultureInfo, ILocalizableResourceProvider<TResourceKey, TResourceValue>>();
            this._defaultCulture = defaultCulture ?? CultureInfo.InvariantCulture;
        }

        public virtual void AddResourceProvider(ILocalizableResourceProvider<TResourceKey, TResourceValue> provider)
        {
            if (provider == null)
                throw new System.ArgumentNullException(nameof(provider));

            var culture = provider.GetCulture();
            if (culture == null)
                throw new InvalidOperationException("Culture could not be retrieved from provider.");

            if (_providers.ContainsKey(culture))
                throw new InvalidOperationException("Provider with this culture was already added.");

            _providers.Add(culture, provider);
        }

        public void Close()
        {
        }

        public void Dispose()
        {
            this._providers?.Each(x => x.Value.Dispose());
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return GetMatchingProvider(this._defaultCulture).Values
                .ToDictionary(x => x.Key, x => x.Value)
                .GetEnumerator();
        }

        public virtual TResourceValue Localize(TResourceKey key, TResourceValue defaultValue = default(TResourceValue), CultureInfo culture = null)
        {
            var provider = GetMatchingProvider(culture ?? this._defaultCulture);
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
                return _providers[culture];

            if (!culture.IsNeutralCulture && SupportsCulture(specifiedNeutralCulture = MakeNeutral(culture)))
                return _providers[specifiedNeutralCulture];

            if (!culture.Equals(this._defaultCulture))
            {
                if (!SupportsCulture(this._defaultCulture))
                    return _providers[this._defaultCulture];

                if (!this._defaultCulture.IsNeutralCulture && SupportsCulture(defaultNeutralCulture = MakeNeutral(this._defaultCulture)))
                    return _providers[this._defaultCulture];
            }

            throw new ArgumentException($"Provider for the culture \"{culture.LCID}\" does not exist.", nameof(culture));
        }

        protected virtual bool SupportsCulture(CultureInfo culture)
        {
            if (culture == null)
                throw new ArgumentNullException(nameof(culture));

            return _providers.ContainsKey(culture);
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