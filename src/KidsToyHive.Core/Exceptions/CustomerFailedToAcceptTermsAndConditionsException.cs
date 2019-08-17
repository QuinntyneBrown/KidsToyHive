using KidsToyHive.Core.Enums;
using System.Net;

namespace KidsToyHive.Core.Exceptions
{
    public class CustomerFailedToAcceptTermsAndConditionsException: HttpStatusCodeException
    {
        public CustomerFailedToAcceptTermsAndConditionsException()
            :base((int)HttpStatusCode.BadRequest,$"{ExceptionType.CustomerFailedToAcceptTermsAndConditions}", "Customer Failed to Accept Terms and Conditions","Customer Failed to Accept Terms and Conditions")
        {

        }
    }
}
