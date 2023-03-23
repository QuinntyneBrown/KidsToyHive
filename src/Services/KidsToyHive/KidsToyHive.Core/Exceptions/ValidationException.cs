// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using FluentValidation.Results;
using KidsToyHive.Core.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace KidsToyHive.Core.Exceptions;

public class ValidationException : HttpStatusCodeException
{
    public ValidationException(List<ValidationFailure> failures)
        : base((int)HttpStatusCode.BadRequest, $"{ExceptionType.FailedModelValidation}", "Model Validation Failed", JsonConvert.SerializeObject(failures))
    { }
}

