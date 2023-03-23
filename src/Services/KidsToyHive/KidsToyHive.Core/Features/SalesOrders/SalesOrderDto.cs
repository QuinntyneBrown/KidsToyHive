// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;
using KidsToyHive.Core.Enums;

namespace KidsToyHive.Core.Features.SalesOrders;

public class SalesOrderDtoValidator : AbstractValidator<SalesOrderDto>
{
    public SalesOrderDtoValidator()
    {
        RuleFor(x => x.SalesOrderId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class SalesOrderDto
{
    public Guid SalesOrderId { get; set; }
    public string Name { get; set; }
    public SalesOrderStatus Status { get; set; }
    public int Version { get; set; }
}
public static class SalesOrderExtensions
{
    public static SalesOrderDto ToDto(this SalesOrder salesOrder)
        => new SalesOrderDto
        {
            SalesOrderId = salesOrder.SalesOrderId,
            Version = salesOrder.Version,
            Status = salesOrder.Status
        };
}

