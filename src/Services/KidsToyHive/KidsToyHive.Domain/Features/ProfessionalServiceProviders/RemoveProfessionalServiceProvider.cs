using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProfessionalServiceProviders;

public class RemoveProfessionalServiceProviderValidator : AbstractValidator<RemoveProfessionalServiceProviderRequest>
{
    public RemoveProfessionalServiceProviderValidator()
    {
        RuleFor(request => request.ProfessionalServiceProviderId).NotNull();
    }
}
public class RemoveProfessionalServiceProviderRequest : IRequest
{
    public Guid ProfessionalServiceProviderId { get; set; }
}
public class RemoveProfessionalServiceProviderHandler : IRequestHandler<RemoveProfessionalServiceProviderRequest>
{
    private readonly IAppDbContext _context;
    public RemoveProfessionalServiceProviderHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveProfessionalServiceProviderRequest request, CancellationToken cancellationToken)
    {
        var professionalServiceProvider = await _context.ProfessionalServiceProviders.FindAsync(request.ProfessionalServiceProviderId);
        _context.ProfessionalServiceProviders.Remove(professionalServiceProvider);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
