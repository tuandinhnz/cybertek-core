using System.Net;

namespace Cybertek.Extensions.Exceptions;

public class GenericApiException : ApiException
{
    public GenericApiException(ServiceError error,
        Exception? innerException = null,
        HttpStatusCode? statusCode = null) : base(error, innerException, statusCode)
    {
    }
}
