using System;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging;

namespace ProductTracking.Contracts.Events
{
    public class ProductPriceUpdated : VersionedEvent, ITimeStampedEvent
    {
        public Guid ProductSourceId { get; set; }
        public decimal NewPrice { get; set; }
        public decimal PreviousPrice { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
