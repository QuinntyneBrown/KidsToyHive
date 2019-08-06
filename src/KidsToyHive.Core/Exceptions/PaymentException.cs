using KidsToyHive.Core.Enums;
using System.Net;

namespace KidsToyHive.Core.Exceptions
{
    public class PaymentException: HttpStatusCodeException
    {
        public PaymentException(string detail)
            : base((int)HttpStatusCode.BadRequest, $"{ExceptionType.Payment}", "Payment Issue", detail)
        {

        }
    }
}
