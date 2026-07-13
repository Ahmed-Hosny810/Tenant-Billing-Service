using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pos.tenant.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Seeders
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                await SubscriptionPlanSeeder.SeedAsync(context);
                logger.LogInformation("Database seed completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
    }
}
