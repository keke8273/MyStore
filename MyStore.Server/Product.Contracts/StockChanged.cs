
namespace Product.Contracts
{
    using System;
    using CQRS.Infrastructure.Messaging;
    public class StockChanged : ProductEvent
    {
        public int Quantity { get; set; }
        public Source Source { get; set; }
    }
}
