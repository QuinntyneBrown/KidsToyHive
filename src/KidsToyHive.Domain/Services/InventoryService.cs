using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services;

public class InventoryService : IInventoryService
{
    private readonly IAppDbContext _context;
    public InventoryService(IAppDbContext context)
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
