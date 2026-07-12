using Pos.tenant.Domain.Common;
using Pos.tenant.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Domain.Models
{
    public partial class TenantSubscription : BaseEntity
    {
        public Guid TenantId { get; set; }
        public Guid PlanId { get; set; }

        public string Status { get; set; } = SubscriptionStatuses.Active;

        public DateTime CurrentPeriodStart { get; set; }
        public DateTime CurrentPeriodEnd { get; set; }

        public DateTime? GracePeriodEndsAt { get; set; }

        public void ChangePlan(Guid newPlanId)
        {
            PlanId = newPlanId;
        }

        public void MarkActive()
        {
            Status = SubscriptionStatuses.Active;
            GracePeriodEndsAt = null;
        }

        public void MarkPastDue(DateTime gracePeriodEndsAt)
        {
            Status = SubscriptionStatuses.PastDue;
            GracePeriodEndsAt = gracePeriodEndsAt;
        }

        public void Expire()
        {
            Status = SubscriptionStatuses.Expired;
        }

        public void Cancel()
        {
            Status = SubscriptionStatuses.Cancelled;
        }
    }
}
