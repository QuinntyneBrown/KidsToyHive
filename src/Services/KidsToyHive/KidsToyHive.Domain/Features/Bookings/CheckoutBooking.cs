using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain;
using KidsToyHive.Domain.Models.DomainEvents;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings;

public class CheckoutBookingValidator : AbstractValidator<CheckoutBookingRequest>
{
    public CheckoutBookingValidator()
    {
    }
}
public class CheckoutBookingRequest : Command<CheckoutBookingResponse>
{
    public string Number { get; set; }
    public long ExpMonth { get; set; }
    public int ExpYear { get; set; }
    public string Cvc { get; set; }
    public int Value { get; set; }
    public string Currency { get; set; } = "CAD";
    public Guid BookingId { get; set; }
}
public class CheckoutBookingResponse
{
    public Guid BookingId { get; set; }
    public int Version { get; set; }
}
public class CheckoutBookingHandler : IRequestHandler<CheckoutBookingRequest, CheckoutBookingResponse>
{
    private readonly IAppDbContext _context;
    private readonly IEmailService _emailService;
    private readonly IPaymentProcessor _paymentProcessor;
    public CheckoutBookingHandler(IAppDbContext context, IEmailService emailService, IPaymentProcessor paymentProcessor)
    {
        _context = context;
        _emailService = emailService;
        _paymentProcessor = paymentProcessor;
    }
    public async Task<CheckoutBookingResponse> Handle(CheckoutBookingRequest request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .Include(x => x.Customer)
            .Include(x => x.BookingDetails)
            .SingleAsync(x => x.BookingId == request.BookingId);
        var tax = await _context.Taxes.FirstAsync(x => x.Effective <= booking.Created);
        var value = booking.Cost + (int)(booking.Cost * tax.Rate);
        var result = await _paymentProcessor.ProcessAsync(new PaymentDto
        {
            Number = request.Number,
            ExpMonth = request.ExpMonth,
            ExpYear = request.ExpYear,
            Cvc = request.Cvc,
            Currency = request.Currency,
            Value = value,
            Description = $"Booking: {booking.BookingId}"
        });
        if (result)
            booking.Status = BookingStatus.Paid;
        await _context.SaveChangesAsync(cancellationToken);
        await _emailService.SendBookingConfirmation(booking);
        booking.RaiseDomainEvent(new BookingCreated(booking));
        return new CheckoutBookingResponse()
        {
            BookingId = booking.BookingId,
            Version = booking.Version
        };
    }
}
