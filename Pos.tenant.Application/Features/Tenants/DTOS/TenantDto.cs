using Pos.tenant.Application.Features.Tenants.Queries.GetAllQuery;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.DTOS
{
    public class TenantDto
    {

        public TenantDto()
        {
            
        }

        public TenantDto(Tenant tenant,TenantIncludes includes)
        {
            includes ??= new TenantIncludes();
            this.Id = tenant.Id;
            this.NameAr = tenant.NameAr;
            this.NameEn = tenant.NameEn;
            this.BusinessTypeCode = tenant.BusinessTypeCode;
            this.InventoryMode = tenant.InventoryMode;
            this.Status = tenant.Status;
            this.CurrencyCode = tenant.CurrencyCode;
            this.CreatedAt = tenant.CreatedAt;
            this.UpdatedAt = tenant.UpdatedAt;
            if (includes.TenantSettings)
                this.Settings= tenant.TenantSettings != null? new TenantSettingsDto(tenant.TenantSettings): null;
            if (includes.TenantUsageCounters)
                UsageCounters = tenant.TenantUsageCounters != null? new TenantUsageCountersDto(tenant.TenantUsageCounters): null;
            

            if (includes.TenantSubscription)
            {
                var currentSubscription = tenant.TenantSubscriptions?
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault();

                Subscription = currentSubscription != null
                    ? new TenantSubscriptionDto(currentSubscription)
                    : null;
            }


        }

        public Guid Id { get; set; }

        public string? NameAr { get; set; }
        public string NameEn { get; set; } = null!;

        public string BusinessTypeCode { get; set; } = null!;
        public string Status { get; set; } = null!;

        public string CurrencyCode { get; set; } = null!;
        public string InventoryMode { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public TenantSettingsDto? Settings { get; set; }
        public TenantSubscriptionDto? Subscription { get; set; }
        public TenantUsageCountersDto? UsageCounters { get; set; }
    }
}
