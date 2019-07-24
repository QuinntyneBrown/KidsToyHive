using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/digitalAssets")]
    public class DigitalAssetsController
    {
        private readonly IMediator _meditator;

        public DigitalAssetsController(IMediator mediator) => _meditator = mediator;

    }
}
