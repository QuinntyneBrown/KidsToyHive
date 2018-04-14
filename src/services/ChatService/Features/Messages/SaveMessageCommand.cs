using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace ChatService.Features.Messages
{
    public class SaveMessageCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Message.FromId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public MessageInputApiModel Message { get; set; }

        }

        public class Response
        {			
            public int MessageId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var message = new Message();

                _context.Messages.Add(message);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { MessageId = message.MessageId };
            }
        }
    }
}
