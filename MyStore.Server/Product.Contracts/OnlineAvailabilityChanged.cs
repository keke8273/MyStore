using System;
using CQRS.Infrastructure.Messaging;

namespace Product.Contracts
{
    public class OnlineAvailabilityChanged : ProductEvent
    {
        /// <summary>
        /// Gets or sets a value indicating whether this product is available online.
        /// </summary>
        public bool IsAvailable { get; set; }
        /// <summary>
        /// Gets or sets the availibility source.
        /// </summary>
        public Source Source { get; set; }
    }
}
