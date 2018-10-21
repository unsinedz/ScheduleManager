using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Proxies.Internal;
using Newtonsoft.Json.Serialization;

namespace ScheduleManager.Api.Serialization.Json
{
    public class IgnoreProxiesLowercaseContractResolver : DefaultContractResolver
    {
        public override JsonContract ResolveContract(Type type)
        {
            return base.ResolveContract(GetSerializationType(type));
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return base.ResolvePropertyName(propertyName).ToLower();
        }

        private Type GetSerializationType(Type type)
        {
            return type.GetInterfaces().Contains(typeof(IProxyLazyLoader))
                ? type.BaseType
                : type;
        }
    }
}