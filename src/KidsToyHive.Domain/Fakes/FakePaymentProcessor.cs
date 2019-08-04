using KidsToyHive.Domain.Common;
using KidsToyHive.Domain.Services;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Fakes
{
    public class FakePaymentProcessor : IPaymentProcessor
    {
        public Task<bool> ProcessAsync(PaymentDto payment)
            => Task.FromResult(true);
    }
}
