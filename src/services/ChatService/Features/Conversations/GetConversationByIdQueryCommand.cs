using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ChatService.Features.Conversations
{
    public class GetConversationByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ConversationId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ConversationId { get; set; }
        }

        public class Response
        {
            public ConversationApiModel Conversation { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Conversation = ConversationApiModel.FromConversation(await _context.Conversations.FindAsync(request.ConversationId))
                };
        }
    }
}
