// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Core.Models;

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

