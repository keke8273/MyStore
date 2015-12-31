using System;
using CQRS.Infrastructure.Messaging;

namespace Product.Contracts
{
    public class OnlineAvailabilityUpdated : ProductEvent
    {
        public Guid ProductSourceId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this product is available online.
        /// </summary>
        public bool NewAvailability { get; set; }
        public bool PreviousAvailability { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
