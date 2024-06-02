using System.Net;

namespace Cybertek.Extensions.Exceptions;

public class ConflictException : ApiException
{
    public ConflictException(ServiceError error,
        Exception? innerException = null) : base(error, innerException, HttpStatusCode.Conflict)
    {
    }
}
