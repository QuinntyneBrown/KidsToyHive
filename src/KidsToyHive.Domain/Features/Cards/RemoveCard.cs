using KidsToyHive.Domain.DataAccess;
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
        RuleFor(request => request.CardId).NotNull();
    }
}
public class RemoveCardRequest : IRequest
{
    public Guid CardId { get; set; }
}
public class RemoveCardHandler : IRequestHandler<Request>
{
    private readonly IAppDbContext _context;
    public RemoveCardHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveCardRequest request, CancellationToken cancellationToken)
    {
        var card = await _context.Cards.FindAsync(request.CardId);
        _context.Cards.Remove(card);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
