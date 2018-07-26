using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using KidsToyHive.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using KidsToyHive.Core.Models;

namespace KidsToyHive.API.Features.DigitalAssets
{
    public class GetDigitalAssetsByIdsQuery
    {
        public class Request : IRequest<Response> {
            public Guid[] DigitalAssetIds { get; set; }
        }

        public class Response
        {
            public IEnumerable<DigitalAssetDto> DigitalAssets { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IEventStore _eventStore { get; set; }
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    DigitalAssets = _eventStore.Query<DigitalAsset>()
                    .Where(x => request.DigitalAssetIds.Contains(x.DigitalAssetId))
                    .Select(x => DigitalAssetDto.FromDigitalAsset(x)).ToList()
                };
        }
    }
}
