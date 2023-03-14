using FluentValidation;
using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Profiles;

 public class Validator : AbstractValidator<Request>
 {
     public Validator()
     {
         RuleFor(request => request.Profile.ProfileId).NotNull();
     }
 }
 public class UpsertProfileRequest : IRequest<UpsertProfileResponse>
 {
     public ProfileDto Profile { get; set; }
 }
 public class UpsertProfileResponse
 {
     public Guid ProfileId { get; set; }
 }
 public class UpsertProfileHandler : IRequestHandler<UpsertProfileRequest, UpsertProfileResponse>
 {
     private readonly IAppDbContext _context;

     public UpsertProfileHandler(IAppDbContext context) => _context = context;
     public async Task<UpsertProfileResponse> Handle(UpsertProfileRequest request, CancellationToken cancellationToken)
     {
         var profile = await _context.Profiles.FindAsync(request.Profile.ProfileId);
         if (profile == null) _context.Profiles.Add(profile = new Profile());
         profile.Name = request.Profile.Name;
         await _context.SaveChangesAsync(cancellationToken);
         return new UpsertProfileResponse() { ProfileId = profile.ProfileId };
     }
 }
