using Microsoft.EntityFrameworkCore;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Domain.Models;
using Pos.tenant.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Repositories
{
    public class TenantRepositoryAsync : GenericRepositoryAsync<Tenant, Guid>, ITenantRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public TenantRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
