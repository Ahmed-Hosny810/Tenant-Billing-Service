using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Interfaces.Repositories
{
    public interface ISubscriptionPaymentRepositoryAsync:IGenericRepositoryAsync<SubscriptionPayment,Guid>
    {
        Task<SubscriptionPayment?> GetByIdempotencyKeyAsync(
            string idempotencyKey,
            CancellationToken cancellationToken = default);
    }
}
