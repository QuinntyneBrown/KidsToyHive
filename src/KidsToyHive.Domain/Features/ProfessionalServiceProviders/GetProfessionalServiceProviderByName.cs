using FluentValidation;
using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProfessionalServiceProviders;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
    }
}
public class GetProfessionalServiceProviderByNameRequest : IRequest<GetProfessionalServiceProviderByNameResponse>
{
    public string FullName { get; set; }
}
public class GetProfessionalServiceProviderByNameResponse
{
    public ProfessionalServiceProviderDto ProfessionalServiceProvider { get; set; }
}
public class GetProfessionalServiceProviderByNameHandler : IRequestHandler<GetProfessionalServiceProviderByNameRequest, GetProfessionalServiceProviderByNameResponse>
{
    public IAppDbContext _context { get; set; }
    public GetProfessionalServiceProviderByNameHandler(IAppDbContext context) => _context = context;
    public async Task<GetProfessionalServiceProviderByNameResponse> Handle(GetProfessionalServiceProviderByNameRequest request, CancellationToken cancellationToken)
        => new GetProfessionalServiceProviderByNameResponse()
        {
            ProfessionalServiceProvider = (await _context.ProfessionalServiceProviders.SingleAsync(x => x.FullName == request.FullName)).ToDto()
        };
}
