namespace Cybertek.Apis.Common.Responses;

public class ErrorDetails
{
    public ErrorDetails(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; set; }

    public string Message { get; set; }

    public string? SubCode { get; set; }

    public string? SubMessage { get; set; }
}
