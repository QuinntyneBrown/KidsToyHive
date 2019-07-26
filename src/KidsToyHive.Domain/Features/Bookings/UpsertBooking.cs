using FluentValidation;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Models.DomainEvents;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings
{
    public class UpsertBooking
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Booking).NotNull();
                RuleFor(request => request.Booking).SetValidator(new BookingDtoValidator());
            }
        }

        public class Request : Command<Response>
        {
            public BookingDto Booking { get; set; }

            public override string Key => Build("Booking", $"{Booking.BookingId}", $"{Booking.Version}");

            public override IEnumerable<string> SideEffects
            {
                get
                {
                    var sideEffects = new List<string>();

                    sideEffects.AddRange(Booking.BookingDetails
                        .Select(x => $"Product {x.ProductId}"));

                    return sideEffects;
                }
            }
        }

        public class Response
        {
            public Guid BookingId { get;set; }
            public int Version { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;            
            private readonly IInventoryService _inventoryService;
            public Handler(IAppDbContext context, IInventoryService inventoryService)
            {
                _context = context;
                _inventoryService = inventoryService;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var booking = await _context.Bookings.FindAsync(request.Booking.BookingId);

                if (booking == null) {
                    booking = new Booking();
                    booking.RaiseDomainEvent(new BookingCreated(booking));
                    _context.Bookings.Add(booking);
                }

                booking.BookingDetails.Clear();

                foreach(var bookingDetail in request.Booking.BookingDetails)
                {
                    if (!_inventoryService.IsItemAvailable(request.Booking.Date, request.Booking.BookingTimeSlot, bookingDetail.ProductId))
                        throw new Exception();
                }
                    
                foreach(var bookingDetail in request.Booking.BookingDetails)
                {
                    booking.BookingDetails.Add(new BookingDetail
                    {
                        ProductId = bookingDetail.ProductId,
                        Quantity = bookingDetail.Quantity,
                        Cost = (4 * bookingDetail.Product.HourlyRate) * bookingDetail.Quantity
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() {
                    BookingId = booking.BookingId,
                    Version = booking.Version
                };
            }
        }
    }
}
