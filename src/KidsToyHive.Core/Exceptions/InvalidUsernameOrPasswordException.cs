using KidsToyHive.Core.Enums;
using System.Net;

namespace KidsToyHive.Core.Exceptions
{
    public class InvalidUsernameOrPasswordException: HttpStatusCodeException
    {
        public InvalidUsernameOrPasswordException()
            :base((int)HttpStatusCode.BadRequest, $"{ExceptionType.InvalidUsernameOrPassword}", "Login Failed", "Invalid username or password")
        { }
    }
}
