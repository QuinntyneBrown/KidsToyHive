using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KidsToyHive.Core.Enums;

namespace KidsToyHive.Domain.Features.Shipments;

 public class Validator : AbstractValidator<Request>
 {
     public Validator()
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
     private readonly IAppDbContext _context;
     public GetIncompleteShipmentsHandler(IAppDbContext context) => _context = context;
     public Task<GetIncompleteShipmentsResponse> Handle(GetIncompleteShipmentsRequest request, CancellationToken cancellationToken)
         => Task.FromResult(new GetIncompleteShipmentsResponse()
         {
             Shipments = _context.Shipments
             .Where(x => x.Status == ShipmentStatus.New)
             .Select(x => x.ToDto())
             .ToList()
         });
 }
