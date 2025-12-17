// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Contacts;

public class GetContactByIdRequest : IRequest<GetContactByIdResponse>
{
    public Guid ContactId { get; set; }
}
public class GetContactByIdResponse
{
    public ContactDto Contact { get; set; }
}
public class GetContactByIdHandler : IRequestHandler<GetContactByIdRequest, GetContactByIdResponse>
{
    public IKidsToyHiveDbContext _context { get; set; }
    public GetContactByIdHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<GetContactByIdResponse> Handle(GetContactByIdRequest request, CancellationToken cancellationToken)
        => new GetContactByIdResponse()
        {
            Contact = (await _context.Contacts.FindAsync(request.ContactId)).ToDto()
        };
}

