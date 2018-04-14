using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Features.Conversations
{
    public class GetConversationsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ConversationApiModel> Conversations { get; set; }
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
                    Conversations = await _context.Conversations.Select(x => ConversationApiModel.FromConversation(x)).ToListAsync()
                };
        }
    }
}
