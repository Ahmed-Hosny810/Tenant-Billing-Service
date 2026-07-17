using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Domain.Models
{
    public partial class TenantUsageCounters
    {
        public Guid TenantId { get; set; }

        public int BranchCount { get; set; }
        public int ProductCount { get; set; }
        public int CashierCount { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Tenant Tenant { get; set; } = null!;
    }
}
