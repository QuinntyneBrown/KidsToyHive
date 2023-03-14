using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Contacts;

public class GetContactsRequest : IRequest<GetContactsResponse> { }
public class GetContactsResponse
{
    public IEnumerable<ContactDto> Contacts { get; set; }
}
public class GetContactsHandler : IRequestHandler<GetContactsRequest, GetContactsResponse>
{
    private readonly IAppDbContext _context;
    public GetContactsHandler(IAppDbContext context) => _context = context;
    public async Task<GetContactsResponse> Handle(GetContactsRequest request, CancellationToken cancellationToken)
        => new GetContactsResponse()
        {
            Contacts = await _context.Contacts.Select(x => x.ToDto()).ToArrayAsync()
        };
}
