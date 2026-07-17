using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Interfaces.Repositories
{
    public interface ITenantStatusHistoryRepositoryAsync:IGenericRepositoryAsync<TenantStatusHistory,Guid>
    {
        Task<IReadOnlyList<TenantStatusHistory>> GetByTenantIdAsync(Guid tenantId);
    }
}
