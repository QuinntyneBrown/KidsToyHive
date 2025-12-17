// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Cards;

public class GetCardsRequest : IRequest<GetCardsResponse> { }
public class GetCardsResponse
{
    public IEnumerable<CardDto> Cards { get; set; }
}
public class GetCardsHandler : IRequestHandler<GetCardsRequest, GetCardsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetCardsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetCardsResponse> Handle(GetCardsRequest request, CancellationToken cancellationToken)
        => new GetCardsResponse()
        {
            Cards = await _context.Cards.Select(x => x.ToDto()).ToArrayAsync()
        };
}

