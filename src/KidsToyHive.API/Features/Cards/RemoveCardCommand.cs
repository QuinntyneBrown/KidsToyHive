using KidsToyHive.Core.Interfaces;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace KidsToyHive.API.Features.Cards
{
    public class RemoveCardCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CardId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid CardId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var card = _eventStore.Query<Card>(request.CardId);

                card.Remove();
                
                _eventStore.Save(card);

                return Task.CompletedTask;
            }
        }
    }
}
