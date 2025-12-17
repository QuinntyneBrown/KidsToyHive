// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using KidsToyHive.Core.Common;
using KidsToyHive.Core.Services;
using KidsToyHive.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace KidsToyHive.Core.Features.SalesOrders;

public class CheckoutSalesOrderValidator : AbstractValidator<CheckoutSalesOrderRequest>
{
    public CheckoutSalesOrderValidator()
    {
    }
}
public class CheckoutSalesOrderRequest : Command<CheckoutSalesOrderResponse>
{
    public Guid SalesOrderId { get; set; }
    public string Number { get; set; }
    public long? ExpMonth { get; set; }
    public int ExpYear { get; set; }
    public string Cvc { get; set; }
    public int Value { get; set; }
    public string Currency { get; set; }
}
public class CheckoutSalesOrderResponse
{
    public Guid SalesOrderId { get; set; }
    public int Version { get; set; }
}
public class CheckoutSalesOrderHandler : IRequestHandler<CheckoutSalesOrderRequest, CheckoutSalesOrderResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    private readonly IPaymentProcessor _paymentProcessor;
    public CheckoutSalesOrderHandler(IKidsToyHiveDbContext context, IPaymentProcessor paymentProcessor)
    {
        _context = context;
        _paymentProcessor = paymentProcessor;
    }
    public async Task<CheckoutSalesOrderResponse> Handle(CheckoutSalesOrderRequest request, CancellationToken cancellationToken)
    {
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
        return new CheckoutSalesOrderResponse()
        {
            SalesOrderId = salesOrder.SalesOrderId,
            Version = salesOrder.Version
        };
    }
}

