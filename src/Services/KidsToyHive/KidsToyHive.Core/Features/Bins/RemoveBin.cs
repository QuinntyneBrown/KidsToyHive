// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Bins;

public class RemoveBinValidator : AbstractValidator<RemoveBinRequest>
{
    public RemoveBinValidator()
    {
        RuleFor(request => request.BinId).NotNull();
    }
}
public class RemoveBinRequest : IRequest
{
    public Guid BinId { get; set; }
}
public class RemoveBinHandler : IRequestHandler<RemoveBinRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveBinHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveBinRequest request, CancellationToken cancellationToken)
    {
        var bin = await _context.Bins.FindAsync(request.BinId);
        _context.Bins.Remove(bin);
        await _context.SaveChangesAsync(cancellationToken);

    }

}

