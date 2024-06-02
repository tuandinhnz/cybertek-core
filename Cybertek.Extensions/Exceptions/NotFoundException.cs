using System.Net;

namespace Cybertek.Extensions.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(ServiceError error,
        Exception? innerException = null) : base(error, innerException, HttpStatusCode.NotFound)
    {
    }
}
