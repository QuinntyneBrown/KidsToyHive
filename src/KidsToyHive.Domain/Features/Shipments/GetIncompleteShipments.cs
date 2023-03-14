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

public class GetIncompleteShipments
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
        }
    }
    public class Request : IRequest<Response>
    {
    }
    public class Response
    {
        public ICollection<ShipmentDto> Shipments { get; set; }
        = new HashSet<ShipmentDto>();
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => Task.FromResult(new Response()
            {
                Shipments = _context.Shipments
                .Where(x => x.Status == ShipmentStatus.New)
                .Select(x => x.ToDto())
                .ToList()
            });
    }
}
