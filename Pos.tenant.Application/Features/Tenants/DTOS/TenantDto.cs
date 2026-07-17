using Pos.tenant.Application.Features.Tenants.Queries.GetAllQuery;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.DTOS
{
    public class TenantDto
    {

        public Guid Id { get; set; }

        public string? NameAr { get; set; }
        public string NameEn { get; set; } = null!;

        public string BusinessTypeCode { get; set; } = null!;
        public string Status { get; set; } = null!;

        public string CurrencyCode { get; set; } = null!;
        public string InventoryMode { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public TenantSettingsDto? TenantSettings { get; set; }
        public List<TenantSubscriptionDto?> TenantSubscriptions { get; set; }= new List<TenantSubscriptionDto?>();
        public TenantUsageCountersDto? TenantUsageCounters { get; set; }
    }
}
