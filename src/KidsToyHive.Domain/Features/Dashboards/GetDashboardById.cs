using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Dashboards;

 public class GetDashboardByIdRequest : IRequest<GetDashboardByIdResponse>
 {
     public Guid DashboardId { get; set; }
 }
 public class GetDashboardByIdResponse
 {
     public DashboardDto Dashboard { get; set; }
 }
 public class GetDashboardByIdHandler : IRequestHandler<GetDashboardByIdRequest, GetDashboardByIdResponse>
 {
     private readonly IAppDbContext _context;
     public GetDashboardByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetDashboardByIdResponse> Handle(GetDashboardByIdRequest request, CancellationToken cancellationToken)
         => new GetDashboardByIdResponse()
         {
             Dashboard = (await _context.Dashboards.FindAsync(request.DashboardId)).ToDto()
         };
 }
