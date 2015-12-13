using System;
using CQRS.Infrastructure.EventSourcing;
using CQRS.Infrastructure.Messaging;

namespace Product.Contracts
{
    public abstract class ProductEvent : VersionedEvent, ITimeStampedEvent
    {
        public Guid ProductId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
