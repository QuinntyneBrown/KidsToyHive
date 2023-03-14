using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Cards;

public class GetCardsRequest : IRequest<GetCardsResponse> { }
public class GetCardsResponse
{
    public IEnumerable<CardDto> Cards { get; set; }
}
public class GetCardsHandler : IRequestHandler<GetCardsRequest, GetCardsResponse>
{
    private readonly IAppDbContext _context;
    public GetCardsHandler(IAppDbContext context) => _context = context;
    public async Task<GetCardsResponse> Handle(GetCardsRequest request, CancellationToken cancellationToken)
        => new GetCardsResponse()
        {
            Cards = await _context.Cards.Select(x => x.ToDto()).ToArrayAsync()
        };
}
