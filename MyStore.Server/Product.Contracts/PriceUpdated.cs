
using System;
using CQRS.Infrastructure.Messaging;

namespace Store.Contracts
{
    public class PriceUpdated : ProductEvent, ITimeStampedEvent
    {
        public Guid ProductSourceId { get; set; }
        public decimal NewPrice { get; set; }
        public decimal PreviousPrice { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
