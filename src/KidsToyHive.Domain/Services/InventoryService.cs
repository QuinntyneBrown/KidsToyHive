using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using System;

namespace KidsToyHive.Domain.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IAppDbContext _context;
        public InventoryService(IAppDbContext context)
        {
            _context = context;
        }
        public bool IsItemAvailable(DateTime date, BookingTimeSlot timeSlot, Guid productId)
        {
            return true;
        }
    }
}
