using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Domain.Models;

public class BaseModel
{
    [ForeignKey("Tenant")]
    public Guid? TenantId { get; set; }
    public BaseModel() => _domainEvents = new List<INotification>();
    private List<INotification> _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
    public void RaiseDomainEvent(INotification eventItem) => _domainEvents.Add(eventItem);
    public void ClearEvents() => _domainEvents.Clear();
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public int Version { get; set; }
    public Tenant Tenant { get; set; }
}
