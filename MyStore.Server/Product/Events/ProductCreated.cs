using System;
using CQRS.Infrastructure.EventSourcing;

namespace Product.Events
{
    public class ProductCreated : VersionedEvent
    {
        public Guid BrandId { get; set; }

        public string ProductName { get; set; }

        public Uri ImageUri { get; set; }
    }
}