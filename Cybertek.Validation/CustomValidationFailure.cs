using FluentValidation.Results;

namespace Cybertek.Validation;

public class CustomValidationFailure : ValidationFailure
{
    public CustomValidationFailure(string propertyName, string errorMessage) : base(propertyName, errorMessage)
    {
    }

    public CustomValidationFailure(string propertyName, string errorMessage, object attemptedValue) : base(propertyName, errorMessage, attemptedValue)
    {
    }

    public CustomValidationFailure(ValidationFailure validation) : base(validation?.PropertyName, validation?.ErrorMessage, validation?.AttemptedValue)
    {
        Validate.IsNotNull(validation, nameof(validation));
    }

    public string? ErrorMessageResourceName { get; set; }
    public string? ErrorCodeResourceName { get; set; }
    public bool ForceUseResourceName { get; set; }
    public bool AllowBypass { get; set; }
    public int SortOrder { get; set; }
}
