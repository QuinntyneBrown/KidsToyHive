using KidsToyHive.Domain.Features.DigitalAssets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/digitalAssets")]
    public class DigitalAssetsController
    {
        private readonly IMediator _mediator;

        public DigitalAssetsController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpGet("serve/{digitalAssetId}")]
        public async Task<IActionResult> Serve([FromRoute]GetDigitalAssetById.Request request)
        {
            var response = await _mediator.Send(request);
            return new FileContentResult(response.DigitalAsset.Bytes, response.DigitalAsset.ContentType);
        }

        [AllowAnonymous]
        [HttpGet("serve/file/{name}")]
        public async Task<IActionResult> Serve([FromRoute]GetDigitalAssetByName.Request request)
        {
            var response = await _mediator.Send(request);
            return new FileContentResult(response.DigitalAsset.Bytes, response.DigitalAsset.ContentType);
        }
    }
}
