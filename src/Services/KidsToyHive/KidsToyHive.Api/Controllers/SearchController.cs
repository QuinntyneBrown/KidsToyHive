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
