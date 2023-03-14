using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bins;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(request => request.BinId).NotNull();
    }
}
public class RemoveBinRequest : IRequest
{
    public Guid BinId { get; set; }
}
public class RemoveBinHandler : IRequestHandler<Request>
{
    private readonly IAppDbContext _context;
    public RemoveBinHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveBinRequest request, CancellationToken cancellationToken)
    {
        var bin = await _context.Bins.FindAsync(request.BinId);
        _context.Bins.Remove(bin);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
