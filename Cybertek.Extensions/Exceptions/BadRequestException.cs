using System.Net;

namespace Cybertek.Extensions.Exceptions;

public class BadRequestException : ApiException
{
    public BadRequestException(ServiceError error,
        Exception? innerException = null) : base(error, innerException, HttpStatusCode.BadRequest)
    {
    }
}
