using KidsToyHive.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrderDetails;

public class GetSalesOrderDetailByIdRequest : IRequest<GetSalesOrderDetailByIdResponse>
{
    public Guid SalesOrderDetailId { get; set; }
}
public class GetSalesOrderDetailByIdResponse
{
    public SalesOrderDetailDto SalesOrderDetail { get; set; }
}
public class GetSalesOrderDetailByIdHandler : IRequestHandler<GetSalesOrderDetailByIdRequest, GetSalesOrderDetailByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetSalesOrderDetailByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetSalesOrderDetailByIdResponse> Handle(GetSalesOrderDetailByIdRequest request, CancellationToken cancellationToken)
        => new GetSalesOrderDetailByIdResponse()
        {
            SalesOrderDetail = (await _context.SalesOrderDetails.FindAsync(request.SalesOrderDetailId)).ToDto()
        };
}
