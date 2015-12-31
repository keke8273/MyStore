using System;

namespace Store.ReadModel
{
    public class ProductPriceRecord
    {
        public Guid ProductPriceRecordId { get; set; }

        public Decimal Price { get; set; }
        
        public DateTime TimeStamp { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProducetSourceId { get; set; }

        public virtual ProductPrice ProductPrice { get; set; }
    }
}
