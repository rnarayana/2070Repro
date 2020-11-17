using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace WebApp.AppInsights
{
    public class CloudRoleNameInitializer : ITelemetryInitializer
    {
        private readonly string cloudRoleName;

        public CloudRoleNameInitializer(string cloudRoleName)
        {
            this.cloudRoleName = cloudRoleName;
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = this.cloudRoleName;
        }
    }
}