// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using System;

namespace KidsToyHive.Core.Models.DomainEvents;

public class ShipmentCompleted : INotification
{
    public Guid ShipmentId { get; set; }
}

