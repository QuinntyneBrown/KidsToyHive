using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Infrastructure.Middleware
{
    public class TenantIdAndUsernameMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantIdAndUsernameMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items.Add("Username", httpContext.Request.GetHeaderValue("Username"));
            httpContext.Items.Add("TenantId", httpContext.Request.GetHeaderValue("TenantId"));
            await _next.Invoke(httpContext);
        }
    }
}
