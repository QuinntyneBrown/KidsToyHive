using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Surveys;

public class GetSurveyById
{
    public class Request : IRequest<Response>
    {
        public Guid SurveyId { get; set; }
    }
    public class Response
    {
        public SurveyDto Survey { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        public IAppDbContext _context { get; set; }
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            => new Response()
            {
                Survey = (await _context.Surveys.FindAsync(request.SurveyId)).ToDto()
            };
    }
}
