// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using System.Net;

namespace KidsToyHive.Core.Exceptions;

public class InvalidUsernameOrPasswordException : HttpStatusCodeException
{
    public InvalidUsernameOrPasswordException()
        : base((int)HttpStatusCode.BadRequest, $"{ExceptionType.InvalidUsernameOrPassword}", "Login Failed", "Invalid username or password")
    { }
}

