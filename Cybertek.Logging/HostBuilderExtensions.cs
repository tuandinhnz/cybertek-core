using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;

namespace Cybertek.Logging
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder builder)
        {
            builder.UseSerilog((context, logger) =>
            {
                logger
                    .Enrich.FromLogContext()
                    .Enrich.WithSpan();
                if (context.HostingEnvironment.IsDevelopment())
                {
                    logger.WriteTo.Console(
                        theme: AnsiConsoleTheme.Code, 
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {TraceId} {Level:u3} {Message}{NewLine}{Exception}]");
                }
                else
                {
                    logger.WriteTo.Console(new JsonFormatter());
                }
            });

            return builder;
        }
    }
}
