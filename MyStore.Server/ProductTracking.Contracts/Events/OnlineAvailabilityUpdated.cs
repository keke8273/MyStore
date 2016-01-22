using System;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;

namespace ProductTracking.Contracts.Events
{
    public class OnlineAvailabilityUpdated : VersionedEvent, ITimeStampedEvent
    {
        public OnlineAvailabilityUpdated(DateTime timeStamp)
        {
            TimeStamp = timeStamp;
        }

        public Guid ProductSourceId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this product is currently available online.
        /// </summary>
        public bool NewAvailability { get; set; }
        public bool PreviousAvailability { get; set; }
        public DateTime TimeStamp { get; private set; }
    }
}
