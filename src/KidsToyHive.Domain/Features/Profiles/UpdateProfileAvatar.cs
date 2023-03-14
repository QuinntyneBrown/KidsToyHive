using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Profiles;

 public class UpdateProfileAvatarRequest : IRequest<UpdateProfileAvatarResponse>
 {
     public int ProfileId { get; set; }
     public string AvatarUrl { get; set; }
 }
 public class UpdateProfileAvatarResponse
 {
     public Guid ProfileId { get; set; }
 }
 public class UpdateProfileAvatarHandler : IRequestHandler<UpdateProfileAvatarRequest, UpdateProfileAvatarResponse>
 {
     private readonly IAppDbContext _context;
     public UpdateProfileAvatarHandler(IAppDbContext context) => _context = context;
     public async Task<UpdateProfileAvatarResponse> Handle(UpdateProfileAvatarRequest request, CancellationToken cancellationToken)
     {
         var profile = _context.Profiles.Find(request.ProfileId);
         profile.AvatarUrl = request.AvatarUrl;
         await _context.SaveChangesAsync(cancellationToken);
         return new UpdateProfileAvatarResponse()
         {
             ProfileId = profile.ProfileId
         };
     }
 }
