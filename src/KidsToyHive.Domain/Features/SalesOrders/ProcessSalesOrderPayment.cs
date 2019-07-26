using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.Services;

namespace KidsToyHive.Domain.Features.SalesOrders
{
    public class ProcessSalesOrderPayment
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {

            }
        }

        public class Request : Command<Response> {
            public Guid SalesOrderId { get; set; }
        }

        public class Response
        {

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

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { };
            }
        }
    }
}
