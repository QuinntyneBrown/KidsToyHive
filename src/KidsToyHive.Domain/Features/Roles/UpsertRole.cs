using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Roles;

 public class Validator : AbstractValidator<Request>
 {
     public Validator()
     {
         RuleFor(request => request.Role).NotNull();
         RuleFor(request => request.Role).SetValidator(new RoleDtoValidator());
     }
 }
 public class UpsertRoleRequest : IRequest<UpsertRoleResponse>
 {
     public RoleDto Role { get; set; }
 }
 public class UpsertRoleResponse
 {
     public Guid RoleId { get; set; }
 }
 public class UpsertRoleHandler : IRequestHandler<UpsertRoleRequest, UpsertRoleResponse>
 {
     private readonly IAppDbContext _context;
     public UpsertRoleHandler(IAppDbContext context) => _context = context;
     public async Task<UpsertRoleResponse> Handle(UpsertRoleRequest request, CancellationToken cancellationToken)
     {
         var role = await _context.Roles.FindAsync(request.Role.RoleId);
         if (role == null)
         {
             role = new Role();
             _context.Roles.Add(role);
         }
         role.Name = request.Role.Name;
         await _context.SaveChangesAsync(cancellationToken);
         return new UpsertRoleResponse() { RoleId = role.RoleId };
     }
 }
