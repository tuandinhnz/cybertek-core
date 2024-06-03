using System.Net;
using Cybertek.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Cybertek.Apis.Common.Responses;

public static class ResponseBuilder
{
    public static IActionResult Ok(object? responseBody = null)
    {
        if (responseBody is null)
        {
            return new OkResult();
        }

        return new OkObjectResult(responseBody);
    }
    
    public static IActionResult BadRequest(string? subCode = null, string? subMessage = null, Exception? exception = null)
    {
        ErrorResponse responseBody = ResponseBodyBuilder.Error("badRequest", "Invalid request parameter or object", subCode, subMessage, exception);
        return CreatedHttpActionResult(HttpStatusCode.BadRequest, responseBody);
    }

    public static IActionResult NotFound(string? subCode = null, string? subMessage = null, Exception? exception = null)
    {
        ErrorResponse responseBody = ResponseBodyBuilder.Error("notFound", "Resource could not be found", subCode, subMessage, exception);
        return CreatedHttpActionResult(HttpStatusCode.NotFound, responseBody);
    }
    
    public static IActionResult Unauthorized(string? subCode = null, string? subMessage = null, Exception? exception = null)
    {
        ErrorResponse responseBody = ResponseBodyBuilder.Error("unauthorized", "Request is not authorized", subCode, subMessage, exception);
        return CreatedHttpActionResult(HttpStatusCode.Unauthorized, responseBody);
    }
    
    public static IActionResult Conflict(string? subCode = null, string? subMessage = null, Exception? exception = null)
    {
        ErrorResponse responseBody = ResponseBodyBuilder.Error("conflict", "Request could not be processed because of a conflict", subCode, subMessage, exception);
        return CreatedHttpActionResult(HttpStatusCode.Conflict, responseBody);
    }
    
    public static IActionResult ServerError(string? subCode = null, string? subMessage = null, Exception? exception = null)
    {
        ErrorResponse responseBody = ResponseBodyBuilder.Error("server", "Internal server error", subCode, subMessage, exception);
        return CreatedHttpActionResult(HttpStatusCode.InternalServerError, responseBody);
    }

    private static IActionResult CreatedHttpActionResult(HttpStatusCode statusCode,
        object? responseBody = null)
    {
        if (responseBody is null)
        {
            return new StatusCodeResult((int) statusCode);
        }

        return new ObjectResult(responseBody)
        {
            StatusCode = (int) statusCode
        };
    }

    public static IActionResult FromApiException(ApiException exception)
    {
        return exception switch
        {
            null => ServerError("Unknown error has occured."),
            BadRequestException _ or GenericApiException _ => BadRequest(exception.Error.SubCode, exception.Error.SubMessage, exception),
            ConflictException _ => Conflict(exception.Error.SubCode, exception.Error.SubMessage, exception),
            NotFoundException _ => NotFound(exception.Error.SubCode, exception.Error.SubMessage, exception),
            ServerException _ => ServerError(exception.Error.SubMessage, exception.Error.SubCode, exception),
            UnauthorizedException _ => Unauthorized(exception.Error.SubCode, exception.Error.SubMessage, exception),
            _ => ServerError(exception.Error.SubMessage, exception.Error.SubCode, exception)
        };
    }
}
