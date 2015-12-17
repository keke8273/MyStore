
namespace Product.Contracts
{
    using System;
    using CQRS.Infrastructure.Messaging;

    public class PriceChanged : ProductEvent, ITimeStampedEvent
    {
        public Guid ProductPriceId { get; set; }

        public decimal Price { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
