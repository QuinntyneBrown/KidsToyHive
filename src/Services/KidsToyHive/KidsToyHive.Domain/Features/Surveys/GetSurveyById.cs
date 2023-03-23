using KidsToyHive.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Surveys;

public class GetSurveyByIdRequest : IRequest<GetSurveyByIdResponse>
{
    public Guid SurveyId { get; set; }
}
public class GetSurveyByIdResponse
{
    public SurveyDto Survey { get; set; }
}
public class GetSurveyByIdHandler : IRequestHandler<GetSurveyByIdRequest, GetSurveyByIdResponse>
{
    public IAppDbContext _context { get; set; }
    public GetSurveyByIdHandler(IAppDbContext context) => _context = context;
    public async Task<GetSurveyByIdResponse> Handle(GetSurveyByIdRequest request, CancellationToken cancellationToken)
        => new GetSurveyByIdResponse()
        {
            Survey = (await _context.Surveys.FindAsync(request.SurveyId)).ToDto()
        };
}
