using System;
using CQRS.Infrastructure.Messaging;

namespace Subscription.Contracts
{
    public class ParcelSubscriptionCreated : IEvent
    {
        public Guid SourceId { get; set; }

        public Guid ParceId { get; set; }

        public Guid UserId { get; set; }

        public string ExpressProvider { get; set; }

        public string TrackingNumber { get; set; }
    }
}
