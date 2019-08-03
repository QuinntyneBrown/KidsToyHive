using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using KidsToyHive.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings
{
    public class CheckoutBooking
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {

            }
        }

        public class Request : Command<Response> {
            public string Number { get; set; }
            public long? ExpMonth { get; set; }
            public int ExpYear { get; set; }
            public string Cvc { get; set; }
            public int Value { get; set; }
            public string Currency { get; set; } = "CAD";
            public Guid BookingId { get; set; }
        }

        public class Response
        {
            public Guid BookingId { get; set; }
            public int Version { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IPaymentProcessor _paymentProcessor;

            public Handler(IAppDbContext context, IPaymentProcessor paymentProcessor)
            {
                _context = context;
                _paymentProcessor = paymentProcessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var booking = await _context.Bookings.FindAsync(request.BookingId);

                var tax = await _context.Taxes.FirstAsync(x => x.Effective <= booking.Created);

                var result = await _paymentProcessor.ProcessAsync(new PaymentDto {
                    Number = request.Number,
                    ExpMonth = request.ExpMonth,
                    ExpYear = request.ExpYear,
                    Cvc = request.Cvc,
                    Currency = request.Currency,
                    Value = booking.Cost + (booking.Cost * tax.Rate),
                    Description = $"Booking: {booking.BookingId}"
                });

                if(result)
                    booking.Status = BookingStatus.Paid;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() {
                    BookingId = booking.BookingId,
                    Version = booking.Version
                };
            }
        }
    }
}
