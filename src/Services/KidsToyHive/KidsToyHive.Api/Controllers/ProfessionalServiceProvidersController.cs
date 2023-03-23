// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Domain.Features.ProfessionalServiceProviders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/professionalServiceProviders")]
public class ProfessionalServiceProvidersController
{
    private readonly IMediator _meditator;
    public ProfessionalServiceProvidersController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetProfessionalServiceProvidersResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetProfessionalServiceProvidersResponse>> Get()
        => await _meditator.Send(new GetProfessionalServiceProvidersRequest());
    [HttpGet("{professionalServiceProviderId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetProfessionalServiceProviderByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetProfessionalServiceProviderByIdResponse>> GetById([FromRoute] GetProfessionalServiceProviderByIdRequest request)
        => await _meditator.Send(request);
    [AllowAnonymous]
    [HttpGet("name/{fullName}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetProfessionalServiceProviderByNameResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetProfessionalServiceProviderByNameResponse>> GetByName([FromRoute] GetProfessionalServiceProviderByNameRequest request)
        => await _meditator.Send(request);
}

