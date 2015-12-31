using System;

namespace Store.Dto
{
    public class ProductInfoDto
    {
        public Guid ProductId { get; set; }
        public Guid ProductSourceId { get; set; }
        public decimal Price { get; set; }
        public decimal RetailPrice { get; set; }
        public bool IsAvailableOnline { get; set; }
    }
}