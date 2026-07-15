using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Domain.Models;
using Pos.tenant.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Repositories
{
    public class TenantSettingsRepositoryAsync : GenericRepositoryAsync<TenantSettings, Guid>, ITenantSettingsRepositoryAsync
    {
        public TenantSettingsRepositoryAsync(ApplicationDbContext context) : base(context)
        {
        }
    }
}
