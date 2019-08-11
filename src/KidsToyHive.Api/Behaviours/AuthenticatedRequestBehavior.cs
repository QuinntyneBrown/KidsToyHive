using KidsToyHive.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Behaviours
{
    public class AuthenticatedRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedRequestBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is IAuthenticatedRequest<TResponse> authenticatedRequest)
            {
                if (!string.IsNullOrEmpty(authenticatedRequest.PartitionKey))
                    return next();

                var user = _httpContextAccessor.HttpContext.User;

                if (user.Identity.IsAuthenticated)
                {
                    authenticatedRequest.CurrentUserId = new Guid(user.Claims.First(x => x.Type == "UserId").Value);
                    authenticatedRequest.PartitionKey = user.Claims.First(x => x.Type == "PartitionKey").Value;
                    authenticatedRequest.CurrentUsername = user.Claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value;
                }
            }

            return next();
        }
    }
}