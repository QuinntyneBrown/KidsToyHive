// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Common;

public static class IdGenerator
{
    public static string GetNextId() => $"{Guid.NewGuid()}";
}

