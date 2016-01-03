using System;

namespace Store.ReadModel
{
    public class Stock
    {
        public Guid ProductId { get; set; }
        public Guid StockLocationId { get; set; }
        public virtual Product Product { get; set; }
    }
}
