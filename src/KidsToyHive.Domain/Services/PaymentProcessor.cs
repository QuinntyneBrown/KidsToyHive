using KidsToyHive.Domain.Common;
using Stripe;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services
{

    public class PaymentProcessor : IPaymentProcessor
    {
        public async Task<bool> ProcessAsync(PaymentDto payment)
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
    }
}
