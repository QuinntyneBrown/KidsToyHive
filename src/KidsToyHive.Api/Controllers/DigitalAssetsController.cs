using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

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
