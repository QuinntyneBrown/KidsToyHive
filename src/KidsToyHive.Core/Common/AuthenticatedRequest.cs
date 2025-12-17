// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using KidsToyHive.Core.Interfaces;
using MediatR;

namespace KidsToyHive.Core.Common;

public abstract class AuthenticatedRequest<TResponse> : IAuthenticatedRequest<TResponse>
{
    public string CurrentUsername { get; set; }
    public Guid CurrentUserId { get; set; }
    public string PartitionKey { get; set; }
}

