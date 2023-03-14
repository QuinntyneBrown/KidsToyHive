using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bins;

public class UpsertBin
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.Bin).NotNull();
            RuleFor(request => request.Bin).SetValidator(new BinDtoValidator());
        }
    }
    public class Request : IRequest<Response>
    {
        public BinDto Bin { get; set; }
    }
    public class Response
    {
        public Guid BinId { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        public IAppDbContext _context { get; set; }
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var bin = await _context.Bins.FindAsync(request.Bin.BinId);
            if (bin == null)
            {
                bin = new Bin();
                _context.Bins.Add(bin);
            }
            bin.Name = request.Bin.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return new Response() { BinId = bin.BinId };
        }
    }
}
