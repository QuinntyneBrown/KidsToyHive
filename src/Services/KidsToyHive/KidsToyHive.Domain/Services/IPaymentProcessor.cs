using KidsToyHive.Domain.Common;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services;

public interface IPaymentProcessor
{
    Task<bool> ProcessAsync(PaymentDto payment);
}
