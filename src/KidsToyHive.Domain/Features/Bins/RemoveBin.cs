using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bins;

public class RemoveBin
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.BinId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid BinId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var bin = await _context.Bins.FindAsync(request.BinId);
            _context.Bins.Remove(bin);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
