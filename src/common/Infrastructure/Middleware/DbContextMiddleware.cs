using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Middleware
{
    public class DbContextMiddleware
    {
        private readonly RequestDelegate _next;

        public DbContextMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items.Add("Username", httpContext.User.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName)?.Value);
            httpContext.Items.Add("TenantId", httpContext.Request.GetHeaderValue("TenantId"));
            await _next.Invoke(httpContext);
        }
    }
}
