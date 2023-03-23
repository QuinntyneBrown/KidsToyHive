using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Common;
using KidsToyHive.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Shipments;

public class GetCommittedShipmentsRequest : AuthenticatedRequest<GetCommittedShipmentsResponse>
{

}
public class GetCommittedShipmentsResponse
{
    public ICollection<ShipmentDto> Shipments { get; set; }
    = new HashSet<ShipmentDto>();
}
public class GetCommittedShipmentsHandler : IRequestHandler<GetCommittedShipmentsRequest, GetCommittedShipmentsResponse>
{
    private readonly IAppDbContext _context;
    public GetCommittedShipmentsHandler(IAppDbContext context) => _context = context;
    public async Task<GetCommittedShipmentsResponse> Handle(GetCommittedShipmentsRequest request, CancellationToken cancellationToken)
    {

        var driver = await _context.Drivers.SingleAsync(x => x.Email == request.CurrentUsername);
        return new GetCommittedShipmentsResponse()
        {
            Shipments = _context.Shipments
            .Where(x => x.DriverId == driver.DriverId
            && x.Status == ShipmentStatus.Committed)
            .Select(x => x.ToDto()).ToList()
        };
    }
}
