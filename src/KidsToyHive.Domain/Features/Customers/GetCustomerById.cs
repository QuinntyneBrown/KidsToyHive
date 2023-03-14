using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Customers;

public class GetCustomerByIdRequest : IRequest<GetCustomerByIdResponse>
{
    public Guid CustomerId { get; set; }
}
public class GetCustomerByIdResponse
{
    public CustomerDto Customer { get; set; }
}
public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetCustomerByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        => new GetCustomerByIdResponse()
        {
            Customer = (await _context.Customers
            .Include(x => x.Address)
            .Include(x => x.CustomerLocations)
            .ThenInclude(x => x.Location)
            .ThenInclude(x => x.Adddress)
            .SingleAsync(x => x.CustomerId == request.CustomerId)
            ).ToDto()
        };
}
