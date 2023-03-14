using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Cards;

public class RemoveCard
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.CardId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid CardId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var card = await _context.Cards.FindAsync(request.CardId);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
