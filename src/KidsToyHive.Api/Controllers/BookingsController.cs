// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Features.Bookings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController
{
    private readonly IMediator _meditator;
    public BookingsController(IMediator mediator) => _meditator = mediator;
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBookingsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBookingsResponse>> Get()
        => await _meditator.Send(new GetBookingsRequest());
    [HttpGet("my")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBookingsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetMyBookingsResponse>> GetMy()
        => await _meditator.Send(new GetMyBookingsRequest());
    [HttpGet("{bookingId}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBookingByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBookingByIdResponse>> GetById([FromRoute] GetBookingByIdRequest request)
        => await _meditator.Send(request);
}

