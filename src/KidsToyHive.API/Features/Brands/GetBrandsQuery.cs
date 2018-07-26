using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.API.Features.Brands
{
    public class GetBrandsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<BrandDto> Brands { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;

            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(new Response()
                {
                    Brands = _eventStore.Query<Brand>().Select(x => BrandDto.FromBrand(x)).ToList()
                });
        }
    }
}
