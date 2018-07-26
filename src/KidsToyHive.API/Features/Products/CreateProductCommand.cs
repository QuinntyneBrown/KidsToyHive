using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace KidsToyHive.API.Features.Products
{
    public class CreateProductCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Product.ProductId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ProductDto Product { get; set; }
        }

        public class Response
        {            
            public Guid ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var product = new Product(
                    request.Product.BrandId,
                    request.Product.Name,
                    request.Product.Description,
                    request.Product.ImageUrl);

                _eventStore.Save(product);
                
                return Task.FromResult(new Response() { ProductId = product.ProductId });
            }
        }
    }
}
