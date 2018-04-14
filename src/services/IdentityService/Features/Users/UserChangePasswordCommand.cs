using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Infrastructure.Services;

namespace IdentityService
{
    public class UserChangePasswordCommand
    {
        public class Request :  IRequest { 
            public int UserId { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
        
        public class Handler : IRequestHandler<Request>
        {
            private readonly IEncryptionService _encryptionService;
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context, IEncryptionService encryptionService)
            {
                _context = context;
                _encryptionService = encryptionService;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.UserId);
                user.Password = _encryptionService.TransformPassword(request.Password);                
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
