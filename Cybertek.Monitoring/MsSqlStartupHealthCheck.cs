using Dapper;
using Cybertek.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cybertek.Monitoring
{
    public class MsSqlStartupHealthCheck : IHealthCheck
    {
        private readonly IAppSettings _appSettings;

        public MsSqlStartupHealthCheck(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            await using var sqlConn = new SqlConnection(_appSettings.SqlDatabase.ConnectionString);
            
            var result = await sqlConn.QuerySingleAsync<int>("SELECT 1");
            return result == 1
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Degraded();
        }
    }
}
