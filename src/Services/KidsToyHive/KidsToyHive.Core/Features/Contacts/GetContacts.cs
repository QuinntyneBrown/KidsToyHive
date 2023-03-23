// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Contacts;

public class GetContactsRequest : IRequest<GetContactsResponse> { }
public class GetContactsResponse
{
    public IEnumerable<ContactDto> Contacts { get; set; }
}

public class GetContactsHandler : IRequestHandler<GetContactsRequest, GetContactsResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public GetContactsHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetContactsResponse> Handle(GetContactsRequest request, CancellationToken cancellationToken)
        => new GetContactsResponse()
        {
            Contacts = await _context.Contacts.Select(x => x.ToDto()).ToArrayAsync()
        };
}

