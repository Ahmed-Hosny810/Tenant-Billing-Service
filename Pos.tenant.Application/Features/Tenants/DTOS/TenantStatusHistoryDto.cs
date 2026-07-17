using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.DTOS
{
    public class TenantStatusHistoryDto
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        public string OldStatus { get; set; } = null!;
        public string NewStatus { get; set; } = null!;

        public string? Reason { get; set; }

        public DateTime ChangedAt { get; set; }
    }
}
