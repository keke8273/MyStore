using System;

namespace Product.ReadModel
{
    public class ProductStock
    {
        public Guid ProductId { get; set; }
        public Guid StockLocationId { get; set; }
        public virtual Product Product { get; set; }
    }
}
