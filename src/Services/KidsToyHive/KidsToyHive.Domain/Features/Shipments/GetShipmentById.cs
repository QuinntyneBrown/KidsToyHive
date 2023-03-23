using KidsToyHive.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments;

public class GetShipmentByIdRequest : IRequest<GetShipmentByIdResponse>
{
    public Guid ShipmentId { get; set; }
}
public class GetShipmentByIdResponse
{
    public ShipmentDto Shipment { get; set; }
}
public class GetShipmentByIdHandler : IRequestHandler<GetShipmentByIdRequest, GetShipmentByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetShipmentByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetShipmentByIdResponse> Handle(GetShipmentByIdRequest request, CancellationToken cancellationToken)
        => new GetShipmentByIdResponse()
        {
            Shipment = (await _context.Shipments.FindAsync(request.ShipmentId)).ToDto()
        };
}
