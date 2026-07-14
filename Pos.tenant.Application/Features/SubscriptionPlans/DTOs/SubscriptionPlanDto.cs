using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPlans.DTOs
{
    public class SubscriptionPlanDto
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;

        public string? DescriptionAr { get; set; }
        public string? DescriptionEn { get; set; }

        public decimal MonthlyPrice { get; set; }

        public string CurrencyCode { get; set; } = "EGP";

        public int? BranchLimit { get; set; }
        public int? ProductLimit { get; set; }
        public int? CashierLimit { get; set; }

        public bool AllowVariants { get; set; }
    }
}
