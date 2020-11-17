using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.AppInsights;

namespace WebApp
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var appInsightsKey = this.configuration["App:ApplicationInsightsKey"];
            services.AddAppInsights("SAMPLE-SERVICE", appInsightsKey);
            services.AddControllers();

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                    {
                        Predicate = _ => false,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                endpoints.MapHealthChecks("/health/live",
                    new HealthCheckOptions
                    {
                        // Exclude all checks and return a 200-Ok.
                        Predicate = _ => false, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
            });
        }
    }
}