using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Drivers;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(request => request.DriverId).NotNull();
    }
}
public class RemoveDriverRequest : IRequest
{
    public Guid DriverId { get; set; }
}
public class RemoveDriverHandler : IRequestHandler<Request>
{
    private readonly IAppDbContext _context;
    public RemoveDriverHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveDriverRequest request, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers.FindAsync(request.DriverId);
        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
