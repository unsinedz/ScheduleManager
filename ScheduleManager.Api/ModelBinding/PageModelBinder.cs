using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ScheduleManager.Api.ModelBinding
{
    public class PageModelBinder : IModelBinder
    {
        private const string ModelName = "page";

        private readonly Type[] SupportedTypes = new Type[]
        {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong)
        };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new System.ArgumentNullException(nameof(bindingContext));

            if (!SupportedTypes.Contains(bindingContext.ModelType))
                return Task.CompletedTask;

            var modelName = bindingContext.BinderModelName;
            if (string.IsNullOrEmpty(modelName))
                modelName = ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            var model = AdjustValue(value);
            if (model != null)
                bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }

        private object AdjustValue(string value)
        {
            if (byte.TryParse(value, out var byteValue))
                return byteValue - 1;

            if (sbyte.TryParse(value, out var sbyteValue))
                return sbyteValue - 1;

            if (short.TryParse(value, out var shortValue))
                return shortValue - 1;

            if (ushort.TryParse(value, out var ushortValue))
                return ushortValue - 1;

            if (int.TryParse(value, out var intValue))
                return intValue - 1;

            if (uint.TryParse(value, out var uintValue))
                return uintValue - 1;

            if (long.TryParse(value, out var longValue))
                return longValue - 1;

            if (ulong.TryParse(value, out var ulongValue))
                return ulongValue - 1;

            return null;
        }
    }
}