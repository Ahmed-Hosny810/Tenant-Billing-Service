using Pos.tenant.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Domain.Models
{
    public class SubscriptionPlan : BaseEntity
    {
        public string Code { get; set; } = null!;

        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;

        public string? DescriptionAr { get; set; }
        public string? DescriptionEn { get; set; }

        public decimal MonthlyPrice { get; set; }

        public int? BranchLimit { get; set; }
        public int? ProductLimit { get; set; }
        public int? CashierLimit { get; set; }

        public bool AllowVariants { get; set; }
        public bool IsActive { get; set; } = true;

        public string GetDisplayName(string? language)
        {
            var isArabic = string.Equals(language, "ar", StringComparison.OrdinalIgnoreCase);

            return isArabic
                ? NameAr ?? NameEn
                : NameEn ?? NameAr;
        }
    }
}
