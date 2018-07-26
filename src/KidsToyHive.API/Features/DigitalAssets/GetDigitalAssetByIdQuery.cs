using MediatR;
using System.Threading.Tasks;
using System.Threading;
using KidsToyHive.Core.Interfaces;
using FluentValidation;
using System;
using KidsToyHive.Core.Models;

namespace KidsToyHive.API.Features.DigitalAssets
{
    public class GetDigitalAssetByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DigitalAssetId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid DigitalAssetId { get; set; }
        }

        public class Response
        {
            public DigitalAssetDto DigitalAsset { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IEventStore _eventStore { get; set; }
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    DigitalAsset = DigitalAssetDto.FromDigitalAsset(_eventStore.Query<DigitalAsset>(request.DigitalAssetId))
                };
        }
    }
}
