using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Domain.Constants
{
    public static class InvoiceStatuses
    {
        public const string Unpaid = "Unpaid";
        public const string Paid = "Paid";
        public const string Overdue = "Overdue";
        public const string Cancelled = "Cancelled";
    }
}
