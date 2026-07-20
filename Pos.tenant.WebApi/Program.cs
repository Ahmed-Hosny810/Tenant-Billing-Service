using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Pos.tenant.Infrastructure.Persistence;
using Pos.tenant.Infrastructure.Shared;
using Pos.tenant.Infrastructure.Persistence.Contexts;
using Pos.tenant.Infrastructure.Persistence.Seeders;
using Pos.tenant.WebApi.Extensions;
using Pos.tenant.Application;
using Serilog;
using Pos.tenant.WebApi.MiddleWares;
namespace Pos.tenant.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                 .WriteTo.Console()
                 .CreateBootstrapLogger();

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext();
            });

            builder.Services.AddControllers();
            // AddHealthChecks
            builder.Services
                    .AddHealthChecks()
                    .AddDbContextCheck<ApplicationDbContext>(
                        name: "tenant-billing-db",
                        failureStatus: HealthStatus.Unhealthy,
                        tags: new[] { "ready", "db" });

            // API Versioning
            builder.Services.AddApiVersioningExtension();
            // AddPersistenceServices
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddSharedInfrastructure(builder.Configuration);
            builder.Services.AddApplicationLayer();
            // Swagger (via extension)
            builder.Services.AddSwaggerExtension();

            var app = builder.Build();

            app.UseMiddleware<ErrorHandlerMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExtension();
                await app.Services.SeedDatabaseAsync();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //app process is alive.
            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false
            });

            //the app is ready to receive traffic, including database check
            app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("ready")
            });
            app.MapControllers();

            app.Run();
        }
    }
}
