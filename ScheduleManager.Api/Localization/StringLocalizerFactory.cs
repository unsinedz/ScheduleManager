using System;
using Microsoft.Extensions.Localization;

namespace ScheduleManager.Api.Localization
{
    public class StringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IStringLocalizer _stringLocalizer;

        public StringLocalizerFactory(IStringLocalizer stringLocalizer)
        {
            this._stringLocalizer = stringLocalizer;
        }

        public IStringLocalizer Create(Type resourceSource) => _stringLocalizer;

        public IStringLocalizer Create(string baseName, string location) => _stringLocalizer;
    }
}