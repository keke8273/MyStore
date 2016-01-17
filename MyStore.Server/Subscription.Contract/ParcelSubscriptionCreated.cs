using System;
using CQRS.Infrastructure.Messaging;

namespace Subscription.Contracts
{
    public class ParcelSubscriptionCreated : IEvent
    {
        public Guid SourceId { get; private set; }

        public Guid UserId { get; set; }

        public Guid ExpressProviderId { get; set; }

        public string TrackingNumber { get; set; }
    }
}
