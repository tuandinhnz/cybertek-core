using Cybertek.Validation;
using FluentValidation;
using FluentValidation.Results;
using ValidationResult = Cybertek.Validation.ValidationResult;

namespace Cybertek.Apis.Common.Responses;

public static class ResponseBodyBuilder
{
    public static ErrorResponse Error(string code, string message, string? subCode = null, string? subMessage = null, Exception? exception = null)
    {
        var response = new ErrorResponse
        {
            Error = new ErrorDetails(code, message)
            {
                SubCode = subCode,
                SubMessage = string.IsNullOrWhiteSpace(subMessage) ? exception?.Message : subMessage
            }
        };

        if (exception is ValidationException validationException)
        {
            response.ValidationResults = ConvertValidationException(validationException);
        }

        return response;
    }

    private static IList<ValidationResult> ConvertValidationException(ValidationException validationException)
    {
        return validationException.Errors
            .DistinctBy(failure => failure.ErrorCode)
            .Select(failure =>
            {
                var result = new ValidationResult
                {
                    Code = failure.ErrorCode,
                    Message = failure.ErrorMessage,
                    Severity = ConvertValidationSeverity(failure)
                };

                if (failure is CustomValidationFailure fluentValidationFailure)
                {
                    result.AllowBypass = fluentValidationFailure.AllowBypass;
                    result.SortOrder = fluentValidationFailure.SortOrder;
                }

                return result;
            })
            .OrderByDescending(result => result.SortOrder)
            .ThenBy(result => result.Severity)
            .ToList();
    }

    private static ValidationResultSeverity ConvertValidationSeverity(ValidationFailure failure)
    {
        return failure.Severity switch
        {
            Severity.Error => ValidationResultSeverity.Error,
            Severity.Warning => ValidationResultSeverity.Warning,
            Severity.Info => ValidationResultSeverity.Info,
            _ => throw new ArgumentOutOfRangeException(nameof(failure), $"Unsupported validation failure severity: '{failure.Severity}'")
        };
    }
}
