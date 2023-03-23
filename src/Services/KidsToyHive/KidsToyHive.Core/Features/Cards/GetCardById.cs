// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Cards;

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
    private readonly IKidsToyHiveDbContext _context;
    public GetCardByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetCardByIdResponse> Handle(GetCardByIdRequest request, CancellationToken cancellationToken)
        => new GetCardByIdResponse()
        {
            Card = (await _context.Cards.FindAsync(request.CardId)).ToDto()
        };
}

