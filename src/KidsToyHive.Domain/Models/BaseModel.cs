using System;

namespace KidsToyHive.Domain.Models
{
    public class BaseModel
    {
        public Guid TenantKey { get; set; }
        public int Version { get; set; }
    }
}
