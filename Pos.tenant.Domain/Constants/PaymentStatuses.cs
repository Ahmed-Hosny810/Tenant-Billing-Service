using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Domain.Constants
{
    public static class PaymentStatuses
    {
        public const string Pending = "Pending";
        public const string Completed = "Completed";
        public const string Failed = "Failed";
        public const string Refunded = "Refunded";
    }
}
