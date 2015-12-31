using System;

namespace Product.Contracts
{
    public class ProductCreated : ProductEvent
    {
        public string Name { get; set; }

        public Guid BrandId { get; set; }

        public Uri ImageUrl { get; set; }
    }
}
