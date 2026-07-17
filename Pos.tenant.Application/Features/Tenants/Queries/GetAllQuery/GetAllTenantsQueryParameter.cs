using Pos.tenant.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.Queries.GetAllQuery
{
    public class GetAllTenantsQueryParameter:RequestParameter<TenantOrderKey>
    {
        public TenantFilter? Filter { get; set; } 
        public TenantIncludes? Includes { get; set; }
    }
    public class TenantFilter
    {
        public Guid? Id { get; set; }

        public string? NameAr { get; set; }
        public string? NameEn { get; set; } 

        public string? BusinessTypeCode { get; set; } 
        public string? Status { get; set; } 

        public string? CurrencyCode { get; set; } 
        public string? InventoryMode { get; set; } 

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class TenantIncludes
    {
        public bool TenantSettings { get; set; }
        public bool TenantSubscription { get; set; }
        public bool TenantUsageCounters { get; set; }
    }

    public enum TenantOrderKey
    {
        NameAr,
        NameEn,
        Status,
        CurrencyCode,
        InventoryMode,
        CreatedAt,
        UpdatedAt
    }
}
