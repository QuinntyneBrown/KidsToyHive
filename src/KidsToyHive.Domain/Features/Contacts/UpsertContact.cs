using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KidsToyHive.Domain.Common;

namespace KidsToyHive.Domain.Features.Contacts;

public class UpsertContact
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.Contact).NotNull();
            RuleFor(request => request.Contact).SetValidator(new ContactDtoValidator());
        }
    }
    [AllowAnonymous]
    public class Request : Command<Response>
    {
        public ContactDto Contact { get; set; }
    }
    public class Response
    {
        public Guid ContactId { get; set; }
        public int Version { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        public IAppDbContext _context { get; set; }
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts.FindAsync(request.Contact.ContactId);
            if (contact == null)
            {
                contact = new Contact();
                _context.Contacts.Add(contact);
            }
            contact.FullName = request.Contact.FullName;
            contact.PhoneNumber = request.Contact.PhoneNumber;
            contact.Email = request.Contact.Email;
            await _context.SaveChangesAsync(cancellationToken);
            return new Response()
            {
                ContactId = contact.ContactId,
                Version = contact.Version
            };
        }
    }
}
