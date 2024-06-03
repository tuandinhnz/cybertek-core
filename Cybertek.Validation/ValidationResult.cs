namespace Cybertek.Validation;

public class ValidationResult
{
    public ValidationResultSeverity Severity { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }
    public bool AllowBypass { get; set; }
    public int SortOrder { get; set; }
}
