using KidsToyHive.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Contacts;

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
    public IAppDbContext _context { get; set; }
    public GetContactByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetContactByIdResponse> Handle(GetContactByIdRequest request, CancellationToken cancellationToken)
        => new GetContactByIdResponse()
        {
            Contact = (await _context.Contacts.FindAsync(request.ContactId)).ToDto()
        };
}
