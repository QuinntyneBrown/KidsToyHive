using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ChatService.Features.Messages
{
    public class GetMessageByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.MessageId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int MessageId { get; set; }
        }

        public class Response
        {
            public MessageApiModel Message { get; set; }
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
                    Message = MessageApiModel.FromMessage(await _context.Messages.FindAsync(request.MessageId))
                };
        }
    }
}
