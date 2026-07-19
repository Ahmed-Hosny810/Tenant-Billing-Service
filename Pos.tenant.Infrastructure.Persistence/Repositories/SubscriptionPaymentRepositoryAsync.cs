using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Domain.Models;
using Pos.tenant.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Repositories
{
    public class SubscriptionPaymentRepositoryAsync : GenericRepositoryAsync<SubscriptionPayment, Guid>, ISubscriptionPaymentRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionPaymentRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
