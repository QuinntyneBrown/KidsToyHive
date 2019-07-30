using KidsToyHive.Core.Common;
using MediatR;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Common
{
    public abstract class Command<TResponse> : AuthenticatedRequest<TResponse>
    {
        public virtual int Version { get; }
        public virtual string Key { get; }
        public virtual string TenantKey { get; }
        public virtual IEnumerable<string> SideEffects { get; }
            = new HashSet<string>();
        public string Build(string tenantKey, string entity, string id, string version)
            => id == "0" ? $"{IdGenerator.GetNextId()}" : $"{tenantKey}-{entity}-{id}-{version}";

        public string Build(string entity, string id, string version)
            => id == "0" ? $"{IdGenerator.GetNextId()}" : $"{entity}-{id}-{version}";

        public string Build(string key, string id)
            => id == "0" ? $"{IdGenerator.GetNextId()}" : $"{key}";
    }
}
