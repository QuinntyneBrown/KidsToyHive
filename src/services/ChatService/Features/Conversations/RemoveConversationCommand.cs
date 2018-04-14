using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ChatService.Features.Conversations
{
    public class RemoveConversationCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Conversation.ConversationId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public Conversation Conversation { get; set; }
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
                _context.Conversations.Remove(await _context.Conversations.FindAsync(request.Conversation.ConversationId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
