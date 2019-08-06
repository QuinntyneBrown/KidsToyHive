using KidsToyHive.Core.Enums;
using System.Net;

namespace KidsToyHive.Core.Exceptions
{
    public class OutOfStockException : HttpStatusCodeException
    {
        public OutOfStockException()
            : base((int)HttpStatusCode.BadRequest, $"{ExceptionType.OutOfStockException}", "OutOfStockException", "OutOfStockException")
        { }
    }
}
