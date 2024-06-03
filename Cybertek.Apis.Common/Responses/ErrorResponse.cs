using FluentValidation.Results;
using ValidationResult = Cybertek.Validation.ValidationResult;

namespace Cybertek.Apis.Common.Responses;

public class ErrorResponse
{
    public ErrorDetails? Error { get; set; }
    public IList<ValidationResult>? ValidationResults { get; set; }
}
