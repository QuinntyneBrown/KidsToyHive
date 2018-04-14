using System;

namespace Core.Entities
{
    public class BaseEntity: ILoggable
    {
        public int BaseEntityId { get; set; }                   
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public virtual Tenant Tenant { get; set; }
        public bool IsDeleted { get; set; }

    }
}
