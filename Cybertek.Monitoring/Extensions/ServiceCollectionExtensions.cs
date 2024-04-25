using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cybertek.Monitoring.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string Startup = "startup";
        private const string Liveness = "liveness";
        public static IServiceCollection AddBasicHealthChecks(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHealthChecks()
                .AddCheck("BasicStartupHealthCheck",
                    () => HealthCheckResult.Healthy(), new[] {Startup})
                .AddCheck("BasicLivenessHealthCheck",
                    () => HealthCheckResult.Healthy(), new[] {Liveness});
                
            return serviceCollection;
        }

        public static IServiceCollection AddAdditionalStartupHealthChecks<THealthCheck>(this IServiceCollection serviceCollection) where THealthCheck : class, IHealthCheck
        {
            serviceCollection.AddHealthChecks()
                .AddCheck<THealthCheck>(nameof(THealthCheck), tags: new[] {Startup});

            return serviceCollection;
        }

        public static IServiceCollection AddAdditionalLivenessHealthChecks<THealthCheck>(this IServiceCollection serviceCollection) where THealthCheck : class, IHealthCheck
        {
            serviceCollection.AddHealthChecks()
                .AddCheck<THealthCheck>(nameof(THealthCheck), tags: new[] {Liveness});

            return serviceCollection;
        }
    }
}
