using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;
using System;

namespace DigitalAssetService.Features.DigitalAssets
{
    public class RemoveDigitalAssetCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DigitalAsset.DigitalAssetId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public DigitalAsset DigitalAsset { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.DigitalAssets.Remove(await _context.DigitalAssets.FindAsync(request.DigitalAsset.DigitalAssetId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
