using System;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging;

namespace ProductTracking.Events
{
    public class OnlineAvailabilityUpdated : VersionedEvent, ITimeStampedEvent
    {
        public Guid ProductSourceId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this product is currently available online.
        /// </summary>
        public bool NewAvailability { get; set; }
        public bool PreviousAvailability { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
