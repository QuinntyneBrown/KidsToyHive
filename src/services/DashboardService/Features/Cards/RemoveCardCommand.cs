using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace DashboardService.Features.Cards
{
    public class RemoveCardCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Card.CardId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public Card Card { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Cards.Remove(await _context.Cards.FindAsync(request.Card.CardId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
