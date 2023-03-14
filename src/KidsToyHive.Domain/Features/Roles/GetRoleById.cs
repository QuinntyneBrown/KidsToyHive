using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Roles;

 public class GetRoleByIdRequest : IRequest<GetRoleByIdResponse>
 {
     public Guid RoleId { get; set; }
 }
 public class GetRoleByIdResponse
 {
     public RoleDto Role { get; set; }
 }
 public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdRequest, GetRoleByIdResponse>
 {
     private readonly IAppDbContext _context;
     public GetRoleByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetRoleByIdResponse> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
         => new GetRoleByIdResponse()
         {
             Role = (await _context.Roles.FindAsync(request.RoleId)).ToDto()
         };
 }
