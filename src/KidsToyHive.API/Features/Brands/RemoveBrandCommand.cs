using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace KidsToyHive.API.Features.Brands
{
    public class RemoveBrandCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.BrandId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid BrandId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var brand = _eventStore.Query<Brand>(request.BrandId);

                brand.Remove();
                
                _eventStore.Save(brand);

                return Task.CompletedTask;
            }
        }
    }
}
