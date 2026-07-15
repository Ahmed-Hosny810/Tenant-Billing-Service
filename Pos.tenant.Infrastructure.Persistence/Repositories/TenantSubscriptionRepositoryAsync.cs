using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Domain.Models;
using Pos.tenant.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Repositories
{
    public class TenantSubscriptionRepositoryAsync : GenericRepositoryAsync<TenantSubscription, Guid>, ITenantSubscriptionRepositoryAsync
    {
        public TenantSubscriptionRepositoryAsync(ApplicationDbContext context) : base(context)
        {
        }
    }
}
