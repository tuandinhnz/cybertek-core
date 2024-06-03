using Cybertek.Apis.Common.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Cybertek.Apis.Common.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        Exception? ex = context.Exception;
        context.Result = ex.ToApiResponse();
        _logger.LogError(ex, "Caught global error.");
    }
}
