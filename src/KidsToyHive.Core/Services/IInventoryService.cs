// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using System;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Services;

public interface IInventoryService
{
    Task<bool> IsItemAvailable(DateTime date, BookingTimeSlot timeSlot, Guid productId);
}

