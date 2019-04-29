using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using ScheduleManager.Domain.Extensions;
using ScheduleManager.Localizations.Data.Strings;

namespace ScheduleManager.Localizations.Data
{
    public class JsonStringProvider : ILocalizableResourceProvider<string, string>
    {
        private readonly ILogger _logger;

        protected virtual string FileName { get; set; }

        protected virtual StringLocalizationDescriptor Descriptor { get; set; }

        public IDictionary<string, string> Values =>
            Descriptor?.Localizations?.ToDictionary(x => x.Key, x => x.Value);

        public JsonStringProvider(string fileName, ILogger logger)
        {
            FileName = fileName;
            this._logger = logger;
        }

        public virtual void Dispose()
        {
            this.Descriptor?.Dispose();
        }

        public virtual string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new System.ArgumentException("Key must not be empty.", nameof(key));

            EnsureDescriptorLoaded();
            return this.Descriptor.Localizations?.SingleOrDefault(x => x.Key.iEquals(key))?.Value;
        }

        public virtual CultureInfo GetCulture()
        {
            EnsureDescriptorLoaded();
            return new CultureInfo(this.Descriptor.CultureCode);
        }

        protected virtual StringLocalizationDescriptor GetDescriptor()
        {
            try
            {
                var source = GetFileContent();
                if (string.IsNullOrEmpty(source))
                    return null;

                return JsonConvert.DeserializeObject<StringLocalizationDescriptor>(source);
            }
            catch (JsonException e)
            {
                _logger.LogError($"[{nameof(JsonStringProvider)}.JsonException]: {e.ToString()}");
            }

            return null;
        }

        protected virtual string GetFileContent()
        {
            try
            {
                return File.ReadAllText(this.FileName);
            }
            catch (IOException e)
            { 
                _logger.LogError($"[{nameof(JsonStringProvider)}.IOException]: {e.ToString()}");
            }

            return null;
        }

        protected virtual void EnsureDescriptorLoaded()
        {
            if (this.Descriptor == null)
            {
                Descriptor = GetDescriptor();
                if (Descriptor == null)
                    throw new InvalidOperationException(string.Format("Descriptor could not be loaded. File path: {0}", this.FileName));
            }
        }
    }
}