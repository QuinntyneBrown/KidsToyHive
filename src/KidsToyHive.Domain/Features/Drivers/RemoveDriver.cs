using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Drivers
{
    public class RemoveDriver
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DriverId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid DriverId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var driver = await _context.Drivers.FindAsync(request.DriverId);

                _context.Drivers.Remove(driver);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
