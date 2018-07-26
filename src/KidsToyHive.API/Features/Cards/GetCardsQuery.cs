using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.API.Features.Cards
{
    public class GetCardsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<CardDto> Cards { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;

            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(new Response()
                {
                    Cards = _eventStore.Query<Card>().Select(x => CardDto.FromCard(x)).ToList()
                });
        }
    }
}
