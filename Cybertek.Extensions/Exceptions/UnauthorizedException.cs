using System.Net;

namespace Cybertek.Extensions.Exceptions;

public class UnauthorizedException : ApiException
{
    public UnauthorizedException(ServiceError error,
        Exception? innerException = null) : base(error, innerException, HttpStatusCode.Unauthorized)
    {
    }
}
