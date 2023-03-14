using MediatR;
using System.Threading.Tasks;
using System.Threading;
using KidsToyHive.Core.Interfaces;
using FluentValidation;
using KidsToyHive.Domain.DataAccess;

namespace KidsToyHive.Domain.Features.Profiles;

 public class Validator : AbstractValidator<Request>
 {
     public Validator()
     {
         RuleFor(request => request.ProfileId).NotEqual(0);
     }
 }
 public class GetProfileByIdRequest : IRequest<GetProfileByIdResponse>
 {
     public int ProfileId { get; set; }
 }
 public class GetProfileByIdResponse
 {
     public ProfileDto Profile { get; set; }
 }
 public class GetProfileByIdHandler : IRequestHandler<GetProfileByIdRequest, GetProfileByIdResponse>
 {
     private readonly IAppDbContext _context;

     public GetProfileByIdHandler(IAppDbContext context) => _context = context;
     public async Task<GetProfileByIdResponse> Handle(GetProfileByIdRequest request, CancellationToken cancellationToken)
         => new GetProfileByIdResponse()
         {
             Profile = ProfileDto.FromProfile(await _context.Profiles.FindAsync(request.ProfileId))
         };
 }
