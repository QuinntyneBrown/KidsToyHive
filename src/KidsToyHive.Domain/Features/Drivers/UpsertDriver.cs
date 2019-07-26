using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Drivers
{
    public class UpsertDriver
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Driver).NotNull();
                RuleFor(request => request.Driver).SetValidator(new DriverDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public DriverDto Driver { get; set; }
        }

        public class Response
        {
            public Guid DriverId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var driver = await _context.Drivers.FindAsync(request.Driver.DriverId);

                if (driver == null) {
                    driver = new Driver();
                    _context.Drivers.Add(driver);
                }

                driver.Name = request.Driver.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { DriverId = driver.DriverId };
            }
        }
    }
}