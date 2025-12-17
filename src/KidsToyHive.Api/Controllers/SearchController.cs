// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KidsToyHive.Api.Controllers;

[ApiController]
public class SearchController
{
    private readonly IMediator _mediator;
    public SearchController(IMediator mediator)
    {
        _mediator = mediator;
    }
}

