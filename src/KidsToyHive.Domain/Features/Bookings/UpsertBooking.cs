using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Core.Exceptions;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Models.DomainEvents;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings;

public class UpsertBookingValidator : AbstractValidator<UpsertBookingRequest>
{
    public UpsertBookingValidator()
    {
    }
}
public class UpsertBookingRequest : Command<UpsertBookingResponse>
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
public class UpsertBookingResponse
{
    public Guid BookingId { get; set; }
    public int Version { get; set; }
}
public class UpsertBookingHandler : IRequestHandler<UpsertBookingRequest, UpsertBookingResponse>
{
    private readonly IAppDbContext _context;
    private readonly IInventoryService _inventoryService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UpsertBookingHandler(IAppDbContext context, IInventoryService inventoryService, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _inventoryService = inventoryService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<UpsertBookingResponse> Handle(UpsertBookingRequest request, CancellationToken cancellationToken)
    {
        Booking booking = await _context.Bookings.FindAsync(request.Booking.BookingId);
        if (booking == null)
        {
            booking = new Booking();
            booking.RaiseDomainEvent(new BookingCreated(booking));
            _context.Bookings.Add(booking);
        }
        booking.CustomerId = new Guid(this._httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "CustomerId").Value);
        booking.Date = request.Booking.Date;
        booking.BookingTimeSlot = request.Booking.BookingTimeSlot;
        booking.Location = await _context.Locations.FindAsync(request.Booking.LocationId);
        if (booking.Location == null)
            booking.Location = new Location();
        booking.Location.Adddress = new Address(
            request.Booking.Location.Address.Street,
            request.Booking.Location.Address.City,
            request.Booking.Location.Address.Province,
            request.Booking.Location.Address.PostalCode);
        booking.BookingDetails.Clear();
        foreach (var bookingDetail in request.Booking.BookingDetails)
        {
            if (!await _inventoryService.IsItemAvailable(request.Booking.Date, request.Booking.BookingTimeSlot, bookingDetail.ProductId))
                throw new OutOfStockException();
        }

        foreach (var bookingDetail in request.Booking.BookingDetails)
        {
            var product = await _context.Products.FindAsync(bookingDetail.ProductId);
            var units = 0;
            var lowUnitsPremium = 0;
            switch (booking.BookingTimeSlot)
            {
                case BookingTimeSlot.FullDay:
                    units = 8;
                    break;
                case BookingTimeSlot.Morning:
                case BookingTimeSlot.Afternoon:
                    units = 4;
                    lowUnitsPremium = 1000;
                    break;
                default:
                    throw new NotImplementedException();
            }
            booking.BookingDetails.Add(new BookingDetail
            {
                ProductId = bookingDetail.ProductId,
                Quantity = bookingDetail.Quantity,
                Cost = ((units * product.ChargePeriodPrice) * bookingDetail.Quantity) + lowUnitsPremium
            });
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertBookingResponse()
        {
            BookingId = booking.BookingId,
            Version = booking.Version
        };
    }
}
