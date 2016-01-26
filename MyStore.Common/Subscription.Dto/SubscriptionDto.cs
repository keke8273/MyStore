using System;

namespace Subscription.Dto
{
    public class SubscriptionDto
    {
        public Guid SubscriberId { get; set; }
        public Guid SubscribeeId { get; set; }
    }
}
