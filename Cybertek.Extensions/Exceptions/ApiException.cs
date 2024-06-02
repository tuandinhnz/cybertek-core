using System.Net;

namespace Cybertek.Extensions.Exceptions;

public abstract class ApiException : HttpRequestException
{
    protected ApiException(ServiceError error,
        Exception? innerException = null,
        HttpStatusCode? statusCode = null) 
        : 
        base(error.Message, innerException, statusCode)
    {
        Error = error;
    }

    public ServiceError Error { get; }

    public override string ToString()
    {
        return InnerException == null
            ? base.ToString()
            : $"{base.ToString()}\nInnerException: {InnerException?.ToString() ?? string.Empty}";
    }
}
