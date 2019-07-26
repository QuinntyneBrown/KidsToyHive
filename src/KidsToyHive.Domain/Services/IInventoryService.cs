using KidsToyHive.Core.Enums;
using System;

namespace KidsToyHive.Domain.Services
{
    public interface IInventoryService
    {
        bool IsItemAvailable(DateTime date, BookingTimeSlot timeSlot, Guid productId);
    }
}
