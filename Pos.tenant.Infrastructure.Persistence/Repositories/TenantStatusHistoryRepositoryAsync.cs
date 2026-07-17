using Microsoft.EntityFrameworkCore;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Domain.Models;
using Pos.tenant.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Repositories
{
    public class TenantStatusHistoryRepositoryAsync: GenericRepositoryAsync<TenantStatusHistory, Guid>, ITenantStatusHistoryRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public TenantStatusHistoryRepositoryAsync(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TenantStatusHistory>> GetByTenantIdAsync(Guid tenantId)
        {
            return await _context.TenantStatusHistory
                .AsNoTracking()
                .Where(x => x.TenantId == tenantId)
                .OrderByDescending(x => x.ChangedAt)
                .ToListAsync();
        }
    }
}
