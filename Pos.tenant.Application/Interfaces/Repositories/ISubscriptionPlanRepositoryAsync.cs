using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Interfaces.Repositories
{
    public interface ISubscriptionPlanRepositoryAsync:IGenericRepositoryAsync<SubscriptionPlan,Guid>
    {
        Task<SubscriptionPlan?> GetByCodeAsync(string code);

        Task<IReadOnlyList<SubscriptionPlan>> GetActivePlansAsync();

        Task<bool> IsCodeExistsAsync(string code);
    }
}
