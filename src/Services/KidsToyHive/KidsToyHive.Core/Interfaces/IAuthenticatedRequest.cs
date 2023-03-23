// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using System;

namespace KidsToyHive.Core.Interfaces;

public interface IAuthenticatedRequest<TResponse> : IRequest<TResponse>
{
    Guid CurrentUserId { get; set; }
    string PartitionKey { get; set; }
    string CurrentUsername { get; set; }
}

