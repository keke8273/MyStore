using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ReadModel
{
    public class ProductPrice
    {
        public Guid ProductPriceId { get; set; }

        public decimal Value { get; set; }

        public Guid ProductId { get; private set; }

        public Guid ProductSourceId { get; private set; }

        #region Navitgation
		public virtual Product Product {get; set;}

        public ProductSource ProductSource { get; set; }

        public ICollection<ProductPriceRecord> PriceHistory { get; set; }
        #endregion
    }
}
