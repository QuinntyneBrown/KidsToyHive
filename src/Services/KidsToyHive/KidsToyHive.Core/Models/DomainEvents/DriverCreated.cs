// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;

namespace KidsToyHive.Core.Models.DomainEvents;

public class DriverCreated : INotification
{
    public DriverCreated(Driver driver)
    {
        Driver = driver;
    }
    public Driver Driver { get; private set; }
}

