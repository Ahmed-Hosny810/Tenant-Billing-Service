using Microsoft.EntityFrameworkCore;
using Pos.tenant.Domain.Common;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantSettings> TenantSettings { get; set; }
        public DbSet<TenantStatusHistory> TenantStatusHistory { get; set; }
        public DbSet<TenantUsageCounters> TenantUsageCounters { get; set; }
        public DbSet<TenantSubscription> TenantSubscriptions { get; set; }
        public DbSet<SubscriptionInvoice> SubscriptionInvoices { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInfo()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.Id == Guid.Empty)
                        entry.Entity.Id = Guid.NewGuid();

                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = null;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                }
            }
        }
    }
}
