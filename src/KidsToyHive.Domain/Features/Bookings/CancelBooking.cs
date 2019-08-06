using FluentValidation;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bookings
{
    public class CancelBooking
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response> {

        }

        public class Response
        {

        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { };
            }
        }
    }
}
