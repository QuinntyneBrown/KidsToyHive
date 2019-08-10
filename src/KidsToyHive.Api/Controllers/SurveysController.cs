using KidsToyHive.Domain.Features.Surveys;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/surveys")]
    public class SurveysController
    {
        private readonly IMediator _meditator;

        public SurveysController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSurveys.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSurveys.Response>> Get()
            => await _meditator.Send(new GetSurveys.Request());

        [HttpGet("{surveyId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetSurveyById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetSurveyById.Response>> GetById([FromRoute]GetSurveyById.Request request)
            => await _meditator.Send(request);
    }
}
