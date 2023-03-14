using KidsToyHive.Core.Enums;
using System.Net;

namespace KidsToyHive.Core.Exceptions;

public class CustomerExistsWithEmailException : HttpStatusCodeException
{
    public CustomerExistsWithEmailException()
        : base((int)HttpStatusCode.BadRequest, $"{ExceptionType.CustomerExistsWithEmail}", "CustomerExistsWithEmail", "CustomerExistsWithEmail")
    { }
}
