using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Proxies.Internal;
using Newtonsoft.Json.Serialization;

namespace ScheduleManager.Api.Serialization.Json
{
    public class IgnoreProxiesContractResolver : DefaultContractResolver
    {
        public override JsonContract ResolveContract(Type type)
        {
            return base.ResolveContract(GetSerializationType(type));
        }

        private Type GetSerializationType(Type type)
        {
            return type.GetInterfaces().Contains(typeof(IProxyLazyLoader))
                ? type.BaseType
                : type;
        }
    }
}