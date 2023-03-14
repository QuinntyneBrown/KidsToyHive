using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Taxes;

public class RemoveTax
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.TaxId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid TaxId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var tax = await _context.Taxes.FindAsync(request.TaxId);
            _context.Taxes.Remove(tax);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
