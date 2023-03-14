using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Surveys;

 public class GetSurveysRequest : IRequest<GetSurveysResponse> { }
 public class GetSurveysResponse
 {
     public IEnumerable<SurveyDto> Surveys { get; set; }
 }
 public class GetSurveysHandler : IRequestHandler<GetSurveysRequest, GetSurveysResponse>
 {
     private readonly IAppDbContext _context;
     public GetSurveysHandler(IAppDbContext context) => _context = context;
     public async Task<GetSurveysResponse> Handle(GetSurveysRequest request, CancellationToken cancellationToken)
         => new GetSurveysResponse()
         {
             Surveys = await _context.Surveys.Select(x => x.ToDto()).ToArrayAsync()
         };
 }
