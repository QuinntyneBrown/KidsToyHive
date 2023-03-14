using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProfessionalServiceProviders;

public class GetProfessionalServiceProvidersRequest : IRequest<GetProfessionalServiceProvidersResponse> { }
public class GetProfessionalServiceProvidersResponse
{
    public IEnumerable<ProfessionalServiceProviderDto> ProfessionalServiceProviders { get; set; }
}
public class GetProfessionalServiceProvidersHandler : IRequestHandler<GetProfessionalServiceProvidersRequest, GetProfessionalServiceProvidersResponse>
{
    private readonly IAppDbContext _context;
    public GetProfessionalServiceProvidersHandler(IAppDbContext context) => _context = context;
    public async Task<GetProfessionalServiceProvidersResponse> Handle(GetProfessionalServiceProvidersRequest request, CancellationToken cancellationToken)
        => new GetProfessionalServiceProvidersResponse()
        {
            ProfessionalServiceProviders = await _context.ProfessionalServiceProviders.Select(x => x.ToDto()).ToArrayAsync()
        };
}
