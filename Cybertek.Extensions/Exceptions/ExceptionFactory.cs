using System.Net;

namespace Cybertek.Extensions.Exceptions;

public static class ExceptionFactory
{
    public static Exception CreateException(string message, HttpStatusCode statusCode, Exception? innerException = null, int operationIndex = 0)
    {
        ServiceError error = CreateError(statusCode, message, null, operationIndex);
        return CreateExceptionFromError(statusCode, error, innerException);
    }

    private static Exception CreateExceptionFromError(HttpStatusCode statusCode,
        ServiceError error,
        Exception? innerException)
    {
        switch (error.Code)
        {
            case "badRequest":
                return new BadRequestException(error, innerException);
            case "notFound": 
                return new NotFoundException(error, innerException);
            case "conflict":
                return new ConflictException(error, innerException);
            case "unauthorized":
                return new UnauthorizedException(error, innerException);
            case "server":
                return new ServerException(error, innerException);
            default:
                return new GenericApiException(error, innerException, statusCode);
        }
    }

    private static ServiceError CreateError(HttpStatusCode statusCode,
        string message,
        string? subMessage,
        int operationIndex,
        string? subCode = null)
    {
        var error = new ServiceError
        {
            SubCode = subCode,
            SubMessage = subMessage,
            OperationIndex = operationIndex
        };

        switch (statusCode)
        {
            case HttpStatusCode.BadRequest:
                error.Code = "badRequest";
                error.Message = message ?? "The request contained invalid data.";
                break;

            case HttpStatusCode.NotFound:
                error.Code = "notFound";
                error.Message = message ?? "The resource is not found.";
                break;

            case HttpStatusCode.Conflict:
                error.Code = "conflict";
                error.Message = message ?? "The request conflicted with an existing record.";
                break;

            case HttpStatusCode.Unauthorized:
                error.Code = "unauthorized";
                error.Message = message ?? "You are not authorized to perform this action.";
                break;

            case HttpStatusCode.InternalServerError:
                error.Code = "server";
                error.Message = message ?? "An exception occurred within the remote service.";
                break;

            default:
                error.Code = "unknown";
                error.Message = message ?? "An error occurred while calling the remote service.";
                break;
        }

        return error;
    }
}
