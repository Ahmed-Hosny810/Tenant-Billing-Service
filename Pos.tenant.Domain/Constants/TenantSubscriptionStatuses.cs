using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Domain.Constants
{
    public static class TenantSubscriptionStatuses
    {
        public const string Pending = "Pending";
        public const string Active = "Active";
        public const string PastDue = "PastDue";
        public const string Expired = "Expired";
        public const string Cancelled = "Cancelled";
    }
}
