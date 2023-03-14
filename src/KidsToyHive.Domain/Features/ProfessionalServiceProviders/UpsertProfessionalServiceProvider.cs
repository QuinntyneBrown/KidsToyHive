using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProfessionalServiceProviders;

public class UpsertProfessionalServiceProviderValidator : AbstractValidator<UpsertProfessionalServiceProviderRequest>
{
    public UpsertProfessionalServiceProviderValidator()
    {
        RuleFor(request => request.ProfessionalServiceProvider).NotNull();
        RuleFor(request => request.ProfessionalServiceProvider).SetValidator(new ProfessionalServiceProviderDtoValidator());
    }
}
public class UpsertProfessionalServiceProviderRequest : IRequest<UpsertProfessionalServiceProviderResponse>
{
    public ProfessionalServiceProviderDto ProfessionalServiceProvider { get; set; }
}
public class UpsertProfessionalServiceProviderResponse
{
    public Guid ProfessionalServiceProviderId { get; set; }
}
public class UpsertProfessionalServiceProviderHandler : IRequestHandler<UpsertProfessionalServiceProviderRequest, UpsertProfessionalServiceProviderResponse>
{
    public IAppDbContext _context { get; set; }
    public UpsertProfessionalServiceProviderHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertProfessionalServiceProviderResponse> Handle(UpsertProfessionalServiceProviderRequest request, CancellationToken cancellationToken)
    {
        var professionalServiceProvider = await _context.ProfessionalServiceProviders.FindAsync(request.ProfessionalServiceProvider.ProfessionalServiceProviderId);
        if (professionalServiceProvider == null)
        {
            professionalServiceProvider = new ProfessionalServiceProvider();
            _context.ProfessionalServiceProviders.Add(professionalServiceProvider);
        }
        professionalServiceProvider.FullName = request.ProfessionalServiceProvider.FullName;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertProfessionalServiceProviderResponse() { ProfessionalServiceProviderId = professionalServiceProvider.ProfessionalServiceProviderId };
    }
}
