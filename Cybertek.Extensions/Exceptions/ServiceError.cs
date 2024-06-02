namespace Cybertek.Extensions.Exceptions;

public class ServiceError
{
    public string? Code { get; set; }

    public string? Message { get; set; }

    public string? SubCode { get; set; }

    public string? SubMessage { get; set; }
        
    public int OperationIndex { get; set; }
}
