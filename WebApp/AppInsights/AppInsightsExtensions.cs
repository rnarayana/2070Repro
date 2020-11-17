using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.AppInsights
{
    public static class AppInsightsExtensions
    {
        public static void AddAppInsights(this IServiceCollection services, string cloudRoleName,
            string instrumentationKey)
        {
            services.AddSingleton<ITelemetryInitializer>(new CloudRoleNameInitializer(cloudRoleName));
            services.AddApplicationInsightsTelemetry(o =>
            {
                o.InstrumentationKey = instrumentationKey;
                o.EnableAdaptiveSampling = false;
            });
            services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
            {
                module.EnableSqlCommandTextInstrumentation = true;
            });
            services.AddApplicationInsightsKubernetesEnricher();
        }
    }
}