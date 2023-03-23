using KidsToyHive.Core.Common;
using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Common;

public abstract class Command<TResponse> : AuthenticatedRequest<TResponse>
{
    public virtual int Version { get; }
    public virtual string Key { get; }
    public virtual string TenantKey { get; }
    public virtual IEnumerable<string> SideEffects { get; }
        = new HashSet<string>();
    public string Build(string tenantKey, string entity, string id, string version)
        => id == $"{default(Guid)}" ? $"{IdGenerator.GetNextId()}" : $"{tenantKey}-{entity}-{id}-{version}";
    public string Build(string entity, string id, string version)
        => id == $"{default(Guid)}" ? $"{IdGenerator.GetNextId()}" : $"{entity}-{id}-{version}";
    public string Build(string key, string id)
        => id == $"{default(Guid)}" ? $"{IdGenerator.GetNextId()}" : $"{key}";
}
