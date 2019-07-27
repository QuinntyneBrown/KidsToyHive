using System;
using KidsToyHive.Core.Interfaces;
using MediatR;

namespace KidsToyHive.Domain.Common
{
    public abstract class AuthenticatedRequest<TResponse>: IAuthenticatedRequest<TResponse>
    {
        public string CurrentUsername { get; set; }
        public Guid CurrentUserId { get; set; }
        public string PartitionKey { get; set; }
    }
}
