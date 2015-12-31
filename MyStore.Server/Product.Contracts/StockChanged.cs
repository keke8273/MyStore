using System;
using CQRS.Infrastructure.Messaging;

namespace Store.Contracts
{
    public class StockChanged : ProductEvent, ITimeStampedEvent
    {
        public Guid ProductSourceId { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeStamp  { get; set; }
    }
}
