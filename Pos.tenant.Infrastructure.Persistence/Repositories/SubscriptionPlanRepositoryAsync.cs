using Microsoft.EntityFrameworkCore;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Domain.Models;
using Pos.tenant.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Repositories
{
    public class SubscriptionPlanRepositoryAsync : GenericRepositoryAsync<SubscriptionPlan, Guid>, ISubscriptionPlanRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionPlanRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<SubscriptionPlan>> GetActivePlansAsync()
        {
           return await _context.SubscriptionPlans
                .AsNoTracking()
                .Where(s=>s.IsActive)
                .ToListAsync();
        }

        public async Task<SubscriptionPlan?> GetByCodeAsync(string code)
        {
            return await _context.SubscriptionPlans
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<bool> IsCodeExistsAsync(string code)
        {
            return await _context.SubscriptionPlans
                .AnyAsync(x => x.Code == code);
        }
    }
}
