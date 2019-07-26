using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using KidsToyHive.Domain.Services;
using KidsToyHive.Domain.Common;

namespace KidsToyHive.Domain.Features.Bookings
{
    public class ProcessBookingPayment
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {

            }
        }

        public class Request : Command<Response> {

        }

        public class Response
        {

        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            private readonly IPaymentProcessor _paymentProcessor;

            public Handler(IAppDbContext context, IPaymentProcessor paymentProcessor)
            {
                _context = context;
                _paymentProcessor = paymentProcessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { };
            }
        }
    }
}
