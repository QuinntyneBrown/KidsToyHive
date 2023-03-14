using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Roles;

 public class GetRolesRequest : IRequest<GetRolesResponse> { }
 public class GetRolesResponse
 {
     public IEnumerable<RoleDto> Roles { get; set; }
 }
 public class GetRolesHandler : IRequestHandler<GetRolesRequest, GetRolesResponse>
 {
     private readonly IAppDbContext _context;
     public GetRolesHandler(IAppDbContext context) => _context = context;
     public async Task<GetRolesResponse> Handle(GetRolesRequest request, CancellationToken cancellationToken)
         => new GetRolesResponse()
         {
             Roles = await _context.Roles.Select(x => x.ToDto()).ToArrayAsync()
         };
 }
