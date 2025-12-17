// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Features.BookingDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/bookingDetails")]
public class BookingDetailsController
{
    private readonly IMediator _meditator;
    public BookingDetailsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBookingDetailsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBookingDetailsResponse>> Get()
        => await _meditator.Send(new GetBookingDetailsRequest());
    [HttpGet("{bookingDetailId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBookingDetailByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBookingDetailByIdResponse>> GetById([FromRoute] GetBookingDetailByIdRequest request)
        => await _meditator.Send(request);
}

