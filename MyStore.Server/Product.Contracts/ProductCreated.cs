using System;

namespace Store.Contracts
{
    public class ProductCreated : ProductEvent
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }
    }
}
