using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.DTOS
{
    public class TenantSettingsDto
    {
        public decimal DefaultVatRate { get; set; }
        public bool PricesIncludeTax { get; set; }

        public string? ReceiptFooterAr { get; set; }
        public string? ReceiptFooterEn { get; set; }

        public bool AllowReturns { get; set; }
        public decimal DiscountLimitPercent { get; set; }

        public string DefaultLanguage { get; set; } = null!;
    }
}
