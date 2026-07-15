using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Interfaces.Repositories
{
    public interface ITenantRepositoryAsync:IGenericRepositoryAsync<Tenant,Guid>
    {
        Task<bool> IsSubdomainExistsAsync(string? subdomain);
        Task<bool> IsSlugExistsAsync(string slug);
    }
}
