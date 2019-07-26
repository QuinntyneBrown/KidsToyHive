﻿using KidsToyHive.Domain.Common;
using Stripe;
using System.Threading.Tasks;

//https://www.youtube.com/watch?v=g9E9MHbbwKQ

namespace KidsToyHive.Domain.Services
{
    public class PaymentProcessor : IPaymentProcessor
    {
        public async Task<dynamic> ProcessAsync(PaymentDto payment)
        {
            var optionsToken = new TokenCreateOptions()
            {
                Card = new CreditCardOptions
                {
                    Number = payment.Number,
                    ExpYear = payment.ExpYear,
                    ExpMonth = payment.ExpMonth,
                    Cvc = payment.Cvc
                }
            };

            var serviceToken = new TokenService();
            Token stripeToken = await serviceToken.CreateAsync(optionsToken);
            var options = new ChargeCreateOptions
            {
                Amount = payment.Value,
                Currency = payment.Currency,
                Description = payment.Description,
                Source = stripeToken.Id
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            return charge.Paid;
        }

        public async Task<dynamic> AuthorizeAsync(PaymentDto payment)
        {
            var optionsToken = new TokenCreateOptions()
            {
                Card = new CreditCardOptions
                {
                    Number = payment.Number,
                    ExpYear = payment.ExpYear,
                    ExpMonth = payment.ExpMonth,
                    Cvc = payment.Cvc
                }
            };

            var serviceToken = new TokenService();
            Token stripeToken = await serviceToken.CreateAsync(optionsToken);
            var options = new ChargeCreateOptions
            {
                Amount = payment.Value,
                Currency = payment.Currency,
                Description = payment.Description,
                Source = stripeToken.Id
            };


            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            return charge.Paid;
        }
    }
}
