using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Cards;

public class UpsertCard
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.Card).NotNull();
            RuleFor(request => request.Card).SetValidator(new CardDtoValidator());
        }
    }
    public class Request : IRequest<Response>
    {
        public CardDto Card { get; set; }
    }
    public class Response
    {
        public Guid CardId { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var card = await _context.Cards.FindAsync(request.Card.CardId);
            if (card == null)
            {
                card = new Card();
                _context.Cards.Add(card);
            }
            card.Name = request.Card.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return new Response() { CardId = card.CardId };
        }
    }
}
