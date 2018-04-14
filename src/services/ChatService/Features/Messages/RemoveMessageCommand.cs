using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ChatService.Features.Messages
{
    public class RemoveMessageCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Message.MessageId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public Message Message { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Messages.Remove(await _context.Messages.FindAsync(request.Message.MessageId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
