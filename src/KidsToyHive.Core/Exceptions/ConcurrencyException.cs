using KidsToyHive.Core.Enums;
using System.Net;

namespace KidsToyHive.Core.Exceptions
{
    public class ConcurrencyException: HttpStatusCodeException
    {
        public ConcurrencyException()
            : base((int)HttpStatusCode.BadRequest, $"{ExceptionType.Concurrency}", "Concurrency", "Concurrency")
        { }
    }
}
