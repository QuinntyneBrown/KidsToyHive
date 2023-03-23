// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Common;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Services;

public interface IPaymentProcessor
{
    Task<bool> ProcessAsync(PaymentDto payment);
}

