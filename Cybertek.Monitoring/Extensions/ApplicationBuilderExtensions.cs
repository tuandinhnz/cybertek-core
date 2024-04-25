using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Cybertek.Monitoring.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private const string Startup = "startup";
        private const string Liveness = "liveness";
        
        public static IApplicationBuilder UseKubernetesHealthCheck(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health/startup", new HealthCheckOptions
            {
                Predicate = registration => registration.Tags.Contains(Startup)
            })
                .UseHealthChecks("/health/liveness", new HealthCheckOptions
            {
                Predicate = registration => registration.Tags.Contains(Liveness)
            });

            return app;
        }
    }
}
