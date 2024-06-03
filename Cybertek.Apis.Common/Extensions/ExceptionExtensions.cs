using System.Text;
using Cybertek.Apis.Common.Responses;
using Cybertek.Extensions.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Cybertek.Apis.Common.Extensions;

public static class ExceptionExtensions
{
    public static IActionResult ToApiResponse(this Exception ex)
    {
        if (ex is AggregateException exception)
        {
            ex = exception.InnerExceptions.FirstOrDefault() ?? exception;
        }

        switch (ex)
        {
            case ApiException apiException:
                return ResponseBuilder.FromApiException(apiException);
            case ValidationException _:
            case System.ComponentModel.DataAnnotations.ValidationException _:
                return ResponseBuilder.BadRequest("validationFailed", ex.Message, ex);
            case ArgumentException _:
                return ResponseBuilder.BadRequest(subMessage: ex.Message, exception: ex);
            case KeyNotFoundException _:
                return ResponseBuilder.NotFound(subMessage: ex.Message, exception: ex);
            case UnauthorizedAccessException _:
                return ResponseBuilder.Unauthorized(subMessage: ex.Message, exception: ex);
            case JsonPatchException _:
            case InvalidCastException _:
                return ResponseBuilder.BadRequest("Malformed json document,", ex.Message, ex);
            default:
#if DEBUG
                return ResponseBuilder.ServerError(ex.GetExceptionAsText(), exception: ex);
#else
                    return ResponseBuilder.ServerError(ex.Message, exception: ex);
#endif
        }
    }

    public static string GetExceptionAsText(this Exception? exception, int depthCount = 0)
    {
        if (exception == null)
        {
            return string.Empty;
        }

        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(exception.ToString());

        if (exception.InnerException != null)
        {
            depthCount++;
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Inner Exception {depthCount}:");
            stringBuilder.AppendLine(exception.InnerException.GetExceptionAsText(depthCount));
        }

        return stringBuilder.ToString();
    }
}
