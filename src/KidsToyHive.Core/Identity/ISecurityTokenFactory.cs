// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Security.Claims;

namespace KidsToyHive.Core.Identity;

public interface ISecurityTokenFactory
{
    string Create(string username, List<Claim> customClaims = null);
}

