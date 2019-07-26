using KidsToyHive.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Services
{
    public interface IPaymentProcessor
    {
        Task<dynamic> ProcessAsync(PaymentDto payment);
    }
}
