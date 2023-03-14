using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Cards;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(request => request.Card).NotNull();
        RuleFor(request => request.Card).SetValidator(new CardDtoValidator());
    }
}
public class UpsertCardRequest : IRequest<UpsertCardResponse>
{
    public CardDto Card { get; set; }
}
public class UpsertCardResponse
{
    public Guid CardId { get; set; }
}
public class UpsertCardHandler : IRequestHandler<UpsertCardRequest, UpsertCardResponse>
{
    private readonly IAppDbContext _context;
    public UpsertCardHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertCardResponse> Handle(UpsertCardRequest request, CancellationToken cancellationToken)
    {
        var card = await _context.Cards.FindAsync(request.Card.CardId);
        if (card == null)
        {
            card = new Card();
            _context.Cards.Add(card);
        }
        card.Name = request.Card.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertCardResponse() { CardId = card.CardId };
    }
}
