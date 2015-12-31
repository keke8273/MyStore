using System;
using System.Collections.Generic;

namespace Store.ReadModel
{
    public class ProductPrice
    {
        public decimal Value { get; set; }

        public DateTime LastUpdated { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProductSourceId { get; set; }

        #region Navitgation
		public virtual Product Product {get; set;}

        public virtual ProductSource ProductSource { get; set; }

        public virtual ICollection<ProductPriceRecord> PriceHistory { get; set; }
        #endregion
    }
}
