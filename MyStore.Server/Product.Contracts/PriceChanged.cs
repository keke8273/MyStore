
namespace Product.Contracts
{
    using System;
    using CQRS.Infrastructure.Messaging;

    public class PriceChanged : ProductEvent
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        public Source Source { get; set; }
        public decimal Price { get; set; }
    }
}
