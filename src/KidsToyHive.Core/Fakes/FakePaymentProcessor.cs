// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Common;
using KidsToyHive.Core.Services;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Fakes;

public class FakePaymentProcessor : IPaymentProcessor
{
    public Task<bool> ProcessAsync(PaymentDto payment)
        => Task.FromResult(true);
}

