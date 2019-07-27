using MediatR;
using System;

namespace KidsToyHive.Core.Interfaces
{
    public interface IAuthenticatedRequest<TResponse>: IRequest<TResponse>
    {
        Guid CurrentUserId { get; set; }
        string PartitionKey { get; set; }
        string CurrentUsername { get; set; }
    }
}

