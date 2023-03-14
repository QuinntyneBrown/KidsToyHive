using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Videos;

public class RemoveVideoValidator : AbstractValidator<RemoveVideoRequest>
{
    public RemoveVideoValidator()
    {
        RuleFor(request => request.VideoId).NotNull();
    }
}
public class RemoveVideoRequest : IRequest
{
    public Guid VideoId { get; set; }
}
public class RemoveVideoHandler : IRequestHandler<RemoveVideoRequest>
{
    private readonly IAppDbContext _context;
    public RemoveVideoHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveVideoRequest request, CancellationToken cancellationToken)
    {
        var video = await _context.Videos.FindAsync(request.VideoId);
        _context.Videos.Remove(video);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
