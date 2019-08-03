using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.Services;
using KidsToyHive.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace KidsToyHive.Domain.Features.SalesOrders
{
    public class CheckoutSalesOrder
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {

            }
        }

        public class Request : Command<Response> {
            public Guid SalesOrderId { get; set; }
            public string Number { get; set; }
            public long? ExpMonth { get; set; }
            public int ExpYear { get; set; }
            public string Cvc { get; set; }
            public int Value { get; set; }
            public string Currency { get; set; }            
        }

        public class Response
        {
            public Guid SalesOrderId { get; set; }
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

                SalesOrder salesOrder = await _context.SalesOrders
                    .Include(x => x.SalesOrderDetails)
                    .SingleAsync(x => x.SalesOrderId == request.SalesOrderId);

                var result = await _paymentProcessor.ProcessAsync(new PaymentDto
                {
                    Number = request.Number,
                    ExpMonth = request.ExpMonth,
                    ExpYear = request.ExpYear,
                    Cvc = request.Cvc,
                    Currency = request.Currency,
                    //Add tax
                    Value = (int)(salesOrder.Cost * 100),
                    Description = $"SalesOrder: {salesOrder.SalesOrderId}"
                });

                if (result)
                    salesOrder.Status = SalesOrderStatus.Paid;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    SalesOrderId = salesOrder.SalesOrderId,
                    Version = salesOrder.Version
                };
            }
        }
    }
}
