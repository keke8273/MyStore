using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ReadModel
{
    public class ProductStock
    {
        public Guid ProductStockId { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
