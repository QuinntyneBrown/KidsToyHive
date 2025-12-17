// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Features.Surveys;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/surveys")]
public class SurveysController
{
    private readonly IMediator _meditator;
    public SurveysController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetSurveysResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetSurveysResponse>> Get()
        => await _meditator.Send(new GetSurveysRequest());
    [HttpGet("{surveyId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetSurveyByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetSurveyByIdResponse>> GetById([FromRoute] GetSurveyByIdRequest request)
        => await _meditator.Send(request);
}

