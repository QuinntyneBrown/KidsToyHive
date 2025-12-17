// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Exceptions;
using KidsToyHive.Core.Common;
using Stripe;
using System;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Services;

public class PaymentProcessor : IPaymentProcessor
{
    public async Task<bool> ProcessAsync(PaymentDto payment)
    {
        try
        {
            var optionsToken = new TokenCreateOptions()
            {
                Card = new TokenCardOptions
                {
                    Number = payment.Number,
/*                    ExpYear = payment.ExpYear,
                    ExpMonth = payment.ExpMonth,*/
                    Cvc = payment.Cvc
                }
            };
            var serviceToken = new TokenService();
            var stripeToken = await serviceToken.CreateAsync(optionsToken);
            var options = new ChargeCreateOptions
            {
                Amount = payment.Value,
                Currency = payment.Currency,
                Description = payment.Description,
                Source = stripeToken.Id
            };
            var service = new ChargeService();
            var charge = await service.CreateAsync(options);
            return charge.Paid;
        }
        catch (Exception e)
        {
            throw new PaymentException(e.Message);
        }
    }
}

