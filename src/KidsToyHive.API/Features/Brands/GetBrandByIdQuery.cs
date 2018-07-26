using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.API.Features.Brands
{
    public class GetBrandByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.BrandId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid BrandId { get; set; }
        }

        public class Response
        {
            public BrandDto Brand { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    Brand = BrandDto.FromBrand(_eventStore.Query<Brand>(request.BrandId))
                });
        }
    }
}
