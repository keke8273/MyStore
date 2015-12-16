using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ReadModel
{
    public class ProductPriceRecord
    {
        public Guid ProductPriceRecordId { get; set; }

        public Guid ProductPriceId { get; set; }

        public DateTime TimeStamp { get; set; }

        public Decimal Price { get; set; }

        public virtual ProductPrice ProductPrice { get; set; }
    }
}
