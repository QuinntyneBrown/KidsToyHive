using KidsToyHive.Domain.Features.Videos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Controllers
{
    [ApiController]
    [Route("api/videos")]
    public class VideosController
    {
        private readonly IMediator _meditator;

        public VideosController(IMediator mediator) => _meditator = mediator;

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetVideos.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetVideos.Response>> Get()
            => await _meditator.Send(new GetVideos.Request());

        [AllowAnonymous]
        [HttpGet("{videoId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetVideoById.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetVideoById.Response>> GetById([FromRoute]GetVideoById.Request request)
            => await _meditator.Send(request);
    }
}
