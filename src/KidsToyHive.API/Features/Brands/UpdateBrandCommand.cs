using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace KidsToyHive.API.Features.Brands
{
    public class UpdateBrandCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Brand.BrandId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public BrandDto Brand { get; set; }
        }

        public class Response
        {			
            public Guid BrandId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var brand = _eventStore.Query<Brand>(request.Brand.BrandId);

                brand.ChangeName(request.Brand.Name);

                _eventStore.Save(brand);

                return Task.FromResult(new Response() { BrandId = request.Brand.BrandId }); 
            }
        }
    }
}
