using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments
{
    public class GetCommittedShipments
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response> {
            public Guid DriverId { get; set; }
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

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) 
                => new Response()
                {
                    Shipments = _context.Shipments
                    .Where(x => x.DriverId == request.DriverId
                    && x.Status == ShipmentStatus.Committed)
                    .Select(x => x.ToDto()).ToList()
                };
        }
    }
}
