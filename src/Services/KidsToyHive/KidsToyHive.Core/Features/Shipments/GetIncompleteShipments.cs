// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KidsToyHive.Core.Enums;

namespace KidsToyHive.Core.Features.Shipments;

public class GetIncompleteShipmentsValidator : AbstractValidator<GetIncompleteShipmentsRequest>
{
    public GetIncompleteShipmentsValidator()
    {
    }
}
public class GetIncompleteShipmentsRequest : IRequest<GetIncompleteShipmentsResponse>
{
}
public class GetIncompleteShipmentsResponse
{
    public ICollection<ShipmentDto> Shipments { get; set; }
    = new HashSet<ShipmentDto>();
}
public class GetIncompleteShipmentsHandler : IRequestHandler<GetIncompleteShipmentsRequest, GetIncompleteShipmentsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetIncompleteShipmentsHandler(IKidsToyHiveDbContext context) => _context = context;
    public Task<GetIncompleteShipmentsResponse> Handle(GetIncompleteShipmentsRequest request, CancellationToken cancellationToken)
        => Task.FromResult(new GetIncompleteShipmentsResponse()
        {
            Shipments = _context.Shipments
            .Where(x => x.Status == ShipmentStatus.New)
            .Select(x => x.ToDto())
            .ToList()
        });
}

