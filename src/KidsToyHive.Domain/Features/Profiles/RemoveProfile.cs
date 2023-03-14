using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using KidsToyHive.Domain.Models;
using KidsToyHive.Core.Interfaces;
using KidsToyHive.Domain.DataAccess;

namespace KidsToyHive.Domain.Features.Profiles;

 public class Validator : AbstractValidator<Request>
 {
     public Validator()
     {
         RuleFor(request => request.ProfileId).NotEqual(0);
     }
 }
 public class RemoveProfileRequest : IRequest<RemoveProfileResponse>
 {
     public int ProfileId { get; set; }
 }
 public class RemoveProfileResponse { }
 public class RemoveProfileHandler : IRequestHandler<RemoveProfileRequest, RemoveProfileResponse>
 {
     private readonly IAppDbContext _context;

     public RemoveProfileHandler(IAppDbContext context) => _context = context;
     public async Task<RemoveProfileResponse> Handle(RemoveProfileRequest request, CancellationToken cancellationToken)
     {
         _context.Profiles.Remove(await _context.Profiles.FindAsync(request.ProfileId));
         await _context.SaveChangesAsync(cancellationToken);
         return new RemoveProfileResponse()
         {
         };
     }
 }
