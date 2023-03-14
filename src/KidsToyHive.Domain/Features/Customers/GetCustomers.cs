using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Customers;

public class GetCustomersRequest : IRequest<GetCustomersResponse> { }
public class GetCustomersResponse
{
    public IEnumerable<CustomerDto> Customers { get; set; }
}
public class GetCustomersHandler : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
{
    private readonly IAppDbContext _context;
    public GetCustomersHandler(IAppDbContext context) => _context = context;
    public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        => new GetCustomersResponse()
        {
            Customers = await _context.Customers.Select(x => x.ToDto()).ToArrayAsync()
        };
}
