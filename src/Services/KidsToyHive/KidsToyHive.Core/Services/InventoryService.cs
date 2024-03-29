// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Services;

public class InventoryService : IInventoryService
{
    private readonly IKidsToyHiveDbContext _context;
    public InventoryService(IKidsToyHiveDbContext context)
    {
        _context = context;
    }
    public async Task<bool> IsItemAvailable(DateTime date, BookingTimeSlot timeSlot, Guid productId)
    {
        List<Booking> bookings;
        var inventoryItem = _context.InventoryItems.Single(x => x.ProductId == productId);
        if (timeSlot == BookingTimeSlot.FullDay)
        {
            bookings = await _context.Bookings
                .Where(x => x.Date == date && x.BookingDetails.Any(x => x.ProductId == productId))
                .ToListAsync();
        }
        else
        {
            bookings = await _context.Bookings
                .Where(x => x.Date == date
                && (x.BookingTimeSlot == timeSlot || x.BookingTimeSlot == BookingTimeSlot.FullDay)
                && x.BookingDetails.Any(x => x.ProductId == productId))
                .ToListAsync();
        }
        return inventoryItem.Quantity > bookings.Count;
    }
}

