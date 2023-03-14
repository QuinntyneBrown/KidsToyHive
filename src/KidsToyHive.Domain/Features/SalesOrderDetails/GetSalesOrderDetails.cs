using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrderDetails;

public class GetSalesOrderDetailsRequest : IRequest<GetSalesOrderDetailsResponse> { }
public class GetSalesOrderDetailsResponse
{
    public IEnumerable<SalesOrderDetailDto> SalesOrderDetails { get; set; }
}
public class GetSalesOrderDetailsHandler : IRequestHandler<GetSalesOrderDetailsRequest, GetSalesOrderDetailsResponse>
{
    private readonly IAppDbContext _context;
    public GetSalesOrderDetailsHandler(IAppDbContext context) => _context = context;
    public async Task<GetSalesOrderDetailsResponse> Handle(GetSalesOrderDetailsRequest request, CancellationToken cancellationToken)
        => new GetSalesOrderDetailsResponse()
        {
            SalesOrderDetails = await _context.SalesOrderDetails.Select(x => x.ToDto()).ToArrayAsync()
        };
}
