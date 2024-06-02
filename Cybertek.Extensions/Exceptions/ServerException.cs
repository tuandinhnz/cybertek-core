using System.Net;

namespace Cybertek.Extensions.Exceptions;

public class ServerException : ApiException
{
    public ServerException(ServiceError error,
        Exception? innerException = null) : base(error, innerException, HttpStatusCode.InternalServerError)
    {
    }
}
