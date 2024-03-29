// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Api.Behaviours;

public class AuthenticatedRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthenticatedRequestBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IAuthenticatedRequest<TResponse> authenticatedRequest)
        {
            if (!string.IsNullOrEmpty(authenticatedRequest.PartitionKey))
                return await next();
            var user = _httpContextAccessor.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                authenticatedRequest.CurrentUserId = new Guid(user.Claims.First(x => x.Type == "UserId").Value);
                authenticatedRequest.PartitionKey = user.Claims.First(x => x.Type == "PartitionKey").Value;
                authenticatedRequest.CurrentUsername = user.Identity.Name;
            }
        }
        return await next();
    }
}

