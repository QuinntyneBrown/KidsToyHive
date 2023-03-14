using KidsToyHive.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProfessionalServiceProviders;

public class GetProfessionalServiceProviderByIdRequest : IRequest<GetProfessionalServiceProviderByIdResponse>
{
    public Guid ProfessionalServiceProviderId { get; set; }
}
public class GetProfessionalServiceProviderByIdResponse
{
    public ProfessionalServiceProviderDto ProfessionalServiceProvider { get; set; }
}
public class GetProfessionalServiceProviderByIdHandler : IRequestHandler<GetProfessionalServiceProviderByIdRequest, GetProfessionalServiceProviderByIdResponse>
{
    public IAppDbContext _context { get; set; }
    public GetProfessionalServiceProviderByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetProfessionalServiceProviderByIdResponse> Handle(GetProfessionalServiceProviderByIdRequest request, CancellationToken cancellationToken)
        => new GetProfessionalServiceProviderByIdResponse()
        {
            ProfessionalServiceProvider = (await _context.ProfessionalServiceProviders.FindAsync(request.ProfessionalServiceProviderId)).ToDto()
        };
}
