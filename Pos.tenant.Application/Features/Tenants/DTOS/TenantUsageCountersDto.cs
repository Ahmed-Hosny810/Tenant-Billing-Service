using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.DTOS
{
    public class TenantUsageCountersDto
    {
        public int BranchCount { get; set; }
        public int ProductCount { get; set; }
        public int CashierCount { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
