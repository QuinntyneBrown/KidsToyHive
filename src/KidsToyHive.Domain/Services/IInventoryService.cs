using KidsToyHive.Core.Enums;
using System;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services
{
    public interface IInventoryService
    {
        Task<bool> IsItemAvailable(DateTime date, BookingTimeSlot timeSlot, Guid productId);
    }
}
