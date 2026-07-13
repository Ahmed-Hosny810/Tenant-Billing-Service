using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Infrastructure.Persistence.Contexts;
using Pos.tenant.Infrastructure.Persistence.Repositories;
using Pos.tenant.Infrastructure.Persistence.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        { 
            services.AddDbContext<ApplicationDbContext>(options=>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IGenericRepositoryAsync<,>), typeof(GenericRepositoryAsync<,>));

            services.AddScoped<ISubscriptionPlanRepositoryAsync, SubscriptionPlanRepositoryAsync>();

            return services;

        }
    }
}
