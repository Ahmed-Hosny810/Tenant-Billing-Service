using Pos.tenant.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Domain.Models
{
    public partial class TenantStatusHistory:BaseEntity
    {
        public Guid TenantId { get; set; }

        public string OldStatus { get; set; } = null!;
        public string NewStatus { get; set; } = null!;

        public string? Reason { get; set; }

        public DateTime ChangedAt { get; set; }
    }
}
