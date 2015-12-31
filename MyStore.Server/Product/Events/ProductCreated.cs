using System;
using Store.Contracts;

namespace Store.Events
{
    public class ProductCreated : ProductEvent
    {
        public Guid BrandId { get; set; }

        public string ProductName { get; set; }

        public Uri ImageUrl { get; set; }
    }
}