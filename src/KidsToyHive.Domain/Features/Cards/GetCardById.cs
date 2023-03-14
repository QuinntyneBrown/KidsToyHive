using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Cards;

public class GetCardByIdRequest : IRequest<GetCardByIdResponse>
{
    public Guid CardId { get; set; }
}
public class GetCardByIdResponse
{
    public CardDto Card { get; set; }
}
public class GetCardByIdHandler : IRequestHandler<GetCardByIdRequest, GetCardByIdResponse>
{
    private readonly IAppDbContext _context;
    public GetCardByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetCardByIdResponse> Handle(GetCardByIdRequest request, CancellationToken cancellationToken)
        => new GetCardByIdResponse()
        {
            Card = (await _context.Cards.FindAsync(request.CardId)).ToDto()
        };
}
