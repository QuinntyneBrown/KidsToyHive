using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Bins;

public class UpsertBinValidator : AbstractValidator<UpsertBinRequest>
{
    public UpsertBinValidator()
    {
        RuleFor(request => request.Bin).NotNull();
        RuleFor(request => request.Bin).SetValidator(new BinDtoValidator());
    }
}
public class UpsertBinRequest : IRequest<UpsertBinResponse>
{
    public BinDto Bin { get; set; }
}
public class UpsertBinResponse
{
    public Guid BinId { get; set; }
}
public class UpsertBinHandler : IRequestHandler<UpsertBinRequest, UpsertBinResponse>
{
    public IAppDbContext _context { get; set; }
    public UpsertBinHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertBinResponse> Handle(UpsertBinRequest request, CancellationToken cancellationToken)
    {
        var bin = await _context.Bins.FindAsync(request.Bin.BinId);
        if (bin == null)
        {
            bin = new Bin();
            _context.Bins.Add(bin);
        }
        bin.Name = request.Bin.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertBinResponse() { BinId = bin.BinId };
    }
}
