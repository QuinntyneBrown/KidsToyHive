using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KidsToyHive.SPA.Clients;

namespace KidsToyHive.SPA.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/brands")]
    public class BrandController
    {
        private readonly BrandsClient _client;

        public BrandController(BrandsClient client) => _client = client;

        [HttpPost]
        public async Task<ActionResult<dynamic>> Save(dynamic brand)
            => await _client.Save(brand);

        [HttpDelete("{brandId}")]
        public async Task Remove(int brandId)
            => await _client.Remove(brandId);

        [AllowAnonymous]
        [HttpGet("{brandId}")]
        public async Task<ActionResult<dynamic>> GetById([FromRoute]int brandId)
            => await _client.GetById(brandId);

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<dynamic>> Get()
            => await _client.Get();
    }
}
